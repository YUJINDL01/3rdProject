using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json; // JSON 처리용 라이브러리 (Newtonsoft.Json)

public class JHAAI : MonoBehaviour
{
    string aiServerUrl = "https://3b89-221-163-19-142.ngrok-free.app/login"; // AI 서버의 URL


    private void Start()
    {
        StartCoroutine(SendProblemData());
    }

    // AI 서버에 요청하는 함수
    public IEnumerator SendProblemData()
    {
        // AI 서버 URL 지정
        string jsonData = "1";

        string body = JsonConvert.SerializeObject(jsonData);
        // UnityWebRequest를 사용하여 POST 요청
        UnityWebRequest request = UnityWebRequest.Post(aiServerUrl, body,"application/json");

        // 서버에 요청을 보냄
        yield return request.SendWebRequest();

        // 응답이 성공적인지 확인
        if (request.result == UnityWebRequest.Result.Success)
        {
            // 서버 응답 데이터를 JSON으로 처리
            string responseData = request.downloadHandler.text;
            Debug.Log("Response: " + responseData);
            JHdata data = JsonConvert.DeserializeObject<JHdata>(responseData);
            foreach (var a in data.questions)
            {
                Debug.Log(a.question);
                Debug.Log(a.options[0]);
                Debug.Log(a.options[1]);
                Debug.Log(a.options[2]);
                Debug.Log(a.options[3]);
            }
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
}