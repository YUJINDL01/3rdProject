using System;
using System.Net;
using System.Net.Sockets;
using Unity.VisualScripting; // 소켓이라는 도구를 사용해서 컴퓨터끼리 대화가 가능하게 해줌
using UnityEngine;

public class TCPServer : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream; // 클라이언트와 데이터를 주고 받는 통로
    public int port = 12345; // 서버가 통신할 포트 번호

    public event Action OnClientConnected; // 다른 컴퓨터가 서버를 연결할때 연결됐다! 라고 말함
    public event Action<byte[]> OnDataReceived; // 데이터를 받으면 받았다!! 함

    public void StartServer() // 서버 켜라
    {
        server = new TcpListener(IPAddress.Any, port); // 서버 만들어서 모든 ip주소와 설정된 포트 번호로 시작
        server.Start(); // 서버 시작!!
        Debug.Log("server" + port);
        
        //연결 될 때까지 기다렸다가 연결 되면 On~ 메소드 호출
        server.BeginAcceptTcpClient(new AsyncCallback(OnClientConnectedCallback), null); 
    }

    private void OnClientConnectedCallback(IAsyncResult ar) // 연결되면 실행
    {
        client = server.EndAcceptTcpClient(ar); // 연결된 클라이언트가 머야!!
        stream = client.GetStream(); // 클라이언트와 데이터를 주고 받기 위한 통로 설정
        Debug.Log("Client connected"); //  연결됨???

        OnClientConnected?.Invoke();

        StartListening();
    }

    private void StartListening() //  데이터를 받기 시작함
    {
        byte[] buffer = new byte[256];
        stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnDataReceivedCallback), buffer);
        // 클라이언트가 데이터를 보낼때까지 기다렸다가 데이터를 받고, On~ 메소드를 실행. > 받은 데이터는 buffer에 저장.
    }

    // 응답처리
    public void OnDataReceivedCallback(IAsyncResult ar) // 데이터 받아와!!!
    {
        byte[] buffer = (byte[])ar.AsyncState;
        int bytesRead = stream.EndRead(ar);

        if (bytesRead > 0) // 받은 데이터가 있으면
        {
            byte[] receivedData = new byte[bytesRead];
            Array.Copy(buffer, receivedData, bytesRead);
            OnDataReceived?.Invoke(receivedData);
            
            StartListening(); // 또 다음 데이터 기다림
        }
    }
    
    public void SendData(byte[] data) //  데이터 보냄!!!
    {
        if (stream != null && stream.CanWrite) // 통로가 있고 보낼 수 있냐?
        {
            byte[] sizeInfo = BitConverter.GetBytes(data.Length); // 기본 데이터 타입을 바이트 배열로 변환
            stream.Write(sizeInfo, 0, sizeInfo.Length); // 크기 먼저 전송
            stream.Write(data, 0, data.Length);// 전송할 데이터 배열 전송
            stream.Flush(); // 남아있는 데이터 전송
        }
    }

    public void StopServer() // 서버 꺼라!!
    {
        if (stream != null) stream.Close();
        if (client != null) client.Close();
        if (server != null) server.Stop();

    }
}
