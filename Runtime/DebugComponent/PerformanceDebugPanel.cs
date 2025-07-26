using System;
using UnityEngine;
using UnityEngine.UI;

namespace UnityGameLib.DebugComponent
{
    public class PerformanceDebugPanel : MonoBehaviour
    {
        [SerializeField] bool showFps = false;
        private float fps = 30f;
        
        void OnGUI()
        {
            float newFPS = 1.0f / Time.smoothDeltaTime;
            fps = Mathf.Lerp(fps, newFPS, 0.0005f);
            GUI.Label(new Rect(0, 0, 100, 100), "FPS: " + (int)(1.0f / Time.smoothDeltaTime));
        }
    }
}