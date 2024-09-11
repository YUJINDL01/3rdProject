/*
//TCP

using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class UnityServer : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream; // 클라이언트와 데이터를 주고 받는 통로

    public int port = 12345; // 서버가 통신할 포트 번호

    private WebCamTexture webcamTexture;

    private void Start()
    {
        server = new TcpListener(IPAddress.Any, port); // 서버를 만들어서 모든 ip주소와 설정된 포트 번호로 시작
        server.Start(); // 서버 시작!!
        Debug.Log("Server started on port " + port);


        //웹캠시작
        WebCamDevice[] devices = WebCamTexture.devices; // 컴퓨터에 달린 총 카메라 갯수
        if (devices.Length > 0) //카메라가 1대 이상 있다면
        {
            webcamTexture = new WebCamTexture(devices[0].name); //첫번째 있는거 
            webcamTexture.Play(); //켜라

        }
        else
        {
            Debug.LogError("카메라 못 찾았디");
            return;
        }
        //연결 될 때 까지 기다렸다가 연결 되면 On~ 메소드 호출
        server.BeginAcceptTcpClient(new System.AsyncCallback(OnClientConnected), null);
    }

    void OnClientConnected(IAsyncResult ar)
    {
        client = server.EndAcceptTcpClient(ar); // 연결된 클라이언트가 머냐!!
        stream = client.GetStream(); // 클라이언트와 데이터를 주고 받기 위한 스트림 설정
        Debug.Log("Client connected"); // 연결됨????

        // 클라이언트에게 주기적으로 영상 전송
        InvokeRepeating("SendWedcamFrame", 1.0f, 0.1f); // 0.1초마다 전송
    }

    void SendWebcamFrame() // 주기적으로 영상 보내라!!
    {
        if (webcamTexture != null && webcamTexture.isPlaying)//웹캠 작동중임?
        {
            //웹캠 텍스처를 텍스처2d로 변환
            Texture2D frame = new Texture2D(webcamTexture.width, webcamTexture.height); // 웹캠에서 가져온 이미지를 담기위한 새로운 텍스처 == 데이터 표시!
            frame.SetPixels(webcamTexture.GetPixels()); // 웹캠에서 가져온 픽셀 데이터를 텍스처에 설정
            frame.Apply(); //변경 사항을 텍스처에 적용.

            // 이미지 데이터를 jpg형식으로 인코딩 , JPG 형식으로 변환하여 바이트 배열로 만듦
            byte[] imageBytes = frame.EncodeToJPG(); 

            //데이터 크기 먼저 전송 
            byte[] sizeInfo = System.BitConverter.GetBytes(imageBytes.Length);
            stream.Write(sizeInfo, 0, sizeInfo.Length);

            //이미지 데이터 전송
            stream.Write(imageBytes, 0, imageBytes.Length);
            stream.Flush(); // 모든 데이터를 클라이언트에게 보내랏!!!!!
        }
    }

    private void OnDestroy()
    {
        if (webcamTexture != null) webcamTexture.Stop();
        if(stream != null) stream.Close();
        if(client != null) client.Close();
        if(server != null) server.Stop();
    }
    
    //응답처리
    void ReceiveClientResponse()
    {
        byte[] responseBytes = new byte[256]; // 응답받을 바이트 배열 준비
        int bytesRead = stream.Read(responseBytes, 0, responseBytes.Length); // 데이터 읽어와랏!
        string responseJson = System.Text.Encoding.UTF8.GetString(responseBytes, 0, bytesRead); // 받은 데이터 문자열 변환
        
        //JSON 파싱 후 사용
        Debug.Log("Received from client: " + responseJson);
        
        
        //JSON 데이터를 파싱하여 손 위치 정보 추출
        try // 예의가 발생할 수 있는 코드
        {
            var data = JsonUtility.FromJson<Handata>(responseJson);
            UpdateOverlay(data);
        }
        catch (Exception ex) // 예외가 발생했을때 처리할 코드 // 여러개 catch 가능!!
        {
            Debug.LogError("Error parsing client response: " + ex.Message);
        }
    }
}
*/
