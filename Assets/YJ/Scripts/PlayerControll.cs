using System;
using Fusion;
using TMPro;
using UnityEngine;

public class PlayerControll : NetworkBehaviour
{
    public TMP_Text nicknameText;
    public TMP_Text scoreText;

    private bool _isKeyPressed;
    
    public static PlayerControll Instance;

   public override void Spawned() //네트워크에서 객체가 생성될 때 호출
    {
        Debug.Log("Spawned");
        Transform parent = GameObject.Find("Quiz/Players").transform;
        transform.SetParent(parent);
        
        if (HasStateAuthority) 
        {
            Instance = this;
            
            //parent = GameObject.Find("Quiz/Players").transform;
            //transform.SetParent(parent);
            
        }
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        Debug.Log("DeSpawned");
        //ScoreManager.Instance.onScoreChanged -= OnScoreChanged; //이벤트 리스너?? 메모리 누수 방지??
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)] // 닉네임을 모든 클라이언트에 설정 
    public void RpcSetNickname(string nickname)
    { 
        nicknameText.text = nickname; // 입력받은 닉네임을 ui에 설정 
        Debug.Log($"OnNicknameChanged: {nickname}");
    }
    
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcScore(int currentScore)
    {
        scoreText.text = currentScore.ToString(); // 내거만 바뀌어야 하는데 공유가 안 됨
        
        Debug.Log($"currentScore : {currentScore}");
    }
  
    private void Update()
    {
        if (!HasStateAuthority)
            return;
        
        if (Input.GetMouseButtonDown(0))
        {
            _isKeyPressed = true;
        }
    }
    
}
