using System.Linq;
using ARComponents;
using UnityEngine;

namespace MainScene {

    public class PathDrawer : MonoBehaviour {
        public Coordinate[] coords;
        private Vector3Int[] locations;
        private double[] cross;
        private LineRenderer liner;
        public void RefreshPosition() {
            int i = 0;
            foreach (Coordinate coord in coords) {
                cross = GpsManager.GetDistanceFromCenter(coord.longitude, coord.latitude);
                liner.SetPosition(i++, new Vector3((float) cross[1], GpsManager.AltToY(coord.altitude), -(float) cross[0]));
            }
        }

        public void Start() {
            liner = GetComponent<LineRenderer>();
            locations = new Vector3Int[20];
            liner.positionCount = 20;
            int x = 0,
                y = 0;
            for(int i=0;i<20;i++) {
                locations[i] = new Vector3Int(x,-1, y);

                do {
                    x += Random.value > .5f ? 5 : -5;
                    y += Random.value > .5f ? 5 : -5;
                } while (locations.Contains(new Vector3Int(x, -1, y)));
                liner.SetPosition(i, locations[i]);
            }
        }
        public void ReInitialize() {

        }
    }

}
