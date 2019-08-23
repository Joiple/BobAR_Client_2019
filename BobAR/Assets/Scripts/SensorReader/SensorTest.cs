using UnityEngine;
using System.Collections;
using SensorReader;
using TMPro;

public class SensorTest : MonoBehaviour
{
    public TextMeshProUGUI view;
    // Use this for initialization
    void Start()
    {
        SensorReaderLibWrapper.Initialize();
        SensorReaderLibWrapper.Start(SensorType.Light);
    }

    // Update is called once per frame
    void Update()
    {
        view.text =$"{SensorReaderLibWrapper.Get(SensorType.Light, 0):F5}";
    }

    void OnDestroy()
    {
        SensorReaderLibWrapper.End(SensorType.Light);
    }
}
