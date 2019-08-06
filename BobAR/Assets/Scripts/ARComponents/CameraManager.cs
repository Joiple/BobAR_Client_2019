using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Debug = DebugWrap.Debug;

namespace ARComponents {

    public class CameraManager : MonoBehaviour {
        private WebCamTexture texture;

        public WebCamTexture Texture => texture;
        public RawImage camRenderer;

        public void Start() {
            StartCoroutine(Auth());
        }

        public IEnumerator Auth() {
            if (!Application.HasUserAuthorization(UserAuthorization.WebCam)) {
                yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

                if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
                    Application.Quit();
            }
        }


        public void OnEnable() {
            StartCoroutine(ResetTex());
        }

        public IEnumerator ResetTex() {
            if (texture != null) yield break;

            yield return new WaitForEndOfFrame();
            WebCamDevice[] dev = WebCamTexture.devices;

            if (dev.Length == 0) {
                DebugWrap.Debug.Log("No Camera");

                yield break;
            }

            foreach (WebCamDevice cam in dev) {
                if (!cam.isFrontFacing) {
                    texture = new WebCamTexture(cam.name, 1280,720, 60);
                    texture.name = "DeviceCameraTexture";
                    
                    break;
                }
            }

            if (texture == null) {
                Debug.Log("No Back Camera");

                yield break;
            }

            texture.Play();
            camRenderer.texture = texture;
        }

        public void Reload() {
            StartCoroutine(ResetTex());
        }

    }

}