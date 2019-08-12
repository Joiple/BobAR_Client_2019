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

        public Image[] tasteStars = new Image[5],
                       clearanceStars= new Image[5],
                       kindnessStars = new Image[5],
                       atmosphereStars= new Image[5],
                       efficiencyStars= new Image[5];
        public int taste,
            clearance,
            kindness,
            atmosphere,
            efficiency;

        public TextMeshProUGUI tasteValue,
                               clearanceValue,
                               kindnessValue,
                               atmosphereValue,
                               efficiencyValue;

        public void SetTaste(int val)
        {
            taste = val;
            for (int i = 0; i < 5; i++) tasteStars[i].sprite = i <= val ? GoldStar : GrayStar;
            tasteValue.text = taste + "점";
        }

        public void SetClearance(int val)
        {
            clearance = val;
            for (int i = 0; i < 5; i++) clearanceStars[i].sprite = i <= val ? GoldStar : GrayStar;
            clearanceValue.text = clearance+ "점";
        }

        public void SetKindness(int val)
        {
            kindness = val;
            for (int i = 0; i < 5; i++) kindnessStars[i].sprite = i <= val ? GoldStar : GrayStar;
            kindnessValue.text = kindness+"점";
        }

        public void SetAtmosphere(int val)
        {
            atmosphere = val;
            for (int i = 0; i < 5; i++) atmosphereStars[ i].sprite = i <= val ? GoldStar : GrayStar;
            atmosphereValue.text = atmosphere+ "점";
        }

        public void SetEfficiency(int val)
        {
            efficiency = val;
            for (int i = 0; i < 5; i++) efficiencyStars[ i].sprite = i <= val ? GoldStar : GrayStar;
            efficiencyValue.text = efficiency+ "점";
        }

        public override void Initialize(NormalSceneManager controller)
        {
            base.Initialize(controller);
            restaurantName.text = "가게 이름";
        }

        public void WriteReview()
        {
            int count = 0;
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