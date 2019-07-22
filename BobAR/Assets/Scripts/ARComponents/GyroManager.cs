using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroManager : MonoBehaviour {
    public GameObject CameraContainer;
    Gyroscope gyro;
    private Quaternion rot = new Quaternion(0, 0, 1, 0);
    public void Start() {
        gyro = Input.gyro;
        transform.parent.rotation= Quaternion.Euler(90f, -90f, 0f);
    }

    public void Update() {
        Input.gyro.enabled = true;
        transform.localRotation = gyro.attitude*rot;
    }
}