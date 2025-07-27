using UnityEngine;
using UnityGameLib.Component.Scene;

namespace UnityGameLib
{
    public class LoadSceneMb : MonoBehaviour
    {
        [SerializeField] private string loadSceneName;
        
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Load()
        {
            SceneManagerComponent.Instance.LoadScene(loadSceneName);
        }

        public void LoadAdditive()
        {
            SceneManagerComponent.Instance.AddScene("SceneAdditive");
        }
    }
}
