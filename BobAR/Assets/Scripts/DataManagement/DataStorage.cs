using System.Collections.Generic;
using Common.Dummies;
using UnityEngine;

namespace DataManagement {

    public enum DataStorageKeyset {
        NextUser,
        NextRestaurant,
        NextReview,
        InitialScene,
        MyKey,
        FollowStatus,
        WritingStatus
    }
    public class DataStorage : MonoBehaviour {
        public static DataStorage instance;
        public string MyKey;
        public DummyContainer dummy;

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

        public void Initialize() {
            if (instance != null) Destroy(instance);
            instance = this;
            dummy.Initialize();
            AddItem(DataStorageKeyset.MyKey, MyKey);
        }
    }

}