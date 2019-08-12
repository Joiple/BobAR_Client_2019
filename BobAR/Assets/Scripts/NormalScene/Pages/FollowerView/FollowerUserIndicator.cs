using UnityEngine;

namespace NormalScene.Pages.FollowerView {

    public class FollowerUserIndicator :MonoBehaviour {
        private FollowerPageManager manager;
        private string id;
        public FollowerUserIndicator Initialize(FollowerPageManager followerPageManager, string id="") {
            this.manager = followerPageManager;
            this.id = id;
            //TODO 로딩
            return this;
        }
    }

}