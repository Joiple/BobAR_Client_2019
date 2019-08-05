using System.Collections.Generic;
using DataManagement;
using TMPro;
using UnityEngine.UI;

namespace NormalScene.Pages.ReviewWriteScene
{
    public class ReviewWriteSceneManager:Page
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
    }
}