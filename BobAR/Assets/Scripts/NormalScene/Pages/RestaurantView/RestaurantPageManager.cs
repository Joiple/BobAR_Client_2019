using System.Collections;
using System.Collections.Generic;
using Common.Dummies;
using DataManagement;
using Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.RestaurantView {

    public class RestaurantPageManager : Page{
        public Image previewImage;
        public RestaurantReviewIndicator indicatorPrefab;
        public TextMeshProUGUI restaurantName,
                               address,
                               phoneNumber;

        public Transform listTransform;
        public Slider[] miniStars;
        public string id;
        public List<RestaurantReviewIndicator> indicators=new List<RestaurantReviewIndicator>();
        //리뷰 추가
        public override Page Initialize(NormalSceneManager controller) {
            base.Initialize(controller);
            StartCoroutine(InternalStart());

            return this;
        }

        public IEnumerator InternalStart() {
            id=DataStorage.instance.GetItem<string>(DataStorageKeyset.NextRestaurant);
            //TODO 가게 네트워크
            DummyRestaurant rest = DummyContainer.instance.restaurantDB[id];
            DummyImage img = DummyContainer.instance.imageDB[DummyContainer.instance.reviewDB[rest.reviewKeys[0].key].imageKeys[0].key];
            restaurantName.text = rest.restaurantName;
            address.text = rest.address;
            phoneNumber.text = rest.phoneNumber;
            previewImage.sprite = Sprite.Create(img.image, new Rect(Vector2.zero, new Vector2(img.image.width, img.image.height)), Vector2.one / 2f);
            float totalAvg = 0f;
            for (int i = 0; i < rest.reviewKeys.Count; i++) {
                AddReview(rest.reviewKeys[i].key);
                totalAvg += rest.reviewKeys[i].Avg;
            }

            totalAvg /= rest.reviewKeys.Count;
            yield return null;

            for (int i = 0; i < miniStars.Length; i++) {
                miniStars[i].value = Mathf.Clamp01(totalAvg- i);
            }
        }

        public void OnEnable() {
            foreach(Transform t in listTransform)Destroy(t.gameObject);
            StartCoroutine(InternalStart());
        }
        
        private void AddReview(string id) {
            
            RestaurantReviewIndicator temp=Instantiate(indicatorPrefab,listTransform).Initialize(this,id);
            indicators.Add(temp);
        }

        public void WriteReview() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextRestaurant, id);
            manager.AddPage(PageType.ReviewWritePage);
        }
        public void FindPath() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextDrawPath, id);
            manager.Exit(true);
        }
    }

}
