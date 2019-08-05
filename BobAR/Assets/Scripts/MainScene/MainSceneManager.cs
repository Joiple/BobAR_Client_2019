using System.Collections;
using System.Collections.Generic;
using CustomSceneManagement;
using DataManagement;
using Network;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainScene {

    public class MainSceneManager : MonoBehaviour{
        public List<Restaurant> searchResults = new List<Restaurant>();

        
        public void ClearSearchResult() => searchResults.Clear();

        public void Search(string value) {
            StartCoroutine(SearchInternal(value));
        }

        public IEnumerator SearchInternal(string query) {
            Client<RestaurantBundle> searchResult = new Client<RestaurantBundle>(query);

            while (!searchResult.Prepared) yield return null;

            List<Client<Restaurant>> pacakges = new List<Client<Restaurant>>();

            foreach (Key k in searchResult.Target.keys) {
                pacakges.Add(new Client<Restaurant>(k.ToString()));
            }

            foreach (Client<Restaurant> t in pacakges) {
                while (!t.Prepared) yield return null;
                searchResults.Add(t.Target);
                t.Target.InitializeLocator();
            }
        }

        public void ToMyPage() {
            DataStorage.Instance.AddItem(DataStorage.InitialScene, DataStorage.Instance.GetItem<Key>("MyKey"));
            DataStorage.Instance.AddItem(DataStorage.NextUser, DataStorage.Instance.GetItem<Key>("MyKey"));
            CustomSceneManager.Instance.LoadScene(1);
        }

        public void ToRestaurantPage(Key key) {
            DataStorage.Instance.AddItem("RestaurantTarget",key);
            CustomSceneManager.Instance.LoadScene(1);
        }
    }

}
