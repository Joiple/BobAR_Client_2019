using System.Collections;
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
            restaurantName.text = "식당 이름";
            yield return null;
        }

        public void Click() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextReview, id);
            manager.manager.AddPage(PageType.ReviewDetailPage);
        }
    }

}