using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Sockets;
using UnityEngine;


public class RunnerController : MonoBehaviour, INetworkRunnerCallbacks
{
    
    public static NetworkRunner Runner;
    public NetworkObject objectToOwn;

    public void Init() // 게임 오브젝트가 활성화 할 떄 호출 
    {
        Debug.Log("러너 컨트롤러 생성됨");
        DontDestroyOnLoad(this); // 새로운 씬 로드되더라도 파괴되지 않도록 
        Runner = GetComponent<NetworkRunner>(); // 현재 게임 오브젝트에서 NetworkRunner 컴포넌트를 가져와서 Runner 필드에 저장 
        Debug.Log("러너생성 진짜 됨");
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) // 플레이어사 게임에 참여 
    {
        Debug.Log($"OnPlayerJoined : {player.PlayerId}");
        
        /*
        Debug.Log("###사람왔다");
        // 첫 번째 플레이어인지 확인
        if (runner.ActivePlayers.ToList().Count == 1&& objectToOwn != null)
        {
            // 소유권 부여
            objectToOwn.AssignInputAuthority(player);
            Debug.Log($"Player {player.PlayerId} has ownership of the object.");
        }
        else if (objectToOwn == null)
        {
            Debug.Log("왜 소유권 있는 물체 없음 !!");
        }
        */
    }

    public void OnConnectedToServer(NetworkRunner runner) // 서버에 연결되었을 때 
    {
    }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) // 게임 나갔을 때 
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input) //플레이어의 입력을 처리하는 메서드 
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) // 서버에서 연결이 끊어졌을 때 
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}
