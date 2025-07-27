using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityGameLib.Component.Scene
{
    public class LoaderScreen : MonoBehaviour
    {
        public UnityEvent<string> OnProcessingMessageEvent;
        public UnityEvent<float> OnProgressEvent;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnEnable()
        {
            OnProgressEvent.Invoke(0);
            OnProcessingMessageEvent.Invoke("");
        }

        public void SendMessage(string message)
        {
            OnProcessingMessageEvent?.Invoke(message);
        }
        
        public void SendProgress(float progress)
        {
            OnProgressEvent?.Invoke(progress);
        }
    }
}