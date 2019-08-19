using System.Collections;
using UnityEngine;

namespace NormalScene.Pages {

    public class LoadingScreen :MonoBehaviour {
        public float expireTime = 2f;
        public void OnEnable() {
            StartCoroutine(Routine());
        }

        IEnumerator Routine() {
            yield return new WaitForSecondsRealtime(expireTime);
            gameObject.SetActive(false);
        }
    }

}