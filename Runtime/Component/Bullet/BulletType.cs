using UnityEngine;

namespace UnityGameLib.Component.Bullet
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "UnityGameLib/Type/BulletType", order = 1)]
    public class BulletType : ScriptableObject
    {
        public BulletComponent _prefab;
        public float Speed;
        public float LifeTime;
        public int Damage;
    }
}