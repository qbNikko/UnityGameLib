using System;
using System.Collections.Generic;
using UnityEngine;
using UnityGameLib.Component.Pool;

namespace UnityGameLib.Component.Bullet
{
    public class MixedBulletSpawner : MonoBehaviour
    {
        private Dictionary<BulletType, BulletSpawner> _poolManager;
        
        [Header("Pool settings")]
        [SerializeField] private int _pullSize;
        [SerializeField] private bool _checkPoolSize;
        private void Awake()
        {
            _poolManager = new Dictionary<BulletType, BulletSpawner>();
        }

        private void OnDestroy()
        {
            foreach (var keyValuePair in _poolManager)
            {
                Destroy(keyValuePair.Value.gameObject);
            }
        }

        public BulletComponent GetBullet(BulletType type)
        {
            if (!_poolManager.TryGetValue(type, out BulletSpawner pool))
            {
                GameObject instantiate = new GameObject();
                instantiate.transform.parent = transform;
                instantiate.name = "PoolFor_" + type.name;
                BulletSpawner bulletSpawner = instantiate.AddComponent<BulletSpawner>();
                bulletSpawner.BulletType = type;
                bulletSpawner.PullSize = _pullSize;
                bulletSpawner.CheckPoolSize = _checkPoolSize;
                bulletSpawner.Initialize();
                pool = bulletSpawner;
                _poolManager.Add(type, bulletSpawner);
            }
            return pool.GetBullet();
        }
        
    }
}