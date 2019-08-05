using System.Collections;
using CustomSceneManagement;
using MainScene;
using Network;
using UnityEngine;

namespace ARComponents {

    public class Poi : MonoBehaviour {
        public double longitude, latitude;
        public float altitude;
        public Restaurant target;
        public void RefreshPosition() {
            transform.position = new Vector3(GPSManager.LonToX(longitude), GPSManager.AltToY(altitude), GPSManager.LatToZ(latitude));
        }

        public void Initialize(Restaurant t) {
            target = t;
        }

        public void SyncWithTarget() {
            longitude = target.longitude;
            latitude = target.latitude;
            altitude = target.altitude;
            RefreshPosition();
        }
    }

}
