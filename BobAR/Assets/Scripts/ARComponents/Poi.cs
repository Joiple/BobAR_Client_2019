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
        public double longitude, latitude;
        public string id;
        public float altitude;
        public Image thumbnail;
        public TextMeshProUGUI distanceIndicator;
        public double[] cross;
        private MainSceneManager manager;

        public void RefreshPosition() {
            cross = GpsManager.GetDistanceFromCenter(longitude, latitude);
            transform.position = new Vector3((float)cross[0], GpsManager.AltToY(altitude), (float)cross[1]);
        }

        public void LateUpdate() {
            transform.rotation=manager.cam.transform.rotation;
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
            longitude = rest.longitude;
            latitude = rest.latitude;
            altitude = rest.altitude;
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
            distanceIndicator.text = Vector3.Distance(transform.position, Vector3.zero).ToString("F0")+"m";
        }

    }

}
