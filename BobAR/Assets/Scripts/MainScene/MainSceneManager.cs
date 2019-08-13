using System.Collections;
using System.Collections.Generic;
using ARComponents;
using CustomSceneManagement;
using DataManagement;
using MainScene.SearchPages;
using Network;
using Network.Data;
using NormalScene;
using TMPro;
using UnityEngine;

namespace MainScene {

    public class MainSceneManager : MonoBehaviour {
        public SearchRestaurantPacket searchResults;
        public GpsManager gps;
        public RestaurantSearchPage filteringPage;
        public ReviewSearchPage reviewPage;
        public Poi poiPrefab;
        public Transform poiTransform;
        public List<Poi> pois;
        public TMP_InputField searchText;
        public void Start() {
            float longitude = gps.initialLon,
                  latitude = gps.initialLat,
                  altitude = gps.initialAlt;
        }

        public void RefreshSearch() {
            StartCoroutine(RefreshRestaurantAsync());
        }

        private IEnumerator RefreshRestaurantAsync() {
            if (pois != null)
                foreach (Poi t in pois) {
                    Destroy(t.gameObject);
                }

            pois = new List<Poi>();
            searchText.text = filteringPage.searchText.text;
            //TODO 정보 수신
            
            for (int i = 0; i < searchResults.restaurantNum; i++) {
                
                Poi temp = Instantiate(poiPrefab, poiTransform);
                pois.Add(temp);
            }

            yield return null;
        }


        public void OpenFiltering() {
            filteringPage.Open();
        }

        public void OpenReviewing() {
            reviewPage.Open();
        }

        public void ToMyPage() {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.UserPage);
            DataStorage.instance.AddItem(DataStorageKeyset.NextUser, DataStorage.instance.GetItem<string>(DataStorageKeyset.MyKey));
            CustomSceneManager.instance.LoadScene(1);
        }

        public void ToRestaurantPage(string key) {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.RestaurantPage);
            DataStorage.instance.AddItem(DataStorageKeyset.NextRestaurant, key);
            CustomSceneManager.instance.LoadScene(1);
        }
    }

}