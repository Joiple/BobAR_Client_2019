using Common.Dummies;
using CustomSceneManagement;
using DataManagement;
using NormalScene;
using TMPro;
using UnityEngine;

namespace MainScene.SearchPages {

    public class ReviewSearchIndicator : MonoBehaviour {

        public TextMeshProUGUI restaurantName,
                               restaurantAddress;

        public string restaurantId;
        public ReviewSearchIndicator Initialize(string id="") {
            restaurantId = id;
            DummyRestaurant rest = DummyContainer.instance.restaurantDB[id];
            this.restaurantName.text = rest.restaurantName;
            this.restaurantAddress.text = rest.address;

            return this;
        }

        public void Clicked() {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.ReviewWritePage);
            CustomSceneManager.instance.LoadScene(1);

        }
    }

}
