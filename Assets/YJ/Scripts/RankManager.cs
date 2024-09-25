using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using TMPro;
using UnityEngine;

public class RankManager : NetworkBehaviour
{
// 플레이어 랭크 구조체
    [Serializable]
    public class PlayerRank
    {
        public string Name;
        public int Score;

        public PlayerRank(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
    public static RankManager Instance;
    
    public GameObject rankPrefabs; // 랭킹을 표현할 프리팹
    private List<Rank> rankObjects; // 생성된 랭킹 오브젝트 리스트
    private int totalPlayers = 0;
    private string[] rankel = new string[3]{"Pos", "Player", "Score" };

    public static Dictionary<NetworkBehaviourId, PlayerRank> playerRanks = new();
    
    public override void Spawned()
    {
        Instance = this;

        totalPlayers = RunnerController.Runner.SessionInfo.MaxPlayers; // 최대값 생성
        Debug.Log($"totalPlayers: {totalPlayers}");
        
        Debug.Log("랭킹 초기화 시작");

        Init(); // 시작 
    }
    
    public static void SetPlayerRank(NetworkBehaviourId id, string nickname = "", int score = 0)
    {
        Debug.Log($"SetPlayerRank : {id}, {nickname}, {score}");

        if (!playerRanks.ContainsKey(id))
        {
            playerRanks.Add(id, new PlayerRank("", 0));
        }
        
        if (nickname != "")
        {
            playerRanks[id].Name = nickname;
        }
        playerRanks[id].Score = score;
    }
    
    private void Init()
    {
        rankObjects = new List<Rank>(); // 생성된 게임 오브젝트를 저장

        Transform parent = GameObject.Find("RankCanvas/Scores/Ranking").transform; // 추가될 위치 


        var list = playerRanks.Values.ToArray();
        Array.Sort(list, (a, b) => b.Score.CompareTo(a.Score));
        
        var pos = 1;
        foreach (var rankinfo in list)
        {
            var rankGo = Instantiate(rankPrefabs, parent);
            var rank = rankGo.GetComponent<Rank>();
            rank.Set(pos++, rankinfo.Name, rankinfo.Score);
        }
    }

    /*
    public void AddPlayerRank(string nickname, int score)
    {
        playerRanks.Add(new PlayerRank(nickname, score)); // 플레이어 랭크 추가
        Debug.Log("랭크에 필요한 거 공유한다");
        UpdateRanking();
    }
*/
    private void UpdateRanking()
    {
        // 점수에 따라 정렬
        /*playerRanks.Sort((x, y) => y.Score.CompareTo(x.Score)); // 높은 점수부터 정렬
        //만약 오름차순 정렬을 원한다면, x.Score.CompareTo(y.Score)를 사용

        for (int i = 0; i < playerRanks.Count; i++)
        {
            rankElementsList[i][0].text = (i + 1).ToString(); // 순위
            rankElementsList[i][1].text = playerRanks[i].Name; // 플레이어 이름
            rankElementsList[i][2].text = playerRanks[i].Score.ToString(); // 점수
            Debug.Log($"rank: {rankElementsList[i][0].text}, {rankElementsList[i][1].text}, {rankElementsList[i][2].text}");
        }*/
    }
}

