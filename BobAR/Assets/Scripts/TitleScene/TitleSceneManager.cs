using System.Collections;
using Common.PluginWrappers;
using CustomSceneManagement;
using UnityEngine;

namespace TitleScene {

    public class TitleSceneManager : MonoBehaviour {
        public void Start() {
            StartCoroutine(Coroutine());
        }
        public IEnumerator Coroutine() {
            yield return new WaitForSeconds(2f);
            Toaster.Initialize();
            Toaster.ShowToast("Server Connection Failed, Enable Demo mode.");
            CustomSceneManager.instance.LoadScene(0);

        }
    }

}