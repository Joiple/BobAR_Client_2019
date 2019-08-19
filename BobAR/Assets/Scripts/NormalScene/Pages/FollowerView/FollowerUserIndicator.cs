using System.Collections;
using Common.Dummies;
using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.FollowerView {

    public class FollowerUserIndicator : MonoBehaviour {
        private FollowerPageManager manager;
        private string id;

        public TextMeshProUGUI nickName,
                               reviewNumber,
                               followButtonText;

        public Image profileImage,
                     followButtonImage;

        public Sprite unclickedSprite,
                      clickedSprite;

        public bool following = false;

        public FollowerUserIndicator Initialize(FollowerPageManager followerPageManager, string id = "") {
            this.manager = followerPageManager;
            this.id = id;

            StartCoroutine(LoadAsync());

            return this;
        }

        public IEnumerator LoadAsync() {
            yield return null;
            //TODO 로딩
            DummyUser user = DummyContainer.instance.userDB[id];
            nickName.text = user.nickname;
            reviewNumber.text = $"리뷰 {DummyContainer.instance.CountReviewOfUser(id).Count}개";
            DummyImage img = DummyContainer.instance.imageDB[user.profileImage.key];
            profileImage.sprite = Sprite.Create(img.image, new Rect(Vector2.zero, new Vector2(img.image.width, img.image.height)), Vector2.one / 2f);
            following = user.iFollowing;
            SetFollowVisual(following);
        }

        public void Click() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextUser, id);
            manager.manager.AddPage(PageType.UserPage);
        }

        public void ClickFollow() {
            following = !following;
            SetFollowVisual(following);
            DummyContainer.instance.userDB[id].iFollowing = following;
        }

        public void SetFollowVisual(bool status) {
            followButtonText.text = status ? "팔로잉" : "+팔로우";
            followButtonText.color = status ? Color.white : new Color(1f, .525f, 0f);
            followButtonImage.sprite = status ? clickedSprite : unclickedSprite;
        }

        public void OnEnable() {
            if (id != null) {
                following = DummyContainer.instance.userDB[id].iFollowing;
                SetFollowVisual(following);
            }
        }
    }

}