using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    private const string MainSceneName = "Written_TEST";
    
    public GameObject runnerPrefab; //네트워크 러너를 생성하기 위한 프리팹, 게임의 네트워크 관리
   
    public TMP_InputField roomNameInputField;
    public TMP_InputField participantsInputField;
    public TMP_InputField problemCountInputField;
    
    private static NetworkRunner Runner; // 현재 사용 중인 네트워크 러너의 인스턴스를 저장, 정적으로 선언되어 있으며, 클래스의 모든 인스턴스가 이 변수를 공유 

    public enum ProblemPart : int
    {
        s1,
        s2,
        l,
        p,
        s, 
        v,
    }
    
    public void CreateRoom()  
    {
        StartCoroutine(CreateRoom(roomNameInputField.text, participantsInputField.text)); 
    }

    //Debug.Log($"runnerPrefab: {runnerPrefab}, roomNameInputField: {roomNameInputField}");
    private IEnumerator CreateRoom(string roomName, string participants)
    {
        Debug.Log($"CreateRoom : {roomName}");

        if (Runner == null) //러너가 아직 생성되지 않았다면
        {
            //runnerPrefab을 인스턴스화하여 새 네트워크 러너를 생성 
            var runnerGo = Instantiate(runnerPrefab);
            Runner = runnerGo.GetComponentInChildren<NetworkRunner>();
            
            var controller = Runner.GetComponent<RunnerController>();
            controller.Init();
        }

        yield return new WaitForSeconds(0.1f);
        
        Debug.Log($"runnerPrefab: {runnerPrefab}, roomNumInputField: {roomNameInputField}");
        
        int maxPlayers = int.Parse(participantsInputField.text); 
        Debug.Log($"maxPlayers : {maxPlayers}");
        
        //int problemCount = int.Parse(problemCountInputField.text);
        //Debug.Log($"problemCount: {problemCount}");
        
        // 문제 숫자랑 분야도 넘겨주고 싶음 
        /*
        var sessionProperties = new SessionProperties()//new Dictionary<int, SessionProperty>()
        {
            {"problemNum", problemCount},
            //customProps["problemNum"] = problemCount;
        };
        */
        
        Runner.StartGame(new StartGameArgs //새 게임 세션을 시작하는 메소드, 필요한 설정을 전달 // 왜 에러가 생길까??
        {
            SessionName = roomName, // 생성할 방의 이름 
            GameMode = GameMode.Shared, // 게임 모드, shared 모드 사용 
            Scene = SceneRef.FromIndex(
                SceneUtility.GetBuildIndexByScenePath(MainSceneName)), // 메인 장면의 인덱스를 가져와 세션에서 사용할 장면으로 설정 
            PlayerCount = maxPlayers, // 최대 인원수 ??
            //SessionProperties = customProps,
            //MatchmakingMode = MatchmakingMode.FillRoom, -> 참여하는 쪽에서 추가 하는 것 
            //IsOpen = true, 
            //IsVisible = true, 
        });
    }
}
