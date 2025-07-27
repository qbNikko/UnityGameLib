using System;
using System.Threading;
using UnityEngine;
using UnityGameLib.Component.Scene;

namespace UnityGameLib
{
    public class WaitComponent : MonoBehaviour
    {
        [SerializeField] private string message;
        [SerializeField] private int delayTime;
        private void Awake()
        {
            SceneManagerComponent.Instance.LoaderScreen.SendMessage(message);
        }
    }
}