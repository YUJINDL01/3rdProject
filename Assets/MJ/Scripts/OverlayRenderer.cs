using System;
using UnityEngine;

public class OverlayRenderer : MonoBehaviour
{
    private Texture2D overlayTexture;

    public void InitializeOverlay(int width, int height)
    {
        overlayTexture = new Texture2D(width, height); // 주어진 크기만큼 새로운 캔버스 만들기
        ClearOverlay();
    }

    // frame 웹캠 화면, 손 위치 data
    public void UpdateOverlay(Texture2D frame, HandData data) // 웹캠 화면에 손 위치 같은 정보를 그려주는 역할
    {
        if (overlayTexture != null)
        {
            ClearOverlay(); // 기존 내용을 지우고 새롭게 업데이트합니다.
            overlayTexture.SetPixels(frame.GetPixels()); // 웹캠 화면을 캔버스에 복사합니다.
            DrawHandPosition(data); // 손의 위치를 캔버스에 그립니다.
            overlayTexture.Apply(); // 모든 변경 사항을 캔버스에 적용합니다.
        }
    }

    private void DrawHandPosition(HandData data) // 손위치를 화면에 그림
    {
        Color color = Color.red;
        int radius = 10; // 손이 그려질 크기, 손 위치를 중심으로 10픽셀정도

        for (int x = (int)data.x - radius; x < (int)data.x + radius; x++) // 손의 위치를 중심으로 그려라!!
        {
            for (int y = (int)data.y - radius; y < (int)data.y + radius; y++)
            {
                if (x >= 0 && x < overlayTexture.width && y >= overlayTexture.height)
                {
                    overlayTexture.SetPixel(x, y, color);
                }
            }
        }

        overlayTexture.Apply(); // 변경된 그림을 실제 화면에 적용
    }

    private void ClearOverlay() // 캔버스 비워라
    {
        Color[] clearColors = new Color[overlayTexture.width * overlayTexture.height];
        for (int i = 0; i < clearColors.Length; i++)
        {
            clearColors[i] = Color.clear; // 모든 픽셀을 투명하게 설정
        }
        overlayTexture.SetPixels(clearColors);
        overlayTexture.Apply();
    }
    
    public Texture2D GetOverlayTexture() // 그림이 그려진 화면을 가져옴
    {
        return overlayTexture;
    }
}

[Serializable] // 직렬화 , 객체를 바이트 스트림으로 변환하는 과정
public class HandData
{
    public float x;
    public float y;
}
    
//초기화 업데이트 손위치 그리기 캔버스 가져오기