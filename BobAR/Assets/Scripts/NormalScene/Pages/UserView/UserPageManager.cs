using System.Collections;
using System.Collections.Generic;
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
        public List<UserReviewIndicator> reviewIndicators=new List<UserReviewIndicator>();
        public UserReviewIndicator userReviewInidicatorPrefab;
        public Transform reviewGroup;
        public string targetId="";
        public void Start() {
            StartCoroutine(InitialLoad());
        }

        private IEnumerator InitialLoad() {
            
            nickname.text = "내 이름";
            following.text= "123";
            follower.text = "123";
            reviewCount.text = "123";
            foreach(UserReviewIndicator t in reviewIndicators)Destroy(t.gameObject);
            reviewIndicators.Clear();
            for (int i = 0; i < 10; i++) {
                UserReviewIndicator temp = Instantiate(userReviewInidicatorPrefab, reviewGroup).Initialize(this);
                reviewIndicators.Add(temp);
            }

            yield return null;
        }
    }

}
