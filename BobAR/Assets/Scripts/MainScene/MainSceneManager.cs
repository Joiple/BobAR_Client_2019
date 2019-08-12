using System.Collections;
using System.Collections.Generic;
using ARComponents;
using CustomSceneManagement;
using DataManagement;
using MainScene.SearchPages;
using Network;
using Network.Data;
using NormalScene;
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

        public void Start() {
            Key nowPos = new Key() {
                type = KeyType.Location,
                longitude = gps.initialLon,
                latitude = gps.initialLat,
                altitude = gps.initialAlt
            };
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

            for (int i = 0; i < searchResults.restaurantNum; i++) {
                Client<ImageDownloadPacket> imageClient=new Client<ImageDownloadPacket>(ImageDownloadPacket.ParsePacket(searchResults.imageFileIds[i]),true);

                while (!imageClient.prepared) yield return null;

                Poi temp = Instantiate(poiPrefab, poiTransform);
                temp.thumbnail.sprite= Sprite.Create(imageClient.Target.tex,temp.thumbnail.GetPixelAdjustedRect(),Vector3.one/2);

            }
        }


        public void OpenFiltering() {
            filteringPage.Open();
        }

        public void OpenReviewing() {
            reviewPage.Open();
        }

        public void ToMyPage() {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.UserPage);
            DataStorage.instance.AddItem(DataStorageKeyset.NextUser, DataStorage.instance.GetItem<Key>(DataStorageKeyset.MyKey));
            CustomSceneManager.instance.LoadScene(1);
        }

        public void ToRestaurantPage(Key key) {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.RestaurantPage);
            DataStorage.instance.AddItem(DataStorageKeyset.NextRestaurant, key);
            CustomSceneManager.instance.LoadScene(1);
        }
    }

}