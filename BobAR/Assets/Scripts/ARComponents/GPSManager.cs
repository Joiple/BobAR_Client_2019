using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using Debug = DebugWrap.Debug;

namespace ARComponents {

    public class GpsManager : MonoBehaviour {
        [NonSerialized] public static GpsManager instance;
        private LocationService gps;
        public float radius = 6378137;
        public  float initialLat, initialLon, initialAlt;
        public float connectionWait = 5f;
        public List<Poi> pois;
        public bool runningGpsFlag = false;
        public void Awake() {
            instance = this;
        }

        public void Start() {
            StartCoroutine(GpsStart());
        }

        public IEnumerator GpsStart() {
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
            float t = connectionWait;

            while (gps.status == LocationServiceStatus.Initializing && t > 0) {
                t -= Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            if (gps.status == LocationServiceStatus.Failed) {
                Debug.Log("Connection Failed");

                yield break;
            }

            runningGpsFlag = true;
            initialLat = Input.location.lastData.latitude;
            initialLon = Input.location.lastData.longitude;

            yield return GpsUpdate();
        }

        public IEnumerator GpsUpdate() {
            while (gps.status == LocationServiceStatus.Running && runningGpsFlag) {
                initialLon = gps.lastData.longitude;
                initialLat = gps.lastData.latitude;
                initialAlt = gps.lastData.altitude;
                foreach (Poi p in pois) {
                    p.RefreshPosition();
                }

                Debug.Log(gps.lastData.longitude + "/" + gps.lastData.latitude + "/" + gps.lastData.altitude);

                yield return new WaitForSeconds(5f);
            }

            gps.Stop();
        }

        public static float LatToZ(double lat) {
            lat = (lat - instance.initialLat) / 0.00001 * 0.12179047095976932582726898256213;
            double z = lat;

            return (float) z;
        }

        public static double ZToLat(float z) {
            double lat = z;
            lat = lat * 0.00001 / 0.12179047095976932582726898256213 + instance.initialLat;

            return (float) lat;
        }

        public static float AltToY(float alt) {
            return alt - instance.initialAlt;
        }

        public static float YtoAlt(float alt) {
            return alt + instance.initialAlt;
        }

        public static float LonToX(double lon) {
            lon = (lon - instance.initialLon) / 0.000001 * 0.00728553580298947812081345114627;
            double x = lon;

            return (float) x;
        }

        public static double XToLon(float x) {
            double lon = x;
            lon = lon * 0.000001 / 0.00728553580298947812081345114627 + instance.initialLon;
            return (float) lon;
        }
    }

}