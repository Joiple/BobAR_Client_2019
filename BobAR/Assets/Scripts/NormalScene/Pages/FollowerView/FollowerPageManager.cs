using System.Collections;
using System.Collections.Generic;
using Common.Dummies;
using DataManagement;
using TMPro;
using UnityEngine;

namespace NormalScene.Pages.FollowerView {

    public class FollowerPageManager :Page {
        public Transform followingTransform,followerTransform;
        public FollowerUserIndicator prefab;
        public List<FollowerUserIndicator> indicators=new List<FollowerUserIndicator>();

        public TabButton followerButtonText,
                               followingButtonText;
        public string id;
        public override Page Initialize(NormalSceneManager controller) {
            base.Initialize(controller);
            this.id = DataStorage.instance.GetItem<string>(DataStorageKeyset.NextUser);
            StartCoroutine(LoadAsync());
            
            return this;
        }

        private IEnumerator LoadAsync() {
            indicators.Clear();
            //TODO 정보 수신
            DummyUser user = DummyContainer.instance.userDB[id];

            foreach (DummyUser key in user.following) Instantiate(prefab, followingTransform).Initialize(this, key.key);
            foreach (DummyUser key in user.followers) Instantiate(prefab, followerTransform).Initialize(this, key.key);
            yield return null;
            ChangeTab(DataStorage.instance.GetItem<bool>(DataStorageKeyset.FollowStatus));
        }

        public Color activated,
                     deactivated;

        public void ChangeTab(bool isFollower) {
            followerButtonText.ChangeVisual(isFollower);
            followingButtonText.ChangeVisual(!isFollower);
            
            (followerTransform.parent.parent).gameObject.SetActive(isFollower);
            (followingTransform.parent.parent).gameObject.SetActive(!isFollower);
        }
    }

}