using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using Debug = DebugWrap.Debug;

namespace ARComponents {

    public class GPSManager : MonoBehaviour {
        [NonSerialized] public static GPSManager instance;
        private LocationService gps;
        public float _radius = 6378137;
        private float initialLat, initialLon, initialAlt;
        public float ConnectionWait = 5f;
        public List<Poi> pois;
        public bool RunningGPSFlag = false;

        public void Awake() {
            instance = this;
        }

        public void Start() {
            StartCoroutine(GPSStart());
        }

        public IEnumerator GPSStart() {
            if (!Permission.HasUserAuthorizedPermission("android.permission.ACCESS_FINE_LOCATION"))
                Permission.RequestUserPermission("android.permission.ACCESS_FINE_LOCATION");
            while (!Permission.HasUserAuthorizedPermission("android.permission.ACCESS_FINE_LOCATION")) {
                yield return null;
            }

            gps = Input.location;

            if (!gps.isEnabledByUser) {
                Debug.Log("gps not working now");
            }

            gps.Start(1f);
            float t = ConnectionWait;

            while (gps.status == LocationServiceStatus.Initializing && t > 0) {
                t -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            if (gps.status == LocationServiceStatus.Failed) {
                Debug.Log("Connection Failed");

                yield break;
            }

            RunningGPSFlag = true;
            initialLat = Input.location.lastData.latitude;
            initialLon = Input.location.lastData.longitude;

            yield return GPSUpdate();
        }

        public IEnumerator GPSUpdate() {
            while (gps.status == LocationServiceStatus.Running && RunningGPSFlag) {
                initialLon = gps.lastData.longitude;
                initialLat = gps.lastData.latitude;
                initialAlt = gps.lastData.altitude;

                foreach (Poi p in pois) {
                    p.RefreshPosition();
                }

                Debug.Log(gps.lastData.longitude+"/"+gps.lastData.latitude+"/"+gps.lastData.altitude);

                yield return new WaitForSeconds(5f);
            }

            gps.Stop();
        }

        public static float LatToZ(double lat) {
            lat = (lat - instance.initialLat) / 0.00001 * 0.12179047095976932582726898256213;
            double z = lat;

            return (float) z;
        }

        public static float AltToY(float alt) {
            return alt - instance.initialAlt;
        }

        public static float LonToX(double lon) {
            lon = (lon - instance.initialLon) / 0.000001 * 0.00728553580298947812081345114627;
            double x = lon;

            return (float) x;
        }
    }

}