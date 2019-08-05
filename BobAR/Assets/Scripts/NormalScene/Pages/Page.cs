using UnityEngine;

namespace NormalScene.Pages {

    public class Page : MonoBehaviour {
        private NormalSceneManager manager;

        public virtual void Initialize(NormalSceneManager controller) {
            this.manager = controller;
        }
    }

}
