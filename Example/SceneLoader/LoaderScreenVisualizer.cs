using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityGameLib.Component.Scene;

namespace UnityGameLib
{
    public class LoaderScreenVisualizer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Image progressBar;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            SceneManagerComponent.Instance.LoaderScreen.OnProcessingMessageEvent.AddListener((m)=> infoText.text = m);
        }

        // Update is called once per frame
        void Update()
        {
            progressBar.transform.Rotate(Vector3.up, 10*Time.deltaTime);
        }
    }
}
