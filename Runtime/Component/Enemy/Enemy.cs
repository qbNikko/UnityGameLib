using System;
using UnityEngine;
using UnityGameLib.Component.Pool;

namespace UnityGameLib.Component.Enemy
{
    public abstract class Enemy : MbPooledObject
    {
        private BoundsObject _boundsObject;
        private HealthComponent _healthComponent;
        protected EnemyType _type;

        public EnemyType Type => _type;

        public virtual void Initialize(EnemyType type)
        {
            _type =  type;
        }

        protected virtual void Awake()
        {
            _boundsObject = new BoundsObject(gameObject);
            TryGetComponent(out _healthComponent);
        }

        public HealthComponent HealthComponent => _healthComponent;
        public BoundsObject BoundsObject => _boundsObject;
    }
}