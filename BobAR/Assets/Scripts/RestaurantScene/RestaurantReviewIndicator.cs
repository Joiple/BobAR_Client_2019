using System.Collections;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RestaurantScene {

    public class RestaurantReviewIndicator :MonoBehaviour {
        public Image thumbnailImage;

        public TextMeshProUGUI content,
                               following;

        public Image FollowedIndicator;

        public Review target;
        public void Initialize(Review review) {
            target = review;
            StartCoroutine(InitializeInternal());

        }

        public IEnumerator InitializeInternal() {
            Client<ImageSet> imgClient = new Client<ImageSet>(target.pictures[0].ToString());
            while (!imgClient.Prepared) yield return null;
            thumbnailImage.sprite = imgClient.Target.sprite;
            content.text = target.content;
            following.text = target.followers.Count.ToString();
        }
    }
}