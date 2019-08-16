using System.Collections;
using System.Collections.Generic;
using ARComponents;
using Common.Dummies;
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
        public Camera cam;
        public GpsManager gps;
        public RestaurantSearchPage filteringPage;
        public ReviewSearchPage reviewPage;
        public Poi poiPrefab;
        public Transform poiTransform;
        public List<Poi> pois;

        public void Start() {
            RefreshSearch();
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
            //TODO 정보 수신
            foreach (DummyRestaurant rest in DummyContainer.instance.restaurantDB.Values) {
                Debug.Log(rest.key);
                Poi temp = Instantiate(poiPrefab, poiTransform).Initialize(this,rest.key);
                pois.Add(temp);
            }

            gps.pois = pois;
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

        public void ToTimeLine() {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.TimelinePage);
            CustomSceneManager.instance.LoadScene(1);
        }

        public void ToRestaurantPage(string key) {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.RestaurantPage);
            DataStorage.instance.AddItem(DataStorageKeyset.NextRestaurant, key);
            CustomSceneManager.instance.LoadScene(1);
        }
    }

}