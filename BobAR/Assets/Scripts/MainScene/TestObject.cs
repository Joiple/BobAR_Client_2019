using System.Collections;
using ARComponents;
using UnityEngine;

namespace MainScene {

    public class TestObject : Poi {
        public MeshRenderer meshRenderer;
        private GPSManager manager;
        public void Awake() {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        

        public void Start() {
            StartCoroutine(InitialGPS());
        }

        public IEnumerator InitialGPS() {
            manager = GPSManager.instance;
            while (!manager.RunningGPSFlag) yield return new WaitForEndOfFrame();
            manager.pois.Add(this);
            longitude = GPSManager.XToLon(transform.position.x);
            latitude = GPSManager.ZToLat(transform.position.z);
            altitude = GPSManager.YtoAlt(transform.position.y);
        }
    }

}
