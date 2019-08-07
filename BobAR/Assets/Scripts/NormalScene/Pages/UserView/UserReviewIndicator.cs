using System.Collections;
using Network;
using Network.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.UserView {

    public class UserReviewIndicator : MonoBehaviour {
        public Image profileImage;
        public TextMeshProUGUI restaurantName;
        public string id;
        public UserReviewIndicator Initialize(string inputId="") {
            id=inputId;
            StartCoroutine(LoadInternal());
            return this;
        }

        private IEnumerator LoadInternal() {
            restaurantName.text = "식당 이름";

            yield return null;
        }
    }

}