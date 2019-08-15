using System.Collections.Generic;
using UnityEngine;

namespace Common.Dummies {

    [CreateAssetMenu(fileName = "DummyReview", menuName = "Custom/DummyReview")]
    public class DummyReview : ScriptableObject {
        public string key,
                      date,
                      content,
            TagText;

        public int taste,
                   clearance,
                   kindness,
                   atmosphere,
                   efficiency,
                   likes;

        public DummyUser writer;
        public DummyRestaurant restaurant;
        public List<DummyImage> imageKeys=new List<DummyImage>();
        public bool iLiked;
    }

}