using System;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    public static RankManager Instance;
    
    public GameObject rankPrefabs; // 랭킹을 표현할 프리팹
    private List<GameObject> rankObjects; // 생성된 랭킹 오브젝트 리스트
    private List<TMP_Text[]> rankElementsList; // 각 랭킹 요소들의 리스트
    private int totalPlayers = 0;
    private string[] rankel = new string[] {"Pos", "Player", "Score" };

    private string nickname;
    private int finalScore;

    private List<PlayerRank> playerRanks;
    public void Awake()
    {
        Instance = this;

        nickname = LoginManager.Value;
        finalScore = ScoreManager.Instance.currentScore;
        
        totalPlayers = RunnerController.Runner.SessionInfo.MaxPlayers; // 최대값 생성
        Debug.Log($"totalPlayers: {totalPlayers}");

        playerRanks = new List<PlayerRank>();
        PlayerControll.Instance.RpcRankElement(nickname, finalScore); 
        Init(); // 시작 
    }

    private void Init()
    {
        rankObjects = new List<GameObject>(); // 생성된 게임 오브젝트를 저장
        rankElementsList = new List<TMP_Text[]>(); // 각 요소들을 저장 

        Transform parent = GameObject.Find("RankCanvas/Scores/Ranking").transform; // 추가될 위치 

        for (int i = 0; i < totalPlayers; i++)
        {
            // 각 플레이어에 대해 랭킹 프리팹 인스턴스화
            GameObject rankObject = Instantiate(rankPrefabs, Vector3.zero, Quaternion.identity);
            rankObject.transform.SetParent(parent);
            rankObjects.Add(rankObject);

            // 랭킹 요소들 초기화
            TMP_Text[] rankElements = new TMP_Text[3]; // Pos, Player, Score
            for (int j = 0; j < rankElements.Length; j++)
            {
                Transform childTransform = rankObject.transform.Find($"{rankel[j]}");
                rankElements[j] = childTransform.GetComponent<TMP_Text>();
            }
            rankElementsList.Add(rankElements); // 각 플레이어의 랭킹 요소들을 리스트에 추가
        }

        UpdateRanking();
    }

    public void AddPlayerRank(string nickname, int score)
    {
        playerRanks.Add(new PlayerRank(nickname, score)); // 플레이어 랭크 추가
        UpdateRanking();
    }

    private void UpdateRanking()
    {
        Debug.Log("랭킹이 업데이트되었습니다.");

        for (int i = 0; i < playerRanks.Count; i++)
        {
            rankElementsList[i][0].text = (i + 1).ToString(); // 순위
            rankElementsList[i][1].text = playerRanks[i].Name; // 플레이어 이름
            rankElementsList[i][2].text = playerRanks[i].Score.ToString(); // 점수
        }
    }
}

// 플레이어 랭크 구조체
[Serializable]
public struct PlayerRank
{
    public string Name;
    public int Score;

    public PlayerRank(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
