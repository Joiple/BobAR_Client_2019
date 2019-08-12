using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NormalScene.Pages.FollowerView {

    public class FollowerPageManager :Page {
        public Transform listTransform;
        public FollowerUserIndicator prefab;
        public List<FollowerUserIndicator> indicators=new List<FollowerUserIndicator>();

        public override Page Initialize(NormalSceneManager controller) {
            base.Initialize(controller);
            StartCoroutine(LoadAsync());
            
            return this;
        }

        private IEnumerator LoadAsync() {
            indicators.Clear();
            //TODO 정보 수신
            for (int i = 0; i < 10; i++) {
                indicators.Add(Instantiate(prefab,listTransform).Initialize(this));
            }
            yield return null;
        }
    }

}