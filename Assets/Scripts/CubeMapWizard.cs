using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;

public class CubeMapWizard : MonoBehaviour
{
    [SerializeField]
    Camera camera;
    [SerializeField]
    RenderTexture _cubemapRenderTexture;
    [SerializeField]
    RenderTexture _2dRenderTexture;

    public void CreateCubemap() 
    {
        //Create cubemap
        camera.RenderToCubemap(_cubemapRenderTexture);
        _cubemapRenderTexture.ConvertToEquirect(_2dRenderTexture);
        SaveImage(_2dRenderTexture);
    }
    
    void SaveImage(RenderTexture renderTexture)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        
        Texture2D image = new Texture2D(renderTexture.width, renderTexture.height);
        RenderTexture.active = renderTexture;
        image.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        RenderTexture.active = null;

        byte[] imageBytes = image.EncodeToPNG();

        System.IO.File.WriteAllBytes(Application.dataPath + "/Screenshots/" + sceneName + DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".png", imageBytes);
    }
}