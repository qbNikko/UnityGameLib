using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using UnityGameLib.Collection;
using UnityGameLib.Component.Utils;

namespace UnityGameLib.Component.Enemy
{
    
    public class LineEnemyEmitter : EnemyEmitter
    {
        private struct PointData
        {
            public readonly int Index;
            public readonly float AllDistance;
            public readonly float Distance;

            public PointData(int index, float allDistance, float distance)
            {
                Index = index;
                AllDistance = allDistance;
                Distance = distance;
            }
        }
        
        [SerializeField] public List<Vector2> positions;
        private float _maxDistance = 0;
        List<PointData> distanceIndexDict;
        
        private DisposeContainer _disposeContainer;
        
        private void Awake()
        {
            _disposeContainer = new DisposeContainer();
            _disposeContainer.AddDisposable(CollectionPool.GetList(out distanceIndexDict));
            Vector2 currentPosition = positions[0];
            for (int i = 1; i < positions.Count; i++)
            {
                float distance = Vector3.Distance(currentPosition, positions[i]);
                _maxDistance += distance;
                distanceIndexDict.Add(new PointData(i,_maxDistance,distance));
            }
        }

        protected override void GetEnemyPosition(out Vector3 position)
        {
            float nextFloat = Random.Random.NextFloat(0, _maxDistance);
            PointData distancePoint = distanceIndexDict.FirstOrDefault(k => k.AllDistance >= nextFloat);
            float elementDistance = nextFloat - (distancePoint.AllDistance-distancePoint.Distance);
            float percent = elementDistance / distancePoint.Distance;
            position = (Vector3)(((positions[distancePoint.Index]-positions[distancePoint.Index-1])*percent)+positions[distancePoint.Index-1])+transform.position;
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.blue;
            Vector3 previousPosition = positions[0];
            positions.ForEach(p=>
            {
                Gizmos.DrawWireSphere((Vector3)p+transform.position, 0.1f);
                if(previousPosition!=(Vector3)positions[0]) Gizmos.DrawLine(previousPosition, (Vector3)p+transform.position);
                previousPosition = (Vector3)p + transform.position;
            });
            
            Gizmos.color = Color.white;
#endif
        }
    }
}