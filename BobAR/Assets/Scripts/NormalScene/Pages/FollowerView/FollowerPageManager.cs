using System.Collections;
using System.Collections.Generic;
using Common.Dummies;
using DataManagement;
using UnityEngine;

namespace NormalScene.Pages.FollowerView {

    public class FollowerPageManager :Page {
        public Transform listTransform,followerTransform;
        public FollowerUserIndicator prefab;
        public List<FollowerUserIndicator> indicators=new List<FollowerUserIndicator>();
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

            foreach (DummyUser key in user.following) Instantiate(prefab, listTransform).Initialize(this, key.key);
            foreach (DummyUser key in user.followers) Instantiate(prefab, followerTransform).Initialize(this, key.key);
            yield return null;
        }
    }

}