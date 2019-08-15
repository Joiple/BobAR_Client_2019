using System;
using System.Collections;
using System.Collections.Generic;
using Common.Dummies;
using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.Timeline {

    public class TimeLineItem : MonoBehaviour {
        public TimelinePage manager;
        public string id,
                      userId,
                      date;

        public Image imageViewer,
                     heart,
                     profileImage;

        public List<Sprite> sprites;

        public Sprite liked,
                      unLiked;

        public int SpriteIndex {
            get => spriteIndex;
            set {
                if (value < 0) value = 0;
                if (value >= sprites.Count) value = sprites.Count - 1;
                spriteIndex = value;
                imageViewer.sprite = sprites[spriteIndex];
            }
        }

        private int spriteIndex = 0;

        public List<string> imageKeys;

        public TextMeshProUGUI dateInd,
                               content,
                               tagInd,
                               likes,
                               userName;

        public void SetLikeVisual(bool status) {
            heart.sprite = status ? liked : unLiked;
            likes.text = (DummyContainer.instance.reviewDB[id].likes + (status ? 1 : 0)).ToString();
        }

        public void Like() {
            bool liked = DummyContainer.instance.reviewDB[id].iLiked = !DummyContainer.instance.reviewDB[id].iLiked;
            SetLikeVisual(liked);
        }

        public void SwipeImage(int input) => SpriteIndex += input;

        public TimeLineItem Initialize(TimelinePage manager,DummyReview rev) {
            this.manager = manager;
            id = rev.key;
            StartCoroutine(InitializeInternal());

            return this;
        }

        public void GoToRestPage() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextRestaurant, DummyContainer.instance.reviewDB[id].restaurant.key);
            manager.manager.AddPage(PageType.RestaurantPage);
        }

        public void GoToUserPage() {
            DataStorage.instance.AddItem(DataStorageKeyset.NextUser, userId);
            manager.manager.AddPage(PageType.UserPage);
        }

        public IEnumerator InitializeInternal() {
            DummyReview rev = DummyContainer.instance.reviewDB[id];

            yield return null;
            userId = rev.writer.key;
            DummyUser user = DummyContainer.instance.userDB[userId];
            userName.text = user.nickname;
            DummyImage imgs = DummyContainer.instance.imageDB[user.profileImage.key];

            yield return null;
            profileImage.sprite = Sprite.Create(imgs.image, new Rect(Vector2.zero, new Vector2(imgs.image.width, imgs.image.height)), Vector2.one / 2f);
            dateInd.text = date = rev.date;
            content.text = rev.content;
            tagInd.text = rev.TagText;

            yield return null;
            SetLikeVisual(rev.iLiked);

            foreach (DummyImage img in rev.imageKeys) {
                sprites.Add(Sprite.Create(img.image, new Rect(Vector2.zero, new Vector2(img.image.width, img.image.height)), Vector2.one / 2f));

                yield return null;
            }

            SpriteIndex = 0;
        }
    }

}