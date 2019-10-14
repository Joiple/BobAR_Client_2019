using System.Collections;
using System.Collections.Generic;
using ARComponents;
using Common.Dummies;
using CustomSceneManagement;
using DataManagement;
using MainScene.SearchPages;
using NormalScene;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainScene {

    public class MainSceneManager : MonoBehaviour {
        public Camera cam;
        public GpsManager gps;
        public RestaurantSearchPage filteringPage;
        public ReviewSearchPage reviewPage;
        public Poi poiPrefab;
        public Transform poiTransform;
        public List<Poi> pois = new List<Poi>();
        public TextMeshProUGUI searchText;
        public Button CancelPathFindButton;
        public int baseThreshold = 1;
        private int threshold = 1;
        public void Start() {
            threshold = baseThreshold;
            RefreshSearch();
        }

        public void RefreshSearch() {
            StartCoroutine(RefreshRestaurantAsync());
        }

        private IEnumerator RefreshRestaurantAsync() {
            if (pois.Count > 0)
                foreach (Poi t in pois) {
                    Destroy(t.gameObject);
                }
            CancelPathFindButton.gameObject.SetActive(false);
            string pathKey = DataStorage.instance.GetItem<string>(DataStorageKeyset.NextDrawPath);
            if (pathKey != null && DataStorage.instance.dummy.restaurantDB.ContainsKey(pathKey)) {
                searchText.text = DataStorage.instance.dummy.restaurantDB[pathKey].restaurantName;
                threshold = searchText.text.Length;
                CancelPathFindButton.gameObject.SetActive(true);
            }
                
            pois.Clear();
            //TODO 정보 수신
            foreach (DummyRestaurant rest in DummyContainer.instance.restaurantDB.Values) {
                Debug.Log(rest.key);
                int score = searchText.text.Length > 0 ? 0 : 999;
                Debug.Log("initialScore : " + score);
                foreach (char t in searchText.text)
                foreach (char tt in rest.restaurantName)
                    score += tt == t ? 1 : 0;
                Debug.Log("resultScore : " + score);
                if (score >= threshold) {
                    Poi temp = Instantiate(poiPrefab, poiTransform).Initialize(this, rest.key);
                    pois.Add(temp);
                }
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

        public void CancelPathFinding() {
            threshold = baseThreshold;
            searchText.text = "";
            DataStorage.instance.Remove(DataStorageKeyset.NextDrawPath);
            gps.pathDrawer.Clear();
            CancelPathFindButton.gameObject.SetActive(false);

        }
    }

}