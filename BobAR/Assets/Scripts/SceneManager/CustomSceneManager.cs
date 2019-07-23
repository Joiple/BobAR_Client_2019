using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class CustomSceneManager : MonoBehaviour
{
    [NonSerialized]
    public static CustomSceneManager Instance;
    [Header("씬 모음")]
    public Object[] scenes;
    [Header("처음 시작하는 씬 번호")]
    public int startingSceneIndex;
    [Header("로딩스크린용 씬")]
    public Object loadingScreen;
    [Header("가고자 하는 씬 번호")] public int Goto;
    [Header("작동")] public bool starter;
    /// <summary>
    /// 초기화
    /// </summary>
    public void Awake()
    {
        Instance = this;
        SceneManager.sceneLoaded+= OnLoaded;
    }
    /// <summary>
    /// 씬 로드시 할 일
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name+" 로드");
        SceneManager.SetActiveScene(scene);
    }
    /// <summary>
    /// 로딩화면 없이 씬 로드
    /// </summary>
    /// <param name="name">대상 씬의 이름</param>
    /// <returns></returns>
    public IEnumerator LoadSceneWithoutLoading(string name)
    {
        yield return new WaitForEndOfFrame();
        Scene buffer = SceneManager.GetSceneByName(name);
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        while (SceneManager.GetActiveScene().name != name) yield return null;
    }


    public void Start() {
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            if (gameObject.scene != SceneManager.GetSceneAt(i)) {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }
        StartCoroutine(LoadSceneWithoutLoading(scenes[startingSceneIndex].name));
    }
    /// <summary>
    /// 로딩화면과 함꼐 씬 로드
    /// </summary>
    /// <param name="name">대상 씬의 이름</param>
    /// <returns></returns>
    public IEnumerator LoadSceneWithLoading(string name)
    {
        Scene scene = SceneManager.GetActiveScene();
        yield return StartCoroutine(LoadSceneWithoutLoading(loadingScreen.name));
        SceneManager.UnloadSceneAsync(scene);
        yield return LoadSceneWithoutLoading(name);
        SceneManager.UnloadSceneAsync(loadingScreen.name);
    }

    /// <summary>
    /// 일반적인 씬 로드
    /// </summary>
    /// <param name="index">씬 번호</param>
    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneWithLoading(scenes[index].name));
    }

    public void Update() {
        if (starter) {
            LoadScene(Mathf.Clamp(Goto,0,scenes.Length));
            starter = false;
        }
    }
}
