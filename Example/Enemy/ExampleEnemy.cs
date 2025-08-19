using System;
using Unity.Mathematics;
using UnityEngine;
using UnityGameLib.Component.Enemy;
using UnityGameLib.Component.Utils;

namespace UnityGameLib.UnityGameLib.Example.Enemy
{
    [RequireComponent(typeof(Component.Enemy.HealthComponent))]
    public class ExampleEnemy : Enemy2D
    {
        private HealthComponent healthComponent;
        private RandomComponent randomComponent;
        private float _timeout;
        private float _currentTimeout=0f;
        private Vector3 _targetPosition;
        protected override void Awake()
        {
            base.Awake();
            healthComponent = GetComponent<HealthComponent>();
            healthComponent.Initialize();
            healthComponent.OnDead.AddListener(()=>ReleaseInPool());
            randomComponent = FindFirstObjectByType<RandomComponent>();
        }

        public override void Initialize(Component.Enemy.EnemyType type)
        {
            base.Initialize(type);
            EnemyTypeGame enemyTypeGame = type as EnemyTypeGame;
            healthComponent.SetMaxHealth(enemyTypeGame.life);
            healthComponent.Initialize();
            _timeout = randomComponent.Random.NextFloat(3, 5);
            _targetPosition = randomComponent.Random.NextFloat3(new float3(-6, -6, 0), new float3(6, 6, 0));
        }

        private void Update()
        {
            _currentTimeout+=Time.deltaTime;
            if (_currentTimeout >= _timeout)
            {
                healthComponent.Damage(1);
                _currentTimeout = 0;
            }
            // transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime*3);
            // if (Vector3.Distance(transform.position,_targetPosition)<0.1f)
            // {
                // _targetPosition = randomComponent.Random.NextFloat3(new float3(-6, -6, 0), new float3(6, 6, 0));
            // }
        }
    }
}