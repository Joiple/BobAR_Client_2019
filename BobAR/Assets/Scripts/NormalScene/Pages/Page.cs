using UnityEngine;

namespace NormalScene.Pages {

    public class Page : MonoBehaviour {
        internal NormalSceneManager manager;
        public LoadingScreen screen;

        public virtual Page Initialize(NormalSceneManager controller) {
            this.manager = controller;
            if (screen != null) screen.gameObject.SetActive(true);

            return this;
        }

        public virtual void Exit() {
            manager.Exit();
        }
    }

}