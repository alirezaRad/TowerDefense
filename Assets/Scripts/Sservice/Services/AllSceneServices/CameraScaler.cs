using System;
using Sservice;
using UnityEngine;

namespace Service
{
    public class CameraScaler : MonoBehaviour,IService
    {
        public void FitCameraToScreen(float targetWidth, float targetHeight, float baseOrthographicSize)
        {
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = targetWidth / targetHeight;
            float differenceInSize = targetRatio / screenRatio;
            float newSize = baseOrthographicSize * differenceInSize;
            Camera.main.orthographicSize = newSize;
        }


        public void Load()
        {
        }
    }
}