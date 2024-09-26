using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControll : NetworkBehaviour
{
    public TMP_Text nicknameText;
    public TMP_Text scoreText;

    public static PlayerControll Instance;
    public Sprite[] playerImages; // 사용할 수 있는 이미지 배열
    private Image playerImage; // 플레이어의 이미지 컴포넌트

    public override void Spawned() // 네트워크에서 객체가 생성될 때 호출
    {
        Debug.Log("Spawned");
        Transform parent = GameObject.Find("Quiz/Players").transform;
        transform.SetParent(parent);

        playerImage = GetComponentInChildren<Image>(); // 자식 오브젝트에서 Image 컴포넌트를 찾기

        if (HasStateAuthority) 
        {
            Instance = this;
            DontDestroyOnLoad(this);
            
            // 플레이어 이미지 설정
            int playerOrder = Runner.SessionInfo.PlayerCount - 1; // 플레이어의 순서 (0부터 시작)
            RpcSetPlayerImage(playerOrder);
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Debug.Log("DeSpawned");
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)] // 닉네임을 모든 클라이언트에 설정 
    public void RpcSetNickname(string nickname)
    { 
        nicknameText.text = nickname; 
        RankManager.SetPlayerRank(Id, nickname);
        Debug.Log($"OnNicknameChanged: {nickname}");
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcScore(int currentScore)
    {
        scoreText.text = $"Score {currentScore.ToString()}";
        RankManager.SetPlayerRank(Id, "", currentScore);
        Debug.Log($"currentScore : {currentScore}");
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcSetPlayerImage(int playerOrder)
    {
        if (playerOrder >= 0 && playerOrder < playerImages.Length) // 배열 범위 체크
        {
            playerImage.sprite = playerImages[playerOrder]; // 이미지 변경
            Debug.Log($"플레이어 이미지 변경됨: {playerImages[playerOrder].name}");
        }
        else
        {
            Debug.LogError("유효하지 않은 플레이어 순서입니다.");
        }
    }
}