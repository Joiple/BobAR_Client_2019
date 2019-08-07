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
        public Review review;
        public UserReviewIndicator Initialize(Review tR) {
            review = tR;
            StartCoroutine(LoadInternal());
            return this;
        }

        private IEnumerator LoadInternal() {
            Client<Restaurant> restaurant=new Client<Restaurant>(review.restaurant.ToString());

            while (!restaurant.prepared) yield return null;
            restaurantName.text = restaurant.Target.name;
            
            Client<ImageDownloadPacket> firstImage=new Client<ImageDownloadPacket>(review.pictures[0].ToString());

            while (!firstImage.prepared) yield return null;
            profileImage.sprite = firstImage.Target.sprite;


        }
    }

}