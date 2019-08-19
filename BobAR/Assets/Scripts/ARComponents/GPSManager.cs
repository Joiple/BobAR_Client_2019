using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

namespace ARComponents {

    public class GpsManager : MonoBehaviour {
        [NonSerialized] public static GpsManager instance;
        public LocationService gps;
        public float radius = 6378137;

        public double initialLat,
                      initialLon;

        public float initialAlt;

        public double[] centerCross;
        public float connectionWait = 5f;
        public List<Poi> pois;
        public bool runningGpsFlag = false;
        private LatLngUTMConverter converter;

        public void Awake() {
            instance = this;
            converter = new LatLngUTMConverter();
        }

        public void Start() {
            StartCoroutine(GpsStart());
        }

        public IEnumerator GpsStart() {
            if (!Permission.HasUserAuthorizedPermission("android.permission.ACCESS_FINE_LOCATION")) Permission.RequestUserPermission("android.permission.ACCESS_FINE_LOCATION");

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
            while (Application.isEditor || (gps.status == LocationServiceStatus.Running && runningGpsFlag)) {
                if (gps.status == LocationServiceStatus.Running) {
                    initialLon = gps.lastData.longitude;
                    initialLat = gps.lastData.latitude;
                    initialAlt = gps.lastData.altitude;
                }

                centerCross = LonLatToCross(initialLon, initialLat);


                foreach (Poi p in pois) {
                    p.RefreshPosition();
                }

                Debug.Log(initialLon + "/" + initialLat + "/" + initialAlt);

                yield return new WaitForSeconds(5f);
            }

            gps.Stop();
        }

        public static double[] LonLatToCross(double lon, double lat) {
            UTMResult result = instance.converter.convertLatLngToUtm(lat, lon);

            double[] ret = new[] {
                result.Easting, result.Northing
            };

            return ret;
        }

        public static double[] GetDistanceFromCenter(double lon, double lat) {
            double[] tempResult = LonLatToCross(lon, lat);

            tempResult[0] -= instance.centerCross[0];
            tempResult[1] -= instance.centerCross[1];

            return tempResult;
        }

        public static float AltToY(float alt) {
            return alt-25;
        }

        public static float YtoAlt(float alt) {
            return alt + instance.initialAlt;
        }
    }

}