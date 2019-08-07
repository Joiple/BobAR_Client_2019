using System.Collections;
using System.Collections.Generic;
using Network;
using Network.Data;
using UnityEngine;

namespace MainScene.SearchPages {

    public class SearchPage : MonoBehaviour {
        public void Search(string value) {
            StartCoroutine(SearchInternal(value));
        }

        public virtual IEnumerator SearchInternal(string query) {
            yield return null;
        }

        public void Open() {
            gameObject.SetActive(true);
            StartCoroutine(OpenCoroutine());
        }
        public virtual IEnumerator OpenCoroutine() {
            yield return null;
        }

        public void Close() => StartCoroutine(CloseCoroutine());
        public virtual IEnumerator CloseCoroutine() {
            yield return null;
            gameObject.SetActive(false);
        }
        public void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                StartCoroutine(CloseCoroutine());
            }
        }
    }

}
