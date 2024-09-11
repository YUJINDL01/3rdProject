using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LaptopCamera : MonoBehaviour
{
    private WebCamTexture webcamTexture;

    public RawImage rawImage;

    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices; // 혹시나 달린 카메라가 여러가지 일수도 있으니 다 찾기

        if (devices.Length > 0) // 카메라가 1개 이상이냐?
        {
            Debug.Log("웹캠 여깄다잉:" + devices[0].name); 
            webcamTexture = new WebCamTexture(devices[0].name); // 카메라 가져와!@!!

            if (rawImage != null)
            {
                rawImage.texture = webcamTexture;
            }

            //웹캠시작
            webcamTexture.Play(); // 실제 카메라를 켜고 영상을 가져와랏
        }
        else
        {
            Debug.LogError("웹캠 없다잉");
        }

    }

    private void OnDestroy()
    {
        //웹캠 정지 및 해제
        if (webcamTexture != null)
        {
            webcamTexture.Stop();
        }
    }
}

// 현재 프레임을 얻을수 있는 방법이 있어야함

// 서버 구축 안되어있을때 그냥 유니티에 캠 화면 띄우기
