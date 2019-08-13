using System.Collections.Generic;
using UnityEngine;

namespace Common.Dummies {

    [CreateAssetMenu(fileName = "DummyRestaurant", menuName = "Custom/DummyRestaurant")]
    public class DummyRestaurant : ScriptableObject {
        public string key,
                      restaurantName,
                      address,
                      phoneNumber;

        public float longitude,
                     latitude,
                     altitude;

        public List<string> reviewKeys = new List<string>();
    }

}