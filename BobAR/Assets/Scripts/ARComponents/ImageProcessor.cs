using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

namespace ARComponents {

    public class ImageProcessor : MonoBehaviour {
        public float threshold, similarity;
        public bool Stopping=true;
        private Color[,] prevValues, nowValues;
        private CameraManager cam;
        private Texture2D tex;
        public RawImage test;
        public Vector2Int samplingSize;

        public void Start() {
            cam = GetComponent<CameraManager>();
            StartCoroutine(Sampling());
        }

        public void LateUpdate() {
            if (tex != null) {
            
            }
        }

        public IEnumerator Sampling() {
            while (cam.Texture == null)
                yield return null;

            while (cam.Texture.width < cam.Texture.requestedWidth || cam.Texture.height < cam.Texture.requestedHeight)
                yield return null;

            while (true) {
                yield return new WaitForSeconds(.05f);
                Debug.Log("Routine Working");

                tex = new Texture2D(cam.Texture.width, cam.Texture.height);
                tex.SetPixels(cam.Texture.GetPixels());
                tex=ScaleTexture(tex, samplingSize.x, samplingSize.y);
                test.texture = tex;
                prevValues = nowValues;
                nowValues = new Color[samplingSize.x, samplingSize.y];
                int looper = 0;
                if (prevValues!=null&&prevValues.Length == nowValues.Length) {
                    float indivPercent = 1f / nowValues.Length;
                    float tmpSim = 0f;

                    for (int i = 0; i < samplingSize.x; i++) {
                        for (int j = 0; j < samplingSize.y; j++) {
                            nowValues[i, j] = tex.GetPixel(i, j);
                            if (ColorDist(prevValues[i, j],nowValues[i, j]) < threshold) tmpSim += indivPercent;
                            looper = (looper + 1) % 1000;
                            if (looper == 999) yield return null;
                        }
                    }

                    similarity = tmpSim;
                    Stopping = similarity > threshold;
                }
                
            }
        }

        public float ColorDist(Color a, Color b) {
            Color t = a - b;
            return new Vector3(t.r,t.g,t.b).magnitude;
        }
        public static Texture2D SampleTexture(WebCamTexture source, Vector2 size) {
            Profiler.BeginSample("IMP : TextureSampling");
            //*** Get All the source pixels
            Color[] aSourceColor = source.GetPixels();
            Vector2 vSourceSize = new Vector2(source.width, source.height);

            //*** Calculate New Size
            float xWidth = size.x;
            float xHeight = size.y;

            //*** Make New
            Texture2D oNewTex = new Texture2D((int)xWidth, (int)xHeight, TextureFormat.RGBA32, false);

            //*** Make destination array
            int xLength = (int)xWidth * (int)xHeight;
            Color[] aColor = new Color[xLength];

            Vector2 vPixelSize = new Vector2(vSourceSize.x / xWidth, vSourceSize.y / xHeight);

            //*** Loop through destination pixels and process
            Vector2 vCenter = new Vector2();
            for (int ii = 0; ii < xLength; ii++) {
                //*** Figure out x&y
                float xX = (float)ii % xWidth;
                float xY = Mathf.Floor((float)ii / xWidth);

                //*** Calculate Center
                vCenter.x = (xX / xWidth) * vSourceSize.x;
                vCenter.y = (xY / xHeight) * vSourceSize.y;

                //*** Average
                //*** Calculate grid around point
                int xXFrom = (int)Mathf.Max(Mathf.Floor(vCenter.x - (vPixelSize.x * 0.5f)), 0);
                int xXTo = (int)Mathf.Min(Mathf.Ceil(vCenter.x + (vPixelSize.x * 0.5f)), vSourceSize.x);
                int xYFrom = (int)Mathf.Max(Mathf.Floor(vCenter.y - (vPixelSize.y * 0.5f)), 0);
                int xYTo = (int)Mathf.Min(Mathf.Ceil(vCenter.y + (vPixelSize.y * 0.5f)), vSourceSize.y);

                //*** Loop and accumulate
                Color oColorTemp = new Color();
                float xGridCount = 0;
                for (int iy = xYFrom; iy < xYTo; iy++) {
                    for (int ix = xXFrom; ix < xXTo; ix++) {

                        //*** Get Color
                        oColorTemp += aSourceColor[(int)(((float)iy * vSourceSize.x) + ix)];

                        //*** Sum
                        xGridCount++;
                    }
                }

                //*** Average Color
                aColor[ii] = oColorTemp / (float)xGridCount;
            }

            //*** Set Pixels
            oNewTex.SetPixels(aColor);
            oNewTex.Apply();
            Profiler.EndSample();
            //*** Return
            return oNewTex;
        }
        Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight) {
            Profiler.BeginSample("IMP : TextureSampling");
            Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, true);
            Color[] rpixels = result.GetPixels(0);
            float incX = (1.0f / (float)targetWidth);
            float incY = (1.0f / (float)targetHeight);
            for (int px = 0; px < rpixels.Length; px++) {
                rpixels[px] = source.GetPixelBilinear(incX * ((float)px % targetWidth), incY * ((float)Mathf.Floor(px / targetWidth)));
            }
            result.SetPixels(rpixels, 0);
            result.Apply();
            Profiler.EndSample();
            return result;
        }


    }

}