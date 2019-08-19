using System.Collections;
using System.Collections.Generic;
using ARComponents;
using TMPro;
using UnityEngine;

public class CoordIndicator : MonoBehaviour {
    public TextMeshProUGUI text;

    public GpsManager manager;
    // Update is called once per frame
    void LateUpdate() {
            text.text = $"{manager.initialLat:F3}/{manager.initialLon:F3}/{manager.initialAlt:F3}";
    }
}