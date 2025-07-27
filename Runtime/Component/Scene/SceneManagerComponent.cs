using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UnityGameLib.Component.Scene
{
    public class SceneManagerComponent : MonoBehaviour
    {
        public static SceneManagerComponent Instance { get; private set; }
        public UnityEvent<string> OnSceneLoaded;
        public UnityEvent<string> OnSceneAdd;
        public UnityEvent<string> OnSceneUnload;
        [SerializeField] private LoaderScreen _loaderScreen;

        public LoaderScreen LoaderScreen => _loaderScreen;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance =  this;
            DontDestroyOnLoad(gameObject);
        }

        public async void LoadScene(string sceneName)
        {
            StartCoroutine(LoadTargetScene(sceneName));
        }

        private IEnumerator LoadTargetScene(string sceneName)
        {
            _loaderScreen?.gameObject.SetActive(true);
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Single);
            operation.allowSceneActivation = false;
            while (!operation.isDone)
            {
                LoaderScreen.SendProgress(operation.progress);
                Debug.Log(operation.progress);
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
            Debug.Log("Loading scene " + sceneName);
            _loaderScreen?.gameObject.SetActive(false);
            OnSceneLoaded.Invoke(sceneName);
        }

        public void AddScene(string sceneName)
        {
            StartCoroutine(LoadTargetAdditionScene(sceneName));
        }

        private IEnumerator LoadTargetAdditionScene(string sceneName)
        {
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
            while (!operation.isDone)
            {
                yield return null;
            }
            OnSceneAdd.Invoke(sceneName);
        }


        public void UnloadScene(string sceneName)
        {
            StartCoroutine(UnloadTargetAdditionScene(sceneName));
        }

        private IEnumerator UnloadTargetAdditionScene(string sceneName)
        {
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            while (!operation.isDone)
            {
                yield return null;
            }
            OnSceneUnload.Invoke(sceneName);
        }
    }
}