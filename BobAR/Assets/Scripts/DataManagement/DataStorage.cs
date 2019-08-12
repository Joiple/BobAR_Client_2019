using System.Collections.Generic;
using UnityEngine;

namespace DataManagement {

    public enum DataStorageKeyset {
        NextUser,
        NextRestaurant,
        NextReview,
        InitialScene,
        MyKey,
        FollowStatus
    }
    public class DataStorage : MonoBehaviour {
        public static DataStorage instance;

        private void Start() {
            if (instance != null) Destroy(instance);
            instance = this;
        }

        private Dictionary<DataStorageKeyset, object> data = new Dictionary<DataStorageKeyset, object>();

        public bool AddItem(DataStorageKeyset key, object input) {
            if (data.ContainsKey(key)) data[key]=input;
            else data.Add(key, input);

            return true;
        }

        public T GetItem<T>(DataStorageKeyset key) {
            if (!data.ContainsKey(key)) return default(T);

            return data[key] is T ? (T) data[key] : default(T);
        }

        public void Clear() {
            data.Clear();
        }

        public bool Remove(DataStorageKeyset key) {
            if (!data.ContainsKey(key)) return false;
            data.Remove(key);

            return true;
        }
    }

}