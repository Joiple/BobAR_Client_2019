using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

namespace ReviewWriteScene
{
    public class ImagePreview:MonoBehaviour
    {
        public Image thumbnail;
        public string path;
        public void DeleteImage()
        {
            Destroy(this);
        }
    }
}