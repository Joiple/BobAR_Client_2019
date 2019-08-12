using System.Collections;
using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.RestaurantView {

    public class RestaurantReviewIndicator :MonoBehaviour {
        public Image thumbnailImage;
        public RestaurantPageManager manager;
        public TextMeshProUGUI content,
                               following;

        public Image followedIndicator;

        public string id;
        public RestaurantReviewIndicator Initialize(RestaurantPageManager restaurantPageManager, string id = "") {
            manager = restaurantPageManager;
            this.id = id;
            StartCoroutine(InitializeInternal());
            return this;
        }

        public IEnumerator InitializeInternal() {
            //TODO 리뷰 내용 수신
            string contentString = "";

            for (int i = 0; i < 100; i++) {
                contentString += "내용";
            }

            content.text = contentString;

            yield return null;
        }

        public void Click() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextReview, id);
            manager.manager.AddPage(PageType.ReviewDetailPage);
        }

        public void Follow() {

        }
    }
}