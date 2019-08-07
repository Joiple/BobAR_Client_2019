using System.Collections;
using System.Collections.Generic;
using DataManagement;
using Network;
using Network.Data;
using TMPro;
using UnityEngine.UI;

namespace NormalScene.Pages.RestaurantView {

    public class RestaurantPageManager : Page{
        public Image previewImage;
        public RestaurantReviewIndicator indicatorPrefab;
        public TextMeshProUGUI restaurantName,
                               address,
                               phoneNumber;

        public Restaurant target;
        public List<Review> reviews;
        public override void Initialize(NormalSceneManager controller) {
            base.Initialize(controller);
            StartCoroutine(InternalStart());
        }

        public IEnumerator InternalStart() {
            Key key=DataStorage.instance.GetItem<Key>(DataStorage.NextRestaurant);
            Client<Restaurant> restaurantData = new Client<Restaurant>(key.ToString());

            while (!restaurantData.prepared) yield return null;

            target = restaurantData.Target;
            restaurantName.text = target.name;
            address.text = target.address;
            phoneNumber.text = target.phoneNumber;
            Client<ImageDownloadPacket> prevImage = new Client<ImageDownloadPacket>(target.prevImageKey.ToString());
            while (!prevImage.prepared) yield return null;
            previewImage.sprite = prevImage.Target.sprite;
            List<Client<Review>> reviewClients = new List<Client<Review>>();
            foreach(Key k in target.reviews)
                reviewClients.Add(new Client<Review>(k.ToString()));

            foreach (Client<Review> t in reviewClients) {
                while (!t.prepared) yield return null;
                AddReview(t.Target);
            }
        }

        private void AddReview(Review review) {
            reviews.Add(review);
            RestaurantReviewIndicator temp=Instantiate(indicatorPrefab);//TODO 레이아웃 포지션 지정
            temp.Initialize(review);
        }

        public void WriteReview() {
            //TODO 리뷰쓰기 포개기
        }
    }

}
