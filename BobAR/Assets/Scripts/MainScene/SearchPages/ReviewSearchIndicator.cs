using CustomSceneManagement;
using DataManagement;
using Network.Data;
using NormalScene;
using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class ReviewSearchIndicator : MonoBehaviour {
        public Restaurant target;

        public TextMeshProUGUI restaurantName,
                               restaurantAddress;
        public ReviewSearchIndicator Initialize(Restaurant input) {
            target = input;
            restaurantName.text = target.name;
            restaurantAddress.text = target.address;

            return this;
        }

        public void Clicked() {
            DataStorage.instance.AddItem(DataStorage.InitialScene, PageType.ReviewWritePage);
            CustomSceneManager.instance.LoadScene(1);

        }
    }

}
