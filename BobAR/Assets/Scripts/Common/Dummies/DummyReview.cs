using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Dummies {

    [CreateAssetMenu(fileName = "DummyReview", menuName = "Custom/DummyReview")]
    public class DummyReview : ScriptableObject, IComparable<DummyReview> {
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
        public List<DummyImage> imageKeys = new List<DummyImage>();
        public bool iLiked;

        public float Avg => (taste + clearance + kindness + atmosphere + efficiency) / 5f;

        public int CompareTo(DummyReview other) {
            return string.Compare(date, other.date, StringComparison.Ordinal);
        }
    }

}