using UnityEditor.Experimental.SceneManagement;
using UnityEngine;

namespace NormalScene {

    public class NormalSceneManager : MonoBehaviour {
        public PrefabManager prefabs;

        public void Start() {
            prefabs = GetComponent<PrefabManager>();
        }
    }

}
