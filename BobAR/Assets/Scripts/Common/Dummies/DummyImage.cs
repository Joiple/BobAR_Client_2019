using UnityEngine;

namespace Common.Dummies {
    [CreateAssetMenu(fileName="DummyImage",menuName="Custom/DummyImage")]
    public class DummyImage :ScriptableObject {
        public string key;
        public Texture2D image;
    }

}