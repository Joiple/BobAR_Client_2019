using System.Collections;
using System.Collections.Generic;
using DataManagement;
using Network;
using Network.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.UserView {

    public class UserPageManager : Page{
        public TextMeshProUGUI nickname,
                                following,
                                follower,
                                reviewCount;

        public Image profileImage;
        public List<Review> reviews=new List<Review>();
        public List<UserReviewIndicator> reviewIndicators=new List<UserReviewIndicator>();
        public UserReviewIndicator userReviewInidicatorPrefab;
        public Transform reviewGroup;
        private User target;
        public void Start() {
            StartCoroutine(InitialLoad());
        }

        private IEnumerator InitialLoad() {
            Client<User> t = new Client<User>(DataStorage.instance.GetItem<Key>(DataStorage.NextUser).ToString());
            while (!t.prepared) yield return null;
            target = t.Target;
            nickname.text = target.name;
            following.text= target.followings.Count.ToString();
            follower.text = target.followers.Count.ToString();
            reviewCount.text = target.reviewIds.Count.ToString();
            Client<ImageDownloadPacket> tImg=new Client<ImageDownloadPacket>(target.imageKey.ToString());
            while (!tImg.prepared) yield return null;
            profileImage.sprite = tImg.Target.sprite;
            List<Client<Review>> tReviews=new List<Client<Review>>();

            foreach (Key k in target.reviewIds) {
                tReviews.Add(new Client<Review>(k.ToString()));
            }

            foreach (Client<Review> tR in tReviews) {
                while (!tR.prepared) yield return null;
                UserReviewIndicator tRet = Instantiate(userReviewInidicatorPrefab, reviewGroup).Initialize(tR.Target);
                reviews.Add(tR.Target);
                reviewIndicators.Add(tRet);
            }
        }
    }

}
