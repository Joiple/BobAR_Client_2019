using UnityEngine;

namespace Common.Dummies {
    [CreateAssetMenu(fileName="DummyUser",menuName="Custom/DummyUser")]
    public class DummyUser :ScriptableObject {
        public string key,
                      id,
                      nickname;
    }

}