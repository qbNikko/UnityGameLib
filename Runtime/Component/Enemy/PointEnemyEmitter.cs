using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameLib.Component.Enemy
{
    public class PointEnemyEmitter : EnemyEmitter
    {
        [SerializeField] public List<Vector2> positions;

        protected override void GetEnemyPosition(out Vector3 position)
        {
            position = (Vector3)positions[Random.Random.NextInt(0, positions.Count)] +  transform.position;
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.blue;
            positions.ForEach(p=>
            {
                Gizmos.DrawWireSphere((Vector3)p+transform.position, 0.1f);
            });
            
            Gizmos.color = Color.white;
#endif
        }
    }
}