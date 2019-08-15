using System.Collections.Generic;
using UnityEngine;

namespace Common.Dummies {

    [CreateAssetMenu(fileName = "DummyRestaurant", menuName = "Custom/DummyRestaurant")]
    public class DummyRestaurant : ScriptableObject {
        public string key,
                      restaurantName,
                      address,
                      phoneNumber;

        public double longitude,
                      latitude;
        public float altitude;

        public List<DummyReview> reviewKeys = new List<DummyReview>();
    }

}