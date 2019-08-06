
using System.Collections;
using System.Collections.Generic;
using ARComponents;
using CustomSceneManagement;
using DataManagement;
using Network;
using Network.Data;
using NormalScene;
using UnityEngine;

namespace MainScene {

    public class MainSceneManager : MonoBehaviour{
        public List<Restaurant> searchResults = new List<Restaurant>();
        public GpsManager gps;
        public void Start() {
            Key nowPos=new Key() {
                type=KeyType.Location,
                longitude = gps.initialLon,
                latitude= gps.initialLat,
                altitude = gps.initialAlt
            };

        }
        public void ClearSearchResult() => searchResults.Clear();

        public void Search(string value) {
            //TODO 검색
            StartCoroutine(SearchInternal(value));
        }

        public IEnumerator SearchInternal(string query) {
            Client<RestaurantBundle> searchResult = new Client<RestaurantBundle>(query);

            while (!searchResult.prepared) yield return null;

            List<Client<Restaurant>> pacakges = new List<Client<Restaurant>>();

            foreach (Key k in searchResult.Target.keys) {
                pacakges.Add(new Client<Restaurant>(k.ToString()));
            }

            foreach (Client<Restaurant> t in pacakges) {
                while (!t.prepared) yield return null;
                searchResults.Add(t.Target);
//                t.Target.InitializeLocator();
            }
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
