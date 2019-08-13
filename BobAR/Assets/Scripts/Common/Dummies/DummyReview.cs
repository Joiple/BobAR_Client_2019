using System.Collections.Generic;
using UnityEngine;

namespace Common.Dummies {

    [CreateAssetMenu(fileName = "DummyReview", menuName = "Custom/DummyReview")]
    public class DummyReview : ScriptableObject {
        public string key,
                      writerKey,
                      restaurantKey,
                      date,
                      content;

        public int taste,
                   clearance,
                   kindness,
                   atmosphere,
                   efficiency;

        public List<string> imageKeys=new List<string>();
    }

}