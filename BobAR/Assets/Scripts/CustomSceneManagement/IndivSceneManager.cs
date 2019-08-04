using UnityEngine;

namespace CustomSceneManagement {

    public class IndivSceneManager :MonoBehaviour{
        public virtual void Start() {
            CustomSceneManager.CurrentManager = this;
        }

        public void OnDestroy() {
            if (CustomSceneManager.CurrentManager.Equals(this)) CustomSceneManager.CurrentManager = null;
        }
    }

}