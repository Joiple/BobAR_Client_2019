using ARComponents;
using UnityEngine;

namespace Network {

    public class Restaurant:ILoadable {
        public Key key;
        public string name;
        public int startTime, endTime;
        public Poi locator;
        public Poi Prefab;
        public double longitude, latitude;
        public float altitude;
        public ILoadable Load(string input) {
            //TODO 식당 패킷 분석
            return this;
        }

        public void InitializeLocator() {
            locator = Object.Instantiate(Prefab);
            locator.Initialize(this);
            locator.SyncWithTarget();
        }
    }

}
