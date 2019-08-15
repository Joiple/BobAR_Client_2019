using System.Collections;
using Common.Dummies;
using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.UserView {

    public class UserReviewIndicator : MonoBehaviour {
        public UserPageManager manager;
        public Image profileImage;
        public TextMeshProUGUI restaurantName;
        public string id;
        public UserReviewIndicator Initialize(UserPageManager manager,string inputId="") {
            this.manager = manager;
            id=inputId;
            StartCoroutine(LoadInternal());
            return this;
        }

        private IEnumerator LoadInternal() {
            DummyReview rev = DummyContainer.instance.reviewDB[id];
            DummyImage img = DummyContainer.instance.imageDB[rev.imageKeys[0].key];
            DummyRestaurant rest = DummyContainer.instance.restaurantDB[rev.restaurant.key];
            profileImage.sprite=Sprite.Create(img.image,new Rect(Vector2.zero,new Vector2(img.image.width,img.image.height)),Vector2.one/2f );
            restaurantName.text = rest.restaurantName;
            yield return null;
        }

        public void Click() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextReview, id);
            manager.manager.AddPage(PageType.ReviewDetailPage);
        }
    }

}