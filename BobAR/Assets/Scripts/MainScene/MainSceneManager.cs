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

    public class MainSceneManager : MonoBehaviour{
        public List<Restaurant> searchResults = new List<Restaurant>();
        public GpsManager gps;
        public RestaurantSearchPage filteringPage;
        public ReviewSearchPage reviewPage;

        public void Start() {
            Key nowPos=new Key() {
                type=KeyType.Location,
                longitude = gps.initialLon,
                latitude= gps.initialLat,
                altitude = gps.initialAlt
            };

        }
        public void ClearSearchResult() => searchResults.Clear();

        public void OpenFiltering() {
            filteringPage.Open();
            
        }

        public void OpenReviewing() {
            reviewPage.Open();
        }
        public void ToMyPage() {
            DataStorage.instance.AddItem(DataStorage.InitialScene, PageType.UserPage);
            DataStorage.instance.AddItem(DataStorage.NextUser, DataStorage.instance.GetItem<Key>(DataStorage.MyKey));
            CustomSceneManager.instance.LoadScene(1);
        }

        public void ToRestaurantPage(Key key) {
            DataStorage.instance.AddItem(DataStorage.InitialScene, PageType.RestaurantPage);
            DataStorage.instance.AddItem(DataStorage.NextRestaurant,key);
            CustomSceneManager.instance.LoadScene(1);
        }
    }

}
