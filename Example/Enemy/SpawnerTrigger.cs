using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameLib.Component.Enemy;
using UnityGameLib.Component.Utils;

namespace UnityGameLib.UnityGameLib.Example.Enemy
{
    public class SpawnerTrigger : MonoBehaviour
    {
        [SerializeField] private List<EnemyEmitter> _emitter;
        [SerializeField] private List<EnemyTypeGame> _enemyTypes;
        private RandomComponent randomComponent;

        private void Awake()
        {
            randomComponent = FindFirstObjectByType<RandomComponent>();
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            int count = 0;
            while (count<100)
            {
                yield return new WaitForSeconds(0.1f);
                foreach (var e in _emitter)
                {
                    int nextInt = randomComponent.NextInt(0,_enemyTypes.Count);
                    e.Spawn(_enemyTypes[nextInt]);
                }
                
            }
            
        }
    }
}