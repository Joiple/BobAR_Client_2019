using UnityEngine;
using UnityEngine.UI;

namespace NormalScene.Pages.ReviewWriteScene
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