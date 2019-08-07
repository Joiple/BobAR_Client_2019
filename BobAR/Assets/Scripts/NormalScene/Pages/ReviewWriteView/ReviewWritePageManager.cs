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

        public IEnumerator WriteReviewInternal() {
            yield return null;
            
        }
    }
}