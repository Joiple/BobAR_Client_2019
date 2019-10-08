using System.Collections;
using CustomSceneManagement;
using UnityEngine;

namespace TitleScene {

    public class TitleSceneManager : MonoBehaviour {
        public void Start() {
            StartCoroutine(Coroutine());
        }
        public IEnumerator Coroutine() {
            yield return new WaitForSeconds(2f);
            //TODO Toast
            CustomSceneManager.instance.LoadScene(0);

        }
    }

}