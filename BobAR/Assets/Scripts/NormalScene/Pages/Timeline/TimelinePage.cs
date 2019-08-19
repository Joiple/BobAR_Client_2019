using System.Collections;
using System.Collections.Generic;
using Common.Dummies;
using UnityEngine;

namespace NormalScene.Pages.Timeline {

    public class TimelinePage :Page {
        public Transform timeLineTransform;
        public TimeLineItem prefab;
        public override Page Initialize(NormalSceneManager controller) {
            base.Initialize(controller);
            StartCoroutine(InitializeInternal());

            return this;
        }

        public void OnEnable() {
            foreach(Transform t in timeLineTransform)Destroy(t.gameObject);
            StartCoroutine(InitializeInternal());
        }
        private IEnumerator InitializeInternal() {
            List<DummyReview> reviews = new List<DummyReview>();
            foreach (DummyReview rev in DummyContainer.instance.reviewDB.Values) {
                reviews.Add(rev);
            }
            reviews.Sort();
            yield return null;
            foreach (DummyReview rev in reviews) {
                Instantiate(prefab, timeLineTransform).Initialize(this,rev);
            }

        }
    }

}