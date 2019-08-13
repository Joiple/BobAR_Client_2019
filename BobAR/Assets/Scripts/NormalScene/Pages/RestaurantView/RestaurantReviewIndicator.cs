using System.Collections;
using Common.Dummies;
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
            //TODO 네트워크 리뷰
            DummyReview rev = DummyContainer.instance.reviewDB[id];
            content.text = rev.content.Length>20?rev.content.Substring(0,20)+"...":rev.content;
            DummyImage img = DummyContainer.instance.imageDB[rev.imageKeys[0].key];
            thumbnailImage.sprite = Sprite.Create(img.image,new Rect(Vector2.zero,new Vector2(img.image.width,img.image.height)),Vector2.one/2f );
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