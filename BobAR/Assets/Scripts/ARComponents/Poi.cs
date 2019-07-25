using UnityEngine;

namespace ARComponents {

    public class Poi : MonoBehaviour {
        public float longitude, latitude, altitude;

        public void RefreshPosition() {
            transform.position = new Vector3(GPSManager.LonToX(longitude), GPSManager.AltToY(altitude), GPSManager.LatToZ(latitude));
        }
    }

}
