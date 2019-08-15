using System.Collections;
using Common.Dummies;
using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.ReviewDetailView {

    public class ReviewDetailPageManager : Page {
        public Image thumbnailImage,
                     profileImage;

        public TextMeshProUGUI followingNumber,
                               userName,
                               content,
                               totalAvgScore,
                               tasteScore,
                               clearanceScore,
                               kindnessScore,
                               atmosphereScore,
                               efficiencyScore,
                               date,
                               reviewTag;

        public Slider tasteSlider,
                      clearanceSlider,
                      kindnessSlider,
                      atmosphereSlider,
                      efficiencySlider;

        public string id,restaurantKey;
        public Slider[] starSliders;

        public override Page Initialize(NormalSceneManager controller) {
            base.Initialize(controller);
            StartCoroutine(InitializeInternal());

            return this;
        }

        public void ClickImage() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextRestaurant,restaurantKey );
            manager.AddPage(PageType.RestaurantPage);
        }
        private IEnumerator InitializeInternal() {
            //TODO 리뷰 정보 수신
            DummyReview rev = DummyContainer.instance.reviewDB[DataStorage.instance.GetItem<string>(DataStorageKeyset.NextReview)];
            DummyUser user=DummyContainer.instance.userDB[rev.writer.key];
            DummyImage img=DummyContainer.instance.imageDB[rev.imageKeys[0].key];
            id = rev.key;
            restaurantKey = rev.restaurant.key;
            thumbnailImage.sprite = Sprite.Create(img.image,new Rect(Vector2.zero,new Vector2(img.image.width,img.image.height)),Vector2.one/2f );
            profileImage.sprite = null;
            followingNumber.text = "" + 123;
            userName.text = user.nickname;
            content.text = rev.content;
            float totalScore = 0f;
            totalScore += rev.taste + rev.atmosphere + rev.clearance + rev.efficiency + rev.kindness;
            totalScore /= 5f;
            totalAvgScore.text = totalScore.ToString("F1");
            tasteScore.text = rev.taste+"점";
            clearanceScore.text = rev.clearance+"점";
            kindnessScore.text = rev.kindness+"점";
            atmosphereScore.text = rev.atmosphere+"점";
            efficiencyScore.text = rev.efficiency+"점";
            reviewTag.text = "#모티집 #서면 #밀푀유나베 #서면맛짐";
            date.text = rev.date;
            tasteSlider.value = rev.taste/5f;
            clearanceSlider.value = rev.clearance / 5f;
            kindnessSlider.value = rev.kindness/5f;
            atmosphereSlider.value = rev.atmosphere/5f;
            efficiencySlider.value = rev.efficiency/5f;
            

            for (int i = 0; i < 5; i++) {
                starSliders[i].value = Mathf.Clamp01(totalScore-i);
            }

            yield return null;
        }

        public void Follow() { }
    }

}