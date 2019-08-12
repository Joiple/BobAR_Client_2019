using System.Collections;
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

            nickName.text = "다른사람이름";
            reviewNumber.text = string.Format("리뷰 {0}개", 4);
        }

        public void FollowClicked() {

        }

        public void Click() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextUser, id);
            manager.manager.AddPage(PageType.UserPage);
        }

    }

}