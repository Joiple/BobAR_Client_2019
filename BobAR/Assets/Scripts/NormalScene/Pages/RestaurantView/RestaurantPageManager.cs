using System.Collections;
using System.Collections.Generic;
using DataManagement;
using Network;
using TMPro;
using UnityEngine.UI;

namespace NormalScene.Pages.RestaurantView {

    public class RestaurantPageManager : Page{
        public Image previewImage;
        public RestaurantReviewIndicator indicatorPrefab;
        public TextMeshProUGUI restaurantName,
                               address,
                               phoneNumber;

        public List<RestaurantReviewIndicator> indicators=new List<RestaurantReviewIndicator>();
        //리뷰 추가
        public override Page Initialize(NormalSceneManager controller) {
            base.Initialize(controller);
            StartCoroutine(InternalStart());

            return this;
        }

        public IEnumerator InternalStart() {
            Key key=DataStorage.instance.GetItem<Key>(DataStorageKeyset.NextRestaurant);
            
            restaurantName.text = "가게 이름";
            address.text = "가게 주소";
            phoneNumber.text = "010-1111-2222";

            for (int i = 0; i < 8; i++) {
                AddReview("");
            }
            yield return null;
        }
        
        private void AddReview(string id) {
            
            RestaurantReviewIndicator temp=Instantiate(indicatorPrefab).Initialize(this,id);//TODO 레이아웃 포지션 지정
            indicators.Add(temp);
        }

        public void WriteReview()
        {
            manager.AddPage(PageType.ReviewWritePage);
        }
    }

}
