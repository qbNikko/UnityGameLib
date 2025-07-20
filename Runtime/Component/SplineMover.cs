using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace UnityGameLib.Component
{
    /**
     * Перемещение объекта по Spline
     */
    public class SplineMover
    {
        /**
         * Событие при достижении пограничной позиции
         */
        public event Action OnBoundaryPosition;
        public event Action OnCirclePass;
        private SplineContainer _splineContainer;
        private Transform _target;
        private bool isLooping;
        public float MoveSpeed { get; set; } = 1f;
        public float CurrentPos { get; set; }
        

        public SplineMover(SplineContainer splineContainer, Transform target)
        {
            if (splineContainer == null || target == null)
            {
                Debug.LogError("splineContainer or target is null");
                return;
            }
            _splineContainer = splineContainer;
            _target = target;
            isLooping = _splineContainer.Spline.Closed;
        }
        
        /**
         * Для 2D Перемещения, у учётом вращения
         */
        public void Move2D(float delta, bool forwardDirection)
        {
            Calculate(delta, forwardDirection, out var pos, out var dir,  out var up);
            _target.SetPositionAndRotation(pos+(float3)_splineContainer.transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1), up));
        }
        /**
         * Для 3D Перемещения
         */
        public void Move3D(float delta, bool forwardDirection)
        {
            Calculate(delta, forwardDirection, out var pos, out var dir,  out var up);
            _target.SetPositionAndRotation(pos+(float3)_splineContainer.transform.position, Quaternion.LookRotation(dir));
        }

        public bool IsEndPosition()
        {
            return CurrentPos >= _splineContainer.Spline.GetLength() || CurrentPos <= 0;
        }

        private void Calculate(float delta, bool forwardDirection, out float3 pos, out float3 dir, out float3 up)
        {
            var splineLength = _splineContainer.Spline.GetLength();
            CurrentPos = Mathf.Clamp(CurrentPos + (forwardDirection ? 1 : -1) * MoveSpeed * delta, 0f, splineLength);
            ValidatePosition(splineLength);
            var normalizedPos = CurrentPos / splineLength;
            _splineContainer.Spline.Evaluate(normalizedPos, out pos, out dir, out up);
        }

        private void ValidatePosition(float splineLength)
        {
            if (CurrentPos >= splineLength)
            {
                if (isLooping)
                {
                    CurrentPos = 0;
                    OnCirclePass?.Invoke();
                }
                else
                {
                    CurrentPos = splineLength;
                    OnBoundaryPosition?.Invoke();
                }
            }
            else if (CurrentPos <= 0)
            {
                if (isLooping)
                {
                    CurrentPos = splineLength;
                    OnCirclePass?.Invoke();
                }
                else
                {
                    CurrentPos = 0;
                    OnBoundaryPosition?.Invoke();
                }
            }
        }
    }
}