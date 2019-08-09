using System.Collections;
using System.Collections.Generic;
using DataManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.ReviewWriteView
{
    public class ReviewWritePageManager:Page
    {
        public TextMeshProUGUI restaurantName;
        public TMP_InputField contentField;
        public List<ImagePreview> images;
        public Sprite GrayStar, GoldStar;
        public Image[] StarInputs;
        public Image[,] Stars=new Image[5,5];
        public int taste,
            clearance,
            kindness,
            atmosphere,
            efficiency;

        public void SetTaste(int val)
        {
            taste = val;
            for (int i = 0; i < 5; i++) Stars[0, i].sprite = i <= val ? GoldStar : GrayStar;
        }

        public void SetClearance(int val)
        {
            clearance = val;
            for (int i = 0; i < 5; i++) Stars[1, i].sprite = i <= val ? GoldStar : GrayStar;
        }

        public void SetKindness(int val)
        {
            kindness = val;
            for (int i = 0; i < 5; i++) Stars[2, i].sprite = i <= val ? GoldStar : GrayStar;
        }

        public void SetAtmosphere(int val)
        {
            atmosphere = val;
            for (int i = 0; i < 5; i++) Stars[3, i].sprite = i <= val ? GoldStar : GrayStar;
        }

        public void SetEfficiency(int val)
        {
            efficiency = val;
            for (int i = 0; i < 5; i++) Stars[4, i].sprite = i <= val ? GoldStar : GrayStar;
        }

        public override void Initialize(NormalSceneManager controller)
        {
            base.Initialize(controller);
            restaurantName.text = "가게 이름";
        }

        public void WriteReview()
        {
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Stars[i, j] = StarInputs[count++];
                }
            }
            StartCoroutine(WriteReviewInternal());
        }

        public IEnumerator WriteReviewInternal() {
            yield return null;
            
        }

        public void Click() {
            //TODO 리뷰송신
            Exit();
        }
    }
}