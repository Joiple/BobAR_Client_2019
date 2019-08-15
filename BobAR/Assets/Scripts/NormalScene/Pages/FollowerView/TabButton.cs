using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.FollowerView {

    public class TabButton :MonoBehaviour{
        public FollowerPageManager manager;
        public bool isFollower;
        public TextMeshProUGUI text;
        public Image bar;
        public void ChangeTab() {
            
            manager.ChangeTab(isFollower);
        }

        public void ChangeVisual(bool status) {
            text.color = status ? manager.activated : manager.deactivated;
            bar.color = status ? manager.activated : manager.deactivated;
        }
    }

}