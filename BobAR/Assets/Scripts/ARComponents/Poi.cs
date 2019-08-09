using System.Collections;
using CustomSceneManagement;
using DataManagement;
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
        public void RefreshPosition() {
            transform.position = new Vector3(GpsManager.LonToX(longitude), GpsManager.AltToY(altitude), GpsManager.LatToZ(latitude));
        }

        public void Initialize() {
            StartCoroutine(InitializeInternal());
        }

        private IEnumerator InitializeInternal() {
            id = "";
            thumbnail.sprite = null;
            yield return null;

        }

        public void Click() {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.RestaurantPage);
            DataStorage.instance.AddItem(DataStorageKeyset.NextRestaurant, id);
            CustomSceneManager.instance.LoadScene(1);
        }

        public void Update() {
            distanceIndicator.text = Vector3.Distance(transform.position, Vector3.zero)+"";
        }

    }

}
