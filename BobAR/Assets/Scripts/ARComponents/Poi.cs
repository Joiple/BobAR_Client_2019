using Network.Data;
using UnityEngine;
using UnityEngine.UI;

namespace ARComponents {

    public class Poi : MonoBehaviour {
        public double longitude, latitude;
        public float altitude;
        public Image thumbnail;

        public void RefreshPosition() {
            transform.position = new Vector3(GpsManager.LonToX(longitude), GpsManager.AltToY(altitude), GpsManager.LatToZ(latitude));
        }

    }

}
