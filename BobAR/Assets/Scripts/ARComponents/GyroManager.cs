using UnityEngine;

namespace ARComponents {

    public class GyroManager : MonoBehaviour {
        public Transform CameraContainer;

        Gyroscope gyro;
        private Quaternion rot = new Quaternion(0, 0, 1, 0);
        public void Start() {
            gyro = Input.gyro;
            CameraContainer.parent.rotation= Quaternion.Euler(90f, -90f, 0f);
        }

        public void Update() {
            Input.gyro.enabled = true;
            CameraContainer.localRotation = gyro.attitude*rot;
        }

        public void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(CameraContainer.position,CameraContainer.forward);
        }
    }

}