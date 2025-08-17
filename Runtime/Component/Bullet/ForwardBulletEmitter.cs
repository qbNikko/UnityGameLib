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
            Gizmos.color = Color.blue;
            
            Gizmos.DrawLine(
                transform.position,
                transform.position+transform.up
            );
            Gizmos.DrawWireSphere(transform.position, 0.05f);
            Gizmos.color = Color.white;
#endif
        }
    }
}