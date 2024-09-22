using Fusion;
using UnityEngine;

public class SharedGameData : NetworkBehaviour
{
    public static SharedGameData Instance;

    public static int ReadyCount { get; set; }
    //public static int GameEndCount { get; set; }

    public override void Spawned()
    {
        if (!HasStateAuthority)
            return;
        
        Instance = this;

        ReadyCount = 0;
        //GameEndCount = 0; 
    }

    // 일단 준비만 감지 
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RpcReady(RpcInfo info = default)
    {
        ReadyCount++;
        Debug.Log($"ReadyCount Changed : {ReadyCount}");
    }
}
