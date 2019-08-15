using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.ReviewWriteView
{
    public class ImagePreview:MonoBehaviour
    {
        public RawImage thumbnail;
        public string path;

        public ImagePreview Initialize(string path) {
            this.path = path;
            Texture2D f = new Texture2D(2, 2);
            f.LoadImage(File.ReadAllBytes(path));
            thumbnail.texture = f;

            return this;
        }
        public void DeleteImage()
        {
            Destroy(this);
        }
    }
}