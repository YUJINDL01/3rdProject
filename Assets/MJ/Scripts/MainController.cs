using System;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public WebcamController webcamController;
    public TCPServer tcpServer;
    public OverlayRenderer overlayRenderer;

    private void Start()
    {
        // 서버 시작
        tcpServer.StartServer();
        tcpServer.OnClientConnected += OnClientConnected;
        tcpServer.OnDataReceived += OnDataReceived;

        // 웹캠 시작
        webcamController.StartWebcam();
    }

    private void OnClientConnected()
    {
        InvokeRepeating("SendWebcamFrame", 1.0f, 0.1f);
    }

    private void SendWebcamFrame()
    {
        Texture2D frame = webcamController.GetWebcamFrame();
        if (frame != null)
        {
            byte[] imageBytes = frame.EncodeToJPG();
            tcpServer.SendData(imageBytes);
        }
    }

    private void OnDataReceived(byte[] responseBytes)
    {
        string responseJson = System.Text.Encoding.UTF8.GetString(responseBytes);
        Debug.Log("Received from client: " + responseJson);

        try
        {
            HandData data = JsonUtility.FromJson<HandData>(responseJson);
            Texture2D frame = webcamController.GetWebcamFrame();
            if (frame != null)
            {
                overlayRenderer.UpdateOverlay(frame, data);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error parsing client response: " + ex.Message);
        }
    }

    private void OnDestroy()
    {
        webcamController.StopWebcam();
        tcpServer.StopServer();
    }
}