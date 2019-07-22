using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationManager : MonoBehaviour {
    private Vector3 pos;
    private Vector3 acc;
    public void FixedUpdate() {
        acc += Input.acceleration-Input.gyro.gravity;
        
    }
    public void OnDrawGizmos() {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(pos, 10f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, acc * 100f);
    }

}

