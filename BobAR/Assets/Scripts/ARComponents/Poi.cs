using System.Collections;
using UnityEngine;

namespace ARComponents {

    public class Poi : MonoBehaviour {
        public double longitude, latitude;
        public float altitude;

        public void RefreshPosition() {
            transform.position = new Vector3(GPSManager.LonToX(longitude), GPSManager.AltToY(altitude), GPSManager.LatToZ(latitude));
        }

    }

}
