using UnityEngine;

namespace UnityGameLib.Component.Bullet
{
    public class TranslateBulletComponent : BulletComponent
    {
        private Vector3 currentPosition;
        public override void StartLifetime()
        {
            base.StartLifetime();
            currentPosition = _transform.position;
        }

        protected override void Move(float dt)
        {
            currentPosition += Direction * (BulletType.Speed * dt);
            _transform.position = currentPosition;
        }
    }
}