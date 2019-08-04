using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RestaurantScene {

    public class RestaruantSceneManager : MonoBehaviour {
        public Image previewImage;

        public TextMeshProUGUI restaurantName,
                               address,
                               phoneNumber;
        public void WriteReview()
        {
            //DataStorage.Instance.AddItem("Restaurant")
        }
    }

}
