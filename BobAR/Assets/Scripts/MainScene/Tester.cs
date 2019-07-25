using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour {
    public MeshRenderer Prefab;

    public void Start() {
        for (int i = 0; i < 200; i++) {
            MeshRenderer t = Instantiate(Prefab, Random.insideUnitSphere * 10f, Quaternion.identity, transform);
            Vector3 tp = t.transform.position / 10f;
            t.material.SetColor("_BaseColor",new Color(Mathf.Abs(tp.x), Mathf.Abs(tp.y), Mathf.Abs(tp.z)));
        }
    }
}
