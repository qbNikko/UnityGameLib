using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace UnityGameLib.Component.Enemy
{
    public class SphereEnemyEmitter : EnemyEmitter
    {
        [SerializeField] public float radius;
        [SerializeField] public float excludeRadius;
        protected override void GetEnemyPosition(out Vector3 position)
        {
            float a = Random.Random.NextFloat() * 2 * Mathf.PI;
            float r = ((radius-excludeRadius) * Mathf.Sqrt(Random.Random.NextFloat()))+excludeRadius;
            position = new Vector3((r * Mathf.Cos(a)), (r * Mathf.Sin(a)), 0) + transform.position;
        }
        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            Gizmos.color = new Color(0, 0, 255, 0.2f);
            Gizmos.DrawSphere(transform.position, radius);
            if (excludeRadius != 0)
            {
                Gizmos.color = new Color(255, 0, 0, 0.2f);
                Gizmos.DrawSphere(transform.position, excludeRadius);
            }
            Gizmos.color = Color.white;
#endif
        }
    }
}