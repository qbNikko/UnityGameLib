using System;
using UnityEngine;

namespace UnityGameLib.Component.Bullet
{
    public abstract class BulletEmitter : MonoBehaviour
    {
        [SerializeField] private MixedBulletSpawner _bulletSpawner;
        [SerializeField] private BulletType _bulletType;

        public MixedBulletSpawner BulletSpawner
        {
            get => _bulletSpawner;
            set => _bulletSpawner = value;
        }

        public BulletType BulletType => _bulletType;

        public void Shoot()
        {
            Shoot(out var component);
        }
        
        public virtual void Shoot(out BulletComponent bulletComponent)
        {
            bulletComponent = _bulletSpawner.GetBullet(_bulletType);
            bulletComponent.transform.position = transform.position;
            bulletComponent.StartLifetime();
            ConfigBullet(bulletComponent);
        }

        protected abstract void ConfigBullet(BulletComponent bullet);
    }
}