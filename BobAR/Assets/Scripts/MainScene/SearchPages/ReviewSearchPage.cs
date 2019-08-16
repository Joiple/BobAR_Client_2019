using System.Collections;
using System.Collections.Generic;
using Common.Dummies;
using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class ReviewSearchPage:SearchPage {
        public Transform restaurantListTransform;
        public ReviewSearchIndicator prefab; 
        public List<ReviewSearchIndicator> indicators=new List<ReviewSearchIndicator>();
        public TMP_InputField searchText;
        public void Search() {
            foreach (Transform t in restaurantListTransform) {
                Destroy(t.gameObject);
            }
            StartCoroutine(SearchInternal(searchText.text));
        }
        public override IEnumerator SearchInternal(string query) {
            foreach (ReviewSearchIndicator t in indicators) Destroy(t);
            indicators.Clear();
            yield return base.SearchInternal(query);
            LinkedList<KeyValuePair<int,DummyRestaurant>> list = new LinkedList<KeyValuePair<int,DummyRestaurant>>();

            foreach (DummyRestaurant d in DummyContainer.instance.restaurantDB.Values) {
                int index = 0,score=0;

                while (index < d.restaurantName.Length && index < query.Length) {
                    if (d.restaurantName[index] == query[index++]) score++;
                }
                if (list.Count == 0) {
                    list.AddFirst(new KeyValuePair<int, DummyRestaurant>(score, d));
                } else {
                    bool added = false;
                    LinkedListNode<KeyValuePair<int, DummyRestaurant>> t = list.First;
                    for (int i = 0; i < list.Count; i++) {
                        if (t.Value.Key < score) {
                            list.AddBefore(t, new LinkedListNode<KeyValuePair<int, DummyRestaurant>>(new KeyValuePair<int, DummyRestaurant>(score, d)));
                            added = true;

                            break;
                        }
                    }

                    if (!added) list.AddLast(new KeyValuePair<int, DummyRestaurant>(score, d));
                }


                
            }
            foreach(KeyValuePair<int,DummyRestaurant> t in list) {

                ReviewSearchIndicator temp = Instantiate(prefab, restaurantListTransform).Initialize(t.Value.key);
                indicators.Add(temp);
            }
        }
    }

}