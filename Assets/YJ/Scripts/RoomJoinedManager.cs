using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomJoined : MonoBehaviour
{
    private const string MainSceneName = "Written_TEST";
    private NetworkRunner runner;

    private void Start()
    {
        // NetworkRunner 인스턴스 초기화
        runner = gameObject.AddComponent<NetworkRunner>();
        runner.ProvideInput = false; // 필요한 경우에 따라 조정
    }

    // 버튼 클릭 시 호출될 메서드
    public async void JoinRandomSession() // async 비동기 작업을 수행할 수 있도록 설정 
    {
        var result = await runner.StartGame(new StartGameArgs() // 
        {
            SessionName = "1234", //일단 방법호 1234에서 하게하기 
            GameMode = GameMode.Shared, // 무작위로 세션에 참여
            MatchmakingMode = MatchmakingMode.FillRoom, // 오래된 방부터 채움 
            Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(MainSceneName)), // 씬 옮겨 가는 거 까진 성공했으나 동기화 안 됨
        });

        if (result.Ok) // 세션 참여가 성공했는지 
        {
            Debug.Log("Successfully joined a random session!");
            
            // 현재 어느 세션에 참가하는 지 잘 모르겠다. 

            string sessionName = runner.SessionInfo.Name;
            Debug.Log($"@@@: {sessionName}");
        }
        else
        {
            Debug.LogError($"Failed to join session: {result.ShutdownReason}");
        }
    }
}