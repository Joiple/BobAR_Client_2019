using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour {
    private WebCamTexture texture;
    public RawImage Renderer;
    private ContentSizeFitter fit;

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
            Debug.Log("No Camera");

            yield break;
        }

        foreach (WebCamDevice cam in dev) {
            if (!cam.isFrontFacing) {
                texture = new WebCamTexture(cam.name, Screen.height/ 2, Screen.width/ 2, 60);
                texture.name = "DeviceCameraTexture";

                break;
            }
        }

        if (texture == null) {
            Debug.Log("No Back Camera");

            yield break;
        }

        texture.Play();
        Renderer.texture = texture;
    }

    public void Reload() {
        StartCoroutine(ResetTex());
    }

}