using System.Collections;
using System.Collections.Generic;
using Common.Dummies;
using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.ReviewDetailView {

    public class ReviewDetailPageManager : Page {
        public Image thumbnailImage,
                     profileImage,
            likeImage;

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
                               reviewTag,
                               likeNumber;

        public Slider tasteSlider,
                      clearanceSlider,
                      kindnessSlider,
                      atmosphereSlider,
                      efficiencySlider;

        public Sprite liked,
                      unLiked;

        public int ThumbnailIndex {
            get => thumbnailIndex;
            set {
                if (value < 0) value = 0;
                if (value >= sprites.Count) value = sprites.Count - 1;
                thumbnailIndex = value;
                thumbnailImage.sprite=sprites[thumbnailIndex];
            }
        }
        private  int thumbnailIndex;

        public List<Sprite> sprites;
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

        public void OnEnable() => StartCoroutine(InitializeInternal());
        private IEnumerator InitializeInternal() {
            //TODO 리뷰 정보 수신
            DummyReview rev = DummyContainer.instance.reviewDB[DataStorage.instance.GetItem<string>(DataStorageKeyset.NextReview)];
            DummyUser user=DummyContainer.instance.userDB[rev.writer.key];

            foreach (DummyImage i in rev.imageKeys) {
                DummyImage img=DummyContainer.instance.imageDB[i.key];
                sprites.Add(Sprite.Create(img.image,new Rect(Vector2.zero,new Vector2(img.image.width,img.image.height)),Vector2.one/2f ));

                yield return null;
            }

            ThumbnailIndex = 0;
            
            DummyImage userProfile = DummyContainer.instance.imageDB[user.profileImage.key];
            id = rev.key;
            restaurantKey = rev.restaurant.key;
            profileImage.sprite = Sprite.Create(userProfile.image,new Rect(Vector2.zero,new Vector2(userProfile.image.width,userProfile.image.height)),Vector2.one/2f );
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
            SetLikeVisual();
            yield return null;
        }

        public void SwipeImage(int diff) => ThumbnailIndex += diff;

        public void SetLikeVisual() {
            DummyReview rev = DummyContainer.instance.reviewDB[id];
            likeNumber.text = (rev.likes + (rev.iLiked ? 1 : 0)).ToString();
            likeImage.sprite = rev.iLiked ? liked : unLiked;
        }

        public void Follow() {
            DummyContainer.instance.reviewDB[id].iLiked = !DummyContainer.instance.reviewDB[id].iLiked;
            SetLikeVisual();
        }
    }

}