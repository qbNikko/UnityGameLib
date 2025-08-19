using UnityEngine;
using UnityGameLib.Component.Enemy;

namespace UnityGameLib.UnityGameLib.Example.Enemy
{
    [CreateAssetMenu(fileName = "EnemyType", menuName = "UnityGameLib/Example/EnemyType", order = 1)]
    public class EnemyTypeGame : Component.Enemy.EnemyType
    {
        public int life;
    }
}