using System.Collections;
using System.Collections.Generic;
using System.IO;
using DataManagement;
using Network;
using Network.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.ReviewWriteView
{
    public class ReviewWritePageManager:Page
    {
        public TextMeshProUGUI restaurantName;
        public InputField contentField;
        public List<ImagePreview> images;

        public int taste,
            clearance,
            kindness,
            atmosphere,
            efficiency;

        public void SetTaste(int val) => taste = val;
        public void SetClearance(int val) => clearance = val;
        public void SetKindness(int val) => kindness= val;
        public void SetAtmosphere(int val) => atmosphere = val;
        public void SetEfficiency(int val) => efficiency = val;

        public override void Initialize(NormalSceneManager controller)
        {
            base.Initialize(controller);
            restaurantName.text=DataStorage.instance.GetItem<string>("RestaurantName");
        }

        public void WriteReview() {
            StartCoroutine(WriteReviewInternal());
        }

        public IEnumerator WriteReviewInternal(){
            Key reviewKey=new Key() {
                type=KeyType.WritingReview,
                key=DataStorage.instance.GetItem<int>(DataStorage.MyKey)
            };
            List<Client<ImageUpload>> imageUploadList=new List<Client<ImageUpload>>();
            foreach (ImagePreview t in images) {

                Key tKey = new Key() {
                    type = KeyType.WritingImage,
                    data = File.ReadAllBytes(t.path)
                };

                Client<ImageUpload> tU = new Client<ImageUpload>(tKey.ToString());
                imageUploadList.Add(tU);
            }
            reviewKey.siblingKeys=new int[imageUploadList.Count];
            int count = 0;
            foreach (Client<ImageUpload> t in imageUploadList) {
                while(!t.prepared) yield return null;

                if (t.Target != null) {
                    reviewKey.siblingKeys[count] = t.Target.key;
                    count++;
                } else {
                    Debug.LogError("업로드 오류");
                    yield break;
                }
            }

            Client<ReviewUpload> result = new Client<ReviewUpload>(reviewKey.ToString());

            while (!result.prepared) yield return null;

            if (result.Target!=null&&result.Target.result) {
                Debug.Log("리뷰 쓰기 성공");
            } else {
                Debug.LogError("리뷰 쓰기 실패");
            }
        }
    }
}