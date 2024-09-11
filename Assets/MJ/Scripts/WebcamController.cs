using UnityEngine;

public class WebcamController : MonoBehaviour
{
   private WebCamTexture webcamTexture;

   public void StartWebcam()
   {
      WebCamDevice[] devices = WebCamTexture.devices; // 여러대의 카메라가 있을수도 있으니 찾아봐
      if (devices.Length > 0) // 카메라가 1대 이상이면
      {
         webcamTexture = new WebCamTexture(devices[0].name);  // 카메라 이름이 머야?
         webcamTexture.Play(); //켜라!
      }
      else
      {
         Debug.LogError("카메라 없지롱");
      }
   }

   public Texture2D GetWebcamFrame() // 웹캠이 보여주고 있는 화면은 IMAGE으로 만들어서 보여줌 
   {
      if (webcamTexture != null && webcamTexture.isPlaying) // 웹캠 작동중임?
      {
         Texture2D frame = new Texture2D(webcamTexture.width, webcamTexture.height); // 웹캠에서 가져온 이미지를 담기위한 새로운 텍스처 = 데이터 표시!
         frame.SetPixels(webcamTexture.GetPixels()); // 웹캠에서 가져온 픽셀 데이터를 텍스처에 설정
         frame.Apply(); 
         return frame; // 데이터 돌려줌
      }

      return null;
   }

   public void StopWebcam()
   {
      if (webcamTexture != null) // 웹캠 없으면 꺼라!
      {
         webcamTexture.Stop();
      }
      
   }
}
