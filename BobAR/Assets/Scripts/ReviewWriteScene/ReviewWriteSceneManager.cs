using System.Collections.Generic;
using CustomSceneManagement;
using DataManagement;
using TMPro;
using UnityEngine.UI;

namespace ReviewWriteScene
{
    public class ReviewWriteSceneManager:IndivSceneManager
    {
        public TextMeshProUGUI restaurantName;
        public InputField ContentField;
        public List<ImagePreview> Images;

        public int Taste,
            Clearance,
            Kindness,
            Atmosphere,
            Efficiency;

        public void SetTaste(int val) => Taste = val;
        public void SetClearance(int val) => Clearance = val;
        public void SetKindness(int val) => Kindness= val;
        public void SetAtmosphere(int val) => Atmosphere = val;
        public void SetEfficiency(int val) => Efficiency = val;

        public void Start()
        {
            restaurantName.text=DataStorage.Instance.GetItem<string>("RestaurantName");
        }
    }
}