using System.Collections.Generic;
using System.Linq;
using ARComponents;
using Common.Dummies;
using DataManagement;
using TMPro;
using UnityEngine;

namespace MainScene {

    public class PathDrawer : MonoBehaviour {
        public List<Coordinate> coords=new List<Coordinate>();
        private double[] cross;
        private LineRenderer liner;
        public void RefreshPosition() {
            List<Vector3> points = new List<Vector3>();
            foreach (Coordinate coord in coords) {
                cross = GpsManager.GetDistanceFromCenter(coord.longitude, coord.latitude);
                points.Add(new Vector3((float) cross[1], GpsManager.AltToY(coord.altitude), -(float) cross[0]));
            }
            if (points.Count == 0) return;
            //가장 가까운 길 찾기
            int closest = -1;
            float closestDist = float.MaxValue;
            Vector3 closestPoint = Vector3.zero;
            for (int i = 0; i < points.Count - 1; i++) {
                Vector3 tmpPos=FindNearestPointOnLine(points[i], points[i + 1], Vector3.zero);
                if (tmpPos.magnitude < closestDist) {
                    closestPoint = tmpPos;
                    closest = i;
                    closestDist = tmpPos.magnitude;
                }
            }
            //가장 가까운 길 이전 삭제
            if (closest > 0)
                points.RemoveRange(0,closest);

            //현위치, 최근접점위치 추가
            points.Insert(0,closestPoint);
            points.Insert(0, Vector3.zero);

            //그리기
            liner.positionCount = points.Count;
            int k = 0;
            foreach (Vector3 point in points) {
                liner.SetPosition(k++, point);
            }
        }

        public void Start() {
            liner = GetComponent<LineRenderer>();
            coords.Clear();
            string key = DataStorage.instance.GetItem<string>(DataStorageKeyset.NextDrawPath);
            if (key != null && DataStorage.instance.dummy.pathDB.ContainsKey(key)) {
                DummyPath p = DataStorage.instance.dummy.pathDB[key];
                if (p != null) coords.AddRange(p.coords);
            }

        }
        public void Clear() => coords.Clear();
        private Vector3 FindNearestPointOnLine(Vector3 origin, Vector3 end, Vector3 point)
        {
            //Get heading
            Vector3 heading = (end - origin);
            float magnitudeMax = heading.magnitude;
            heading.Normalize();

            //Do projection from the point but clamp it
            Vector3 lhs = point - origin;
            float dotP = Vector3.Dot(lhs, heading);
            dotP = Mathf.Clamp(dotP, 0f, magnitudeMax);
            return origin + heading * dotP;
        }
    }

}
