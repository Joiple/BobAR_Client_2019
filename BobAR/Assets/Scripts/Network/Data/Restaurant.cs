using System.Collections.Generic;
using ARComponents;
using UnityEngine;

namespace Network.Data {

    public class Restaurant:ILoadable {
        public Key key,prevImageKey;
        public string name;
        public int startTime, endTime;
        public Poi locator;
        public double longitude, latitude;
        public float altitude;
        public string address,phoneNumber;
        public List<Key> reviews=new List<Key>();
        public ILoadable Load(string input) {
            //TODO 식당 패킷 분석
            return this;
        }

        public void InitializeLocator(Poi prefab) {
            locator = Object.Instantiate(prefab);
            locator.Initialize(this);
            locator.SyncWithTarget();
        }
    }

}
