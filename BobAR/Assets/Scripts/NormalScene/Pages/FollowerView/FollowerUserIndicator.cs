using System.Collections;
using Common.Dummies;
using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.FollowerView {

    public class FollowerUserIndicator :MonoBehaviour {
        private FollowerPageManager manager;
        private string id;

        public TextMeshProUGUI nickName,
                               reviewNumber;

        public Image profileImage;
        public FollowerUserIndicator Initialize(FollowerPageManager followerPageManager, string id="") {
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
        }

        public void FollowClicked() {

        }

        public void Click() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextUser, id);
            manager.manager.AddPage(PageType.UserPage);
        }

    }

}