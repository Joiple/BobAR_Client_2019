using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = DebugWrap.Debug;

namespace CustomSceneManagement {

    public class CustomSceneManager : MonoBehaviour {
        [NonSerialized] public static CustomSceneManager Instance;
        [Header("씬 모음")] public string[] scenes;
        [Header("처음 시작하는 씬 번호")] public int startingSceneIndex;
        [Header("로딩스크린용 씬")] public string loadingScreen;
        [Header("가고자 하는 씬 번호")] public int Goto;
        [Header("작동")] public bool starter;

        /// <summary>
        /// 초기화
        /// </summary>
        public void Awake() {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 씬 로드시 할 일
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        private void OnLoaded(Scene scene, LoadSceneMode mode) {
            Debug.Log(scene.name + " loaded");
            SceneManager.SetActiveScene(scene);
        }

        /// <summary>
        /// 로딩화면 없이 씬 로드
        /// </summary>
        /// <param name="name">대상 씬의 이름</param>
        /// <returns></returns>
        public IEnumerator LoadSceneWithoutLoading(string name) {
            yield return new WaitForEndOfFrame();
            Debug.Log("Getting Target Scene");
            Scene buffer = SceneManager.GetSceneByName(name);
            Debug.Log("Loading Target Scene");
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
            Debug.Log("Checking Scene Loaded");
            while (SceneManager.GetActiveScene().name != name) yield return null;
            Debug.Log("Load done");
        }


        public void Start() {
            Debug.Log("Initialized");
#if UNITY_EDITOR
            for (int i = 0; i < SceneManager.sceneCount; i++) {
                if (gameObject.scene != SceneManager.GetSceneAt(i)) {
                    SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
                    Debug.Log("Removed "+i);
                }
            }
            Debug.Log("Removing Finished");
#endif
            Debug.Log("EventBinding");
            SceneManager.sceneLoaded += OnLoaded;
            Debug.Log(scenes!=null);
            Debug.Log(scenes[startingSceneIndex]!=null);
            StartCoroutine(LoadSceneWithoutLoading(scenes[startingSceneIndex]));
            Debug.Log("Loading Started");
        }

        /// <summary>
        /// 로딩화면과 함꼐 씬 로드
        /// </summary>
        /// <param name="name">대상 씬의 이름</param>
        /// <returns></returns>
        public IEnumerator LoadSceneWithLoading(string name) {
            Debug.Log("Getting Active Scene");
            Scene scene = SceneManager.GetActiveScene();
            Debug.Log("Loading LoadingScene");
            yield return StartCoroutine(LoadSceneWithoutLoading(loadingScreen));
            Debug.Log("Unloading Previous Scene");
            SceneManager.UnloadSceneAsync(scene);
            Debug.Log("Loading Next Scene");
            yield return LoadSceneWithoutLoading(name);
            Debug.Log("Unload LoadingScene");
            SceneManager.UnloadSceneAsync(loadingScreen);
        }

        /// <summary>
        /// 일반적인 씬 로드
        /// </summary>
        /// <param name="index">씬 번호</param>
        public void LoadScene(int index) {
            StartCoroutine(LoadSceneWithLoading(scenes[index]));
        }

        /// <summary>
        /// 일반적인 씬 로드
        /// </summary>
        /// <param name="index">씬 번호</param>
        public static void StaticLoadScene(int index) {
            Instance.LoadScene(index);
        }


        public void Update() {
            if (starter) {
                LoadScene(Mathf.Clamp(Goto, 0, scenes.Length));
                starter = false;
            }
        }
    }

}