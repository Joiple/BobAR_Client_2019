﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class ReviewSearchPage:SearchPage {
        public Transform restaurantListTransform;
        public ReviewSearchIndicator prefab; 
        public List<ReviewSearchIndicator> indicators=new List<ReviewSearchIndicator>();
        public TMP_InputField searchText;
        public void Search() {
            StartCoroutine(SearchInternal(searchText.text));
        }
        public override IEnumerator SearchInternal(string query) {
            foreach (ReviewSearchIndicator t in indicators) Destroy(t);
            indicators.Clear();
            yield return base.SearchInternal(query);

            for(int i=0;i<8;i++) {
                ReviewSearchIndicator temp = Instantiate(prefab, restaurantListTransform).Initialize();
                indicators.Add(temp);
            }
        }
    }

}