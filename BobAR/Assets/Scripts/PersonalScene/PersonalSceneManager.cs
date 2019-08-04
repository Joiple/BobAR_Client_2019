using System.Collections;
using System.Collections.Generic;
using CustomSceneManagement;
using DataManagement;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PersonalScene {

    public class PersonalSceneManager : IndivSceneManager {
        public TextMeshProUGUI nickname,
                                following,
                                follower,
                                reviewCount;

        public Image profileImage;
        public List<Review> reviews=new List<Review>();
        public List<PersonalReviewIndicator> reviewIndicators=new List<PersonalReviewIndicator>();
        public PersonalReviewIndicator personalReviewInidicatorPrefab;
        public Transform ReviewGroup;
        private User target;
        public override void Start() {
            base.Start();
            StartCoroutine(InitialLoad());
        }

        private IEnumerator InitialLoad() {
            Client<User> t = new Client<User>(DataStorage.Instance.GetItem<Key>("PersonalTarget").ToString());
            while (!t.Prepared) yield return null;
            target = t.Target;
            nickname.text = target.name;
            following.text= target.followings.Count.ToString();
            follower.text = target.followers.Count.ToString();
            reviewCount.text = target.reviewIds.Count.ToString();
            Client<ImageSet> tImg=new Client<ImageSet>(target.imageKey.ToString());
            while (!tImg.Prepared) yield return null;
            profileImage.sprite = tImg.Target.sprite;
            List<Client<Review>> tReviews=new List<Client<Review>>();

            foreach (Key k in target.reviewIds) {
                tReviews.Add(new Client<Review>(k.ToString()));
            }

            foreach (Client<Review> tR in tReviews) {
                while (!tR.Prepared) yield return null;
                PersonalReviewIndicator tRet = Instantiate(personalReviewInidicatorPrefab, ReviewGroup).Initialize(tR.Target);
                reviews.Add(tR.Target);
                reviewIndicators.Add(tRet);
            }
        }
    }

}
