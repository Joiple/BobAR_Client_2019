using System.Collections;
using System.Collections.Generic;
using Network;
using Network.Data;
using UnityEngine;

namespace MainScene.SearchPages {

    public class SearchPage : MonoBehaviour {
        public List<Restaurant> searchResults;
        public void Search(string value) {
            //TODO 검색
            StartCoroutine(SearchInternal(value));
        }

        public virtual IEnumerator SearchInternal(string query) {
            Client<RestaurantBundle> searchResult = new Client<RestaurantBundle>(query);

            while (!searchResult.prepared) yield return null;

            List<Client<Restaurant>> pacakges = new List<Client<Restaurant>>();

            foreach (Key k in searchResult.Target.keys) {
                pacakges.Add(new Client<Restaurant>(k.ToString()));
            }

            foreach (Client<Restaurant> t in pacakges) {
                while (!t.prepared) yield return null;
                searchResults.Add(t.Target);
            }
        }
    }

}
