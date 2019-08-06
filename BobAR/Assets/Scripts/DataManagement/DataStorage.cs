using System.Collections.Generic;
using UnityEngine;

namespace DataManagement {

    public class DataStorage : MonoBehaviour {
        public static DataStorage instance;

        public const string NextUser = "NextUser",
                            NextRestaurant = "NextRestaurant",
                            NextReview = "NextReview",
                            InitialScene = "InitialScene",
                            MyKey = "MyKey";

        private void Start() {
            if (instance != null) Destroy(instance);
            instance = this;
        }

        private Dictionary<string, object> data = new Dictionary<string, object>();

        public bool AddItem(string key, object input) {
            if (data.ContainsKey(key)) return false;
            data.Add(key, input);

            return true;
        }

        public T GetItem<T>(string key) {
            if (!data.ContainsKey(key)) return default(T);

            return data[key] is T ? (T) data[key] : default(T);
        }

        public void Clear() {
            data.Clear();
        }

        public bool Remove(string key) {
            if (!data.ContainsKey(key)) return false;
            data.Remove(key);

            return true;
        }
    }

}