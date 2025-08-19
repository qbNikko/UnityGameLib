using UnityEngine;
using UnityGameLib.Component.Utils;

namespace UnityGameLib.Component.Enemy
{
    public abstract class EnemyEmitter : MonoBehaviour
    {
        [SerializeField] public EnemySpawner  Spawner;
        [SerializeField] public RandomComponent Random;
        
        public void Spawn(EnemyType type)
        {
            Enemy enemy = Spawner.Spawn(type);
            GetEnemyPosition(out Vector3 position);
            enemy.transform.position = position;
            enemy.Initialize(type);
            InitializeEnemy(enemy);
        }

        protected virtual void InitializeEnemy(Enemy enemy)
        {
            
        }

        protected abstract void GetEnemyPosition(out Vector3 position);
    }
    
    
}