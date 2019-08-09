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
        public ReviewSearchIndicator Initialize(string id="",string name="식당 이름",string address="식당 주소") {
            restaurantId = id;
            this.restaurantName.text = name;
            this.restaurantAddress.text = address;

            return this;
        }

        public void Clicked() {
            DataStorage.instance.AddItem(DataStorageKeyset.InitialScene, PageType.ReviewWritePage);
            CustomSceneManager.instance.LoadScene(1);

        }
    }

}
