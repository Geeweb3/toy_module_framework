using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PFramework;
using UnityEngine;
using Graphics = UnityEngine.Graphics;

namespace GameMain
{
    public class ScreenShot : MonoBehaviour
    {
        public float value1 = 0.09f;

        public float w = 0.43f;
        //public float h = 0.2f;

        public void CapturePic(string picName)
        {
            float tw = Screen.width * w;
            float th = tw;
            float tv = Screen.height * value1; 
            float x = Screen.width / 2 - tw / 2;
            float y = Screen.height / 2 - tv - th / 2;
            CaptureScreen(FindObjectOfType<Camera>(), new Rect(x, y, tw, th), picName);
        }

        private static Texture2D CaptureScreen(Camera camera, Rect rect,string picName)
        {
            RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 50);
            camera.targetTexture = rt;
            camera.Render();
            RenderTexture.active = rt;
            Texture2D screenShot = new Texture2D((int) rect.width, (int) rect.height, TextureFormat.ARGB32, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();
            camera.targetTexture = null;
            RenderTexture.active = null;
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToJPG();
            string fileName =picName + ".jpg";

            File.WriteAllBytes(Application.persistentDataPath + $"/{fileName}", bytes);
            return screenShot;
        }
    }
}