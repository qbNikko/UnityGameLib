using System;
using UnityEngine;
using UnityGameLib.Component.Pool;
using UnityGameLib.Interface;

namespace UnityGameLib.Component.Bullet
{
    public class BulletSpawner : MonoBehaviour,IInitialized
    {
        private ObjectPoolManager<BulletComponent> _poolManager;
        [Header("Bullet settings")]
        [SerializeField] private BulletType _bulletType;
        
        [Header("Pool settings")]
        [SerializeField] private int _pullSize = 1000;
        [SerializeField] private bool _checkPoolSize;

        public BulletType BulletType
        {
            get => _bulletType;
            internal set => _bulletType = value;
        }

        public int PullSize
        {
            get => _pullSize;
            internal set => _pullSize = value;
        }

        public bool CheckPoolSize
        {
            get => _checkPoolSize;
            internal set => _checkPoolSize = value;
        }


        private void Awake()
        {
            if(_poolManager==null) Initialize();
        }

        private void OnDestroy()
        {
            _poolManager.Dispose();
        }

        public void Initialize()
        {
            if(_poolManager!=null) _poolManager.Dispose();
            _poolManager = new ObjectPoolManager<BulletComponent>(_pullSize, Math.Clamp(100, 1, _pullSize), _checkPoolSize, () => Instantiate(_bulletType._prefab, transform, true));
        }

        public BulletComponent GetBullet()
        {
            BulletComponent bulletComponent =  _poolManager.Get();
            bulletComponent.Type = _bulletType;
            return bulletComponent;
        }
    }
}