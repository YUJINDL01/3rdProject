using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class SharedGameData : NetworkBehaviour
{
    public static SharedGameData Instance;

    public List<Problem> problemsList = new List<Problem>();
    
    public static int ReadyCount { get; set; }
    //public static int GameEndCount { get; set; }

    public override void Spawned()
    {
        if (!HasStateAuthority)
            return;
        
        Instance = this;

        
        ReadyCount = 0;
        
        //InitializeProblems();
        // -> 같은 플레이어의 플레이어가 2개 생기는 듯
    }

    // 일단 준비만 감지 
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcReady(RpcInfo info = default)
    {
        ReadyCount++;
        Debug.Log($"ReadyCount Changed : {ReadyCount}");
    }

    /*
    private void InitializeProblems()
    {
        //problemsList = 
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcShareProblems(List<Problem> sharedProblens)
    {
        
    }
    */

}