using UnityEngine;

namespace NormalScene.Pages {

    public class Page : MonoBehaviour {
        internal NormalSceneManager manager;

        public virtual Page Initialize(NormalSceneManager controller) {
            this.manager = controller;

            return this;
        }

        public virtual void Exit()
        {
            manager.Exit();
        }
    }

}
