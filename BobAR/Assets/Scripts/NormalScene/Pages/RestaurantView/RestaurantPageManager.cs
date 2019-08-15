﻿using System.Collections;
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

        public List<RestaurantReviewIndicator> indicators=new List<RestaurantReviewIndicator>();
        //리뷰 추가
        public override Page Initialize(NormalSceneManager controller) {
            base.Initialize(controller);
            StartCoroutine(InternalStart());

            return this;
        }

        public IEnumerator InternalStart() {
            string key=DataStorage.instance.GetItem<string>(DataStorageKeyset.NextRestaurant);
            //TODO 가게 네트워크
            DummyRestaurant rest = DummyContainer.instance.restaurantDB[key];
            DummyImage img = DummyContainer.instance.imageDB[DummyContainer.instance.reviewDB[rest.reviewKeys[0].key].imageKeys[0].key];
            restaurantName.text = rest.restaurantName;
            address.text = rest.address;
            phoneNumber.text = rest.phoneNumber;
            previewImage.sprite = Sprite.Create(img.image, new Rect(Vector2.zero, new Vector2(img.image.width, img.image.height)), Vector2.one / 2f);
            for (int i = 0; i < rest.reviewKeys.Count; i++) {
                AddReview(rest.reviewKeys[i].key);
            }
            yield return null;
        }
        
        private void AddReview(string id) {
            
            RestaurantReviewIndicator temp=Instantiate(indicatorPrefab,listTransform).Initialize(this,id);
            indicators.Add(temp);
        }

        public void WriteReview()
        {
            manager.AddPage(PageType.ReviewWritePage);
        }
    }

}
