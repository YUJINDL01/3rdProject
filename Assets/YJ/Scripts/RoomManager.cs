using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    private const string MainSceneName = "Written_TEST";
    
    public GameObject runnerPrefab; //네트워크 러너를 생성하기 위한 프리팹, 게임의 네트워크 관리
   
    public TMP_InputField roomNameInputField;
    //public TMP_InputField participantsInputField;
    //public TMP_InputField problemCountInputField;
    public Button[] peopleNumButtons;
    public Button[] problemNumButtons;
    public Button[] problemPartButtons;

    private bool _isClickedPeople;
    private bool _isClickedProblems;
    private bool _isClickedPart;

    private bool[] clickPeople = new bool[5]{false,false,false,false,false};
    private bool[] clickProblem = new bool[4]{false,false,false,false};
    
    
    private Color _clickColors = new Color(46f/255f, 199f/255f, 111f/255f, 255f/255f);
    private Color _originalColors = Color.white;
    
    public static RoomManager Instance;
    
    private static NetworkRunner Runner; // 현재 사용 중인 네트워크 러너의 인스턴스를 저장, 정적으로 선언되어 있으며, 클래스의 모든 인스턴스가 이 변수를 공유 


    [HideInInspector]
    public string mode;
    [HideInInspector]
    public bool[] clickPart = new bool[6]{true,true,true,true,true,true}; 
    [HideInInspector]
    public int peopleNum = 0;
    [HideInInspector]
    public int problemNum = 0; 

    public void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void PeopleNum(int num)
    {
        if (clickPeople[num-1] == false) // 클릭 안 되어 있는데 입력이 들어온다면 
        {
            // 클릭색으로 변경 
            for (int i = 0; i < peopleNumButtons.Length; i++)
            {
                if (i == num - 1)
                {
                    peopleNumButtons[num - 1].image.color = _clickColors;
                    peopleNum = num;
                    clickPeople[i] = true;
                }
                else
                {
                    peopleNumButtons[i].image.color = _originalColors;
                    clickPeople[i] = false;
                }

            }
        }
        
        Debug.Log($"참여인원 :{peopleNum}");
        
    }

    public void ProblemNum(int proNum)
    {
        int num = 0;
        
        Debug.Log("문제 세팅");
        
        switch (proNum)
        {
            case 5:
                num = 1;
                break;
            case 10:
                num = 2;
                break;
            case 20:
                num = 3;
                break;
            case 40:
                num = 4;
                break;
        }
        
        if (clickProblem[num-1] == false) // 클릭 안 되어 있는데 입력이 들어온다면 
        {
            // 클릭색으로 변경 
            for (int i = 0; i < problemNumButtons.Length; i++)
            {
                if (i == num - 1)
                {
                    problemNumButtons[num - 1].image.color = _clickColors;
                    problemNum = proNum;
                    clickProblem[i] = true;
                }
                else
                {
                    problemNumButtons[i].image.color = _originalColors;
                    clickProblem[i] = false;
                }
            }
        }
        
        Debug.Log($"문제수: {problemNum}");
        
    }

    public void ProblemPart(int num)
    {
        Debug.Log($"problemPart: {num}");
        
        if (clickPart[num] == false) // 클릭 안 되어 있는데 입력이 들어온다면 
        {
            problemPartButtons[num].image.color = _clickColors;
            clickPart[num] = true;
        }
        else
        {
            problemPartButtons[num].image.color = _originalColors;
            clickPart[num] = false;
        }
    }

    public void CreateRoom()  
    {
        StartCoroutine(CreateRoom(roomNameInputField.text, peopleNum)); 
    }

    //Debug.Log($"runnerPrefab: {runnerPrefab}, roomNameInputField: {roomNameInputField}");
    private IEnumerator CreateRoom(string roomName, int participants)
    {
        mode = ModeManager.Instance.mode;
        Debug.Log($"Mode : {mode}");
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
        
        int maxPlayers = participants; 
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
