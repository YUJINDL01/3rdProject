using System;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class RoomManager : MonoBehaviour
{
    private const string MainSceneName = "Written_TEST";
    
    public GameObject runnerPrefab; //네트워크 러너를 생성하기 위한 프리팹, 게임의 네트워크 관리
    public TMP_InputField roomNameInputField;

    private static NetworkRunner Runner; // 현재 사용 중인 네트워크 러너의 인스턴스를 저장, 정적으로 선언되어 있으며, 클래스의 모든 인스턴스가 이 변수를 공유 
    
    public void CreateRoom()  
    {
        CreateRoom(roomNameInputField.text); 
    }

    //Debug.Log($"runnerPrefab: {runnerPrefab}, roomNameInputField: {roomNameInputField}");
    private void CreateRoom(string roomName)
    {
        Debug.Log($"CreateRoom : {roomName}");

        if (Runner == null) //러너가 아직 생성되지 않았다면
        {
            //runnerPrefab을 인스턴스화하여 새 네트워크 러너를 생성 
            var runnerGo = Instantiate(runnerPrefab);
            Runner = runnerGo.GetComponentInChildren<NetworkRunner>();
        }
        
        Debug.Log($"runnerPrefab: {runnerPrefab}, roomNumInputField: {roomNameInputField}");
        
        Runner.StartGame(new StartGameArgs//새 게임 세션을 시작하는 메소드, 필요한 설정을 전달 // 왜 에러가 생길까??
        {
            SessionName = roomName, // 생성할 방의 이름 
            GameMode = GameMode.Shared, // 게임 모드, shared 모드 사용 
            //MaxPlayers = 6; // 최대 인원수 
            Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(MainSceneName)), // 메인 장면의 인덱스를 가져와 세션에서 사용할 장면으로 설정 
            PlayerCount = 6, // 최대 인원수 ??
        });
    }
}
