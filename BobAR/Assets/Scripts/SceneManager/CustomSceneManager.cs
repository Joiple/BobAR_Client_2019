using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class CustomSceneManager : MonoBehaviour
{
    [NonSerialized]
    public static CustomSceneManager Instance;

    public int StartingSceneIndex;
    public string NowLoaded;
    public Object LoadingScreen;
    public Object[] Scenes;
    
    public void Awake()
    {
        Instance = this;
        SceneManager.sceneLoaded+= OnLoaded;
    }

    private void OnLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name+" 로드");
        SceneManager.SetActiveScene(scene);
    }

    public IEnumerator LoadSceneWithoutLoading(string name)
    {
        yield return new WaitForEndOfFrame();
        Scene buffer = SceneManager.GetSceneByName(name);
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        while (SceneManager.GetActiveScene().name != name) yield return null;
    }


    public void Start()
    {
        StartCoroutine(LoadSceneWithoutLoading(Scenes[StartingSceneIndex].name));
    }

    public IEnumerator LoadSceneWithLoading(string name)
    {
        Scene scene = SceneManager.GetActiveScene();
        yield return StartCoroutine(LoadSceneWithoutLoading(LoadingScreen.name));
        SceneManager.UnloadSceneAsync(scene);
        yield return LoadSceneWithoutLoading(name);
        SceneManager.UnloadSceneAsync(LoadingScreen.name);
    }


    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneWithLoading(Scenes[index].name));
    }
}
