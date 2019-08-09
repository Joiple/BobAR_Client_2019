using UnityEngine;

namespace NormalScene.Pages {

    public class Page : MonoBehaviour {
        internal NormalSceneManager manager;

        public virtual void Initialize(NormalSceneManager controller) {
            this.manager = controller;
        }

        public virtual void Exit()
        {
            manager.Exit();
        }
    }

}
