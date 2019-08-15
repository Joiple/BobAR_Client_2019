using System.Collections;
using System.Collections.Generic;
using Common.Dummies;
using DataManagement;
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

            DummyUser user=DummyContainer.instance.userDB[DataStorage.instance.GetItem<string>(DataStorageKeyset.NextUser)];
            DummyImage img = DummyContainer.instance.imageDB[user.profileImage.key];
            profileImage.sprite = Sprite.Create(img.image, new Rect(Vector2.zero, new Vector2(img.image.width, img.image.height)), Vector2.one / 2f);
            targetId = user.key;
            List<string> reviewKeys = DummyContainer.instance.CountReviewOfUser(user.key);
            nickname.text = user.nickname;
            following.text= user.following.Length.ToString();
            follower.text = user.followers.Length.ToString();
            reviewCount.text = reviewKeys.Count.ToString();
            foreach(UserReviewIndicator t in reviewIndicators)Destroy(t.gameObject);
            reviewIndicators.Clear();
            foreach(string k in reviewKeys) {
                UserReviewIndicator temp = Instantiate(userReviewInidicatorPrefab, reviewGroup).Initialize(this,k);
                reviewIndicators.Add(temp);
            }

            yield return null;
        }

        public void OpenFollower() {
            
            DataStorage.instance.AddItem(DataStorageKeyset.FollowStatus, true);
            DataStorage.instance.AddItem(DataStorageKeyset.NextUser, targetId);
            manager.AddPage(PageType.FollowerPage);
        }

        public void OpenFollowing() {
            DataStorage.instance.AddItem(DataStorageKeyset.FollowStatus, false);
            DataStorage.instance.AddItem(DataStorageKeyset.NextUser, targetId);
            manager.AddPage(PageType.FollowerPage);
        }
    }

}
