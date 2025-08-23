using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityGameLib.Collection;
using UnityGameLib.Component.Pool;

namespace UnityGameLib.Component.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        
        private List<Enemy> _lifeEnemy;
        private Dictionary<EnemyType, ObjectPoolManager<Enemy>> _poolManager;
        private void Awake()
        {
            CollectionPool.GetList(out _lifeEnemy);
            CollectionPool.GetDictionary(out _poolManager);
        }

        public Enemy Spawn(EnemyType type)
        {
            ObjectPoolManager<Enemy> pool;
            if (!_poolManager.TryGetValue(type, out pool))
            {
                GameObject poolParentGo = new GameObject("PoolFor_"+type.name);
                poolParentGo.transform.parent = transform;
                pool = new ObjectPoolManager<Enemy>(200, 10, false, () => Instantiate(type.prefub, poolParentGo.transform));
                _poolManager.Add(type, pool);
            }
            Enemy enemy = pool.Get();
            enemy.SubscribeOnRelease((t)=>_lifeEnemy.Remove(enemy));
            _lifeEnemy.Add(enemy);
            return enemy;
        }

        public int GetLifeEnemy()
        {
            return _lifeEnemy.Count;
        }
        
        public bool TryFindEnemy(Func<Enemy, bool> predicate, ref List<Enemy> enemies)
        {
            enemies.Clear();
            foreach (Enemy enemy in _lifeEnemy)
            {
                if(predicate.Invoke(enemy)) enemies.Add(enemy);
            }
            return enemies.Count>0;
        }
        
        public bool TryFindEnemy(Vector3 position, ref List<Enemy> enemies)
        {
            enemies.Clear();
            foreach (Enemy enemy in _lifeEnemy)
            {
                if(enemy.BoundsObject.IsBounds(position)) enemies.Add(enemy);
            }
            return enemies.Count>0;
        }
        
        public bool TryFindEnemy(Collider2D collider, ref List<Enemy> enemies)
        {
            enemies.Clear();
            foreach (Enemy enemy in _lifeEnemy)
            {
                if(enemy.BoundsObject.IsBounds(collider)) enemies.Add(enemy);
            }
            return enemies.Count>0;
        }
        
        public bool TryFindEnemy(Ray ray, ref List<Enemy> enemies)
        {
            enemies.Clear();
            foreach (Enemy enemy in _lifeEnemy)
            {
                if(enemy.BoundsObject.IsBounds(ray)) enemies.Add(enemy);
            }
            return enemies.Count>0;
        }
    }
}