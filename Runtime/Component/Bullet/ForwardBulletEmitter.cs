using System;
using UnityEditor;
using UnityEngine;

namespace UnityGameLib.Component.Bullet
{
    public class ForwardBulletEmitter : BulletEmitter
    {
        protected override void ConfigBullet(BulletComponent bullet)
        {
            bullet.Direction = transform.up;
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.red;
            
            Gizmos.DrawLine(
                Vector3.zero,
                transform.up
            );
            Gizmos.DrawWireSphere(Vector3.zero, 0.05f);
            Gizmos.color = Color.white;
#endif
        }
    }
}