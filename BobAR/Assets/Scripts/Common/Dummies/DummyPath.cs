using System;
using ARComponents;
using UnityEngine;

namespace Common.Dummies {
    [CreateAssetMenu(fileName = "DummyPath", menuName = "Custom/DummyPath")]
    public class DummyPath :ScriptableObject{
        public Coordinate[] coords;
        public string key;
    }

}