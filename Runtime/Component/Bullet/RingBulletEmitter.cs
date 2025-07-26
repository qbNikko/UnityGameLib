using System;
using UnityEngine;

namespace UnityGameLib.Component.Bullet
{
    public class RingBulletEmitter : BulletEmitter
    {
        [SerializeField] float _angle;
        [SerializeField, Range(1,36)] int _spawnCount = 1;
        [SerializeField] Vector3 _startDirection;
        float _offsetAngle;
        float _currentAngle;
        Vector3 _currentDirection;
        

        private void OnValidate()
        {
            if(_spawnCount<1) _spawnCount = 1;
        }

        private void Awake()
        {
            
            if(_startDirection == Vector3.zero) _startDirection =  transform.up;
            _offsetAngle = 360 / _spawnCount;
            _currentAngle = _angle;
        }

        public override void Shoot(out BulletComponent bulletComponent)
        {
            BulletComponent bullet = null; 
            _startDirection = Quaternion.Euler(0, 0, _angle) * _startDirection;
            _currentDirection = _startDirection;
            for (int i = 0; i < _spawnCount; i++)
            {
                base.Shoot(out bullet);
                _currentAngle += _offsetAngle;
            }
            _currentAngle = 0;
            bulletComponent = bullet;
        }

        protected override void ConfigBullet(BulletComponent bullet)
        {
            _currentDirection = Quaternion.Euler(0, 0, _currentAngle) * _currentDirection;
            bullet.Direction =  _currentDirection;
        }
    }
}