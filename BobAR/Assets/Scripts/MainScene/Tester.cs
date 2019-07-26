using System.Collections;
using System.Collections.Generic;
using ARComponents;
using MainScene;
using UnityEngine;

public class Tester : MonoBehaviour {
    public TestObject Prefab;

    public void Start() {
        for (int i = 0; i < 200; i++) {
            TestObject t = Instantiate(Prefab, Random.insideUnitSphere * 10f, Quaternion.identity, transform);
            Vector3 tp = t.transform.position / 10f;
            t.meshRenderer.material.SetColor("_BaseColor",new Color(Mathf.Abs(tp.x), Mathf.Abs(tp.y), Mathf.Abs(tp.z)));
        }
    }
}
