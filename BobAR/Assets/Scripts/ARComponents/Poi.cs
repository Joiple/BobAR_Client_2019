using System.Collections;
using Common.Dummies;
using CustomSceneManagement;
using DataManagement;
using MainScene;
using NormalScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARComponents {

    public class Poi : MonoBehaviour {
        public Coordinate coord;
        public string id;
        public Image thumbnail;
        public TextMeshProUGUI distanceIndicator;
        public double[] cross;
        private MainSceneManager manager;
        public AnimationCurve curve;
        public void RefreshPosition() {
            cross = GpsManager.GetDistanceFromCenter(coord.longitude, coord.latitude);
            transform.position = new Vector3((float)cross[1], GpsManager.AltToY(coord.altitude), -(float)cross[0]);
        }

        public Poi Initialize(MainSceneManager manager,string id="") {
            this.manager = manager;
            GetComponentInChildren<Canvas>().worldCamera = manager.cam;
            this.id = id;
            StartCoroutine(InitializeInternal());
            return this;
        }

        private IEnumerator InitializeInternal() {
            //TODO 서버수신 가게정보
            DummyRestaurant rest = DummyContainer.instance.restaurantDB[id];
            coord.longitude = rest.longitude;
            coord.latitude = rest.latitude;
            coord.altitude = rest.altitude;
            DummyImage img = DummyContainer.instance.imageDB[DummyContainer.instance.GetIdOfProfileImage(rest)];
            thumbnail.sprite = Sprite.Create(img.image, new Rect(Vector2.zero, new Vector2(img.image.width, img.image.height)), Vector2.one / 2f);
            yield return null;

        }

        public void Click() {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.RestaurantPage);
            DataStorage.instance.AddItem(DataStorageKeyset.NextRestaurant, id);
            CustomSceneManager.instance.LoadScene(1);
        }

        public void Update() {
            transform.rotation=manager.cam.transform.rotation;
            float dist = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), Vector3.zero);
            distanceIndicator.text = dist.ToString("F0")+"m";
            transform.localScale = Vector3.one * curve.Evaluate(dist);
        }

    }

}
