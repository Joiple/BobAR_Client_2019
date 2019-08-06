using System;
using System.Collections.Generic;
using UnityEngine;

public class SunshineNativeGalleryHandler : MonoBehaviour
{

    private const string PACKAGE_NAME = "com.SmileSoft.unityplugin";

    //Gallery

    private const string GALLERY_CLASS_NAME = ".GalleryFrag";
    private const string GALLERY_METHOD_TAKE = "OpenGallery";
    private const string GALLERY_METHOD_TAKE_CALLBACK = "OpenGalleryCallback";


    private const string FileProviderName = "com.SmileSoft.unityplugin.ShareProvider_test";

    public delegate void OnGalleryCallbackHandler(bool success, List<String> paths);
    private OnGalleryCallbackHandler _callbackGallery;


    public void OpenGallery(OnGalleryCallbackHandler callback)
    {
#if UNITY_ANDROID
        using (AndroidJavaObject gallery = new AndroidJavaObject(PACKAGE_NAME + GALLERY_CLASS_NAME))
        {
            _callbackGallery = callback;
            gallery.Call(GALLERY_METHOD_TAKE, gameObject.name, GALLERY_METHOD_TAKE_CALLBACK);
        }
#endif
        Debug.Log("This plugin Only worked in android build");
    }


    public void OpenGalleryCallback(string result)
    {

#if UNITY_ANDROID
        Debug.Log("Take Picture Callback | " + "result: " + result);
        if (_callbackGallery != null)
        {
            _callbackGallery.Invoke(!string.IsNullOrEmpty(result), SpilitGalleryPaths(result));

            _callbackGallery = null;
        }
#endif

        Debug.Log("This plugin Only worked in android build");
    }


   List<String> SpilitGalleryPaths(string bigString)
    {

        //Debug.Log("Big String : " + bigString);
        string separator = "<Separate01234>";
        //bigString.Split();

        Debug.Log("p>>" + bigString);
        string[] splitString = bigString.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
        List<string> finalSplitString = new List<string>();
      //  string[] finalSplitString;
        for (int i = 1; i < splitString.Length; i++)
        {
            finalSplitString.Add(splitString[i]);
        }


        return finalSplitString;
    }
}