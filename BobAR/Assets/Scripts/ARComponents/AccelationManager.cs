using UnityEngine;

namespace ARComponents {

    public class AccelationManager : MonoBehaviour {
        public Vector3 velocity, acc;
        public Transform target;
        public ImageProcessor imgProcessor;

        public void Start() {
            target.transform.position= velocity = Vector3.zero;

        }
        public void Update() {
            acc = Input.gyro.userAcceleration*Time.deltaTime;
            velocity -= acc;

            if (imgProcessor.Stopping)
                velocity = Vector3.zero;

            target.Translate(velocity, Space.Self);
        }

        public void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(target.position,1f);
            Gizmos.DrawRay(target.position,acc*10f);
        }
    }

}