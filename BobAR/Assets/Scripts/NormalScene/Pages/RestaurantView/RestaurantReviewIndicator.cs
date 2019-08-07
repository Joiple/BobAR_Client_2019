using System.Collections;
using Network;
using Network.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.RestaurantView {

    public class RestaurantReviewIndicator :MonoBehaviour {
        public Image thumbnailImage;

        public TextMeshProUGUI content,
                               following;

        public Image followedIndicator;

        public string id;
        public RestaurantReviewIndicator Initialize(string id="") {
            this.id = id;
            StartCoroutine(InitializeInternal());
            return this;
        }

        public IEnumerator InitializeInternal() {
            string contentString = "";

            for (int i = 0; i < 100; i++) {
                contentString += "내용";
            }

            content.text = contentString;

            yield return null;
        }
    }
}