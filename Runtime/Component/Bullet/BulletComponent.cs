using System;
using System.Collections;
using UnityEngine;
using UnityGameLib.Component.Pool;

namespace UnityGameLib.Component.Bullet
{
    public abstract class BulletComponent : MbPooledObject
    {
        protected Transform _transform;
        internal BulletType Type;
        public BulletType BulletType => Type;
        public Vector3 Direction;
        private float _currentLifetime = 0;

        protected virtual void Awake()
        {
            _transform = transform;
        }

        public override void Reset()
        {
            base.Reset();
            Reset_();
        }

        protected virtual void Reset_()
        {
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            Move(deltaTime);
            if((_currentLifetime+=deltaTime)>=BulletType.LifeTime) ReleaseInPool();
        }

        protected abstract void Move(float dt);

        public virtual void StartLifetime()
        {
            _currentLifetime = 0;
        }
    }
}