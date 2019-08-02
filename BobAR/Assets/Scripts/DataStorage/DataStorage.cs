using System.Collections.Generic;
using UnityEngine;

namespace DataStorage {

    public class DataStorage : MonoBehaviour {
        public static DataStorage Instance;

        private void Start() {
            if (Instance != null) Destroy(Instance);
            Instance = this;
        }

        private Dictionary<string, object> Data = new Dictionary<string, object>();

        public bool AddItem(string key, object input) {
            if (Data.ContainsKey(key)) return false;
            Data.Add(key, input);

            return true;
        }

        public T GetItem<T>(string key) {
            if (!Data.ContainsKey(key)) return default(T);

            return Data[key] is T ? (T) Data[key] : default(T);
        }

        public void Clear() {
            Data.Clear();
        }

        public bool Remove(string key) {
            if (!Data.ContainsKey(key)) return false;
            Data.Remove(key);

            return true;
        }
    }

}