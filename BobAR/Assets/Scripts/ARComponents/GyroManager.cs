using UnityEngine;

namespace ARComponents {

    public class GyroManager : MonoBehaviour {
        public Transform cameraContainer;

        Gyroscope gyro;
        private Quaternion rot = new Quaternion(0, 0, 1, 0);

        public void Start() {
            gyro = Input.gyro;
            cameraContainer.parent.rotation = Quaternion.Euler(90f, -90f, 0f);
        }

        public void Update() {
            Input.gyro.enabled = true;
            cameraContainer.localRotation = gyro.attitude  * rot;
            Debug.Log("Compass : " + Input.compass.trueHeading);
        }

        public void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(cameraContainer.position, cameraContainer.forward);
        }
    }

}