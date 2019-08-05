using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.RestaurantView {

    public class RestaruantPageManager : MonoBehaviour {
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
