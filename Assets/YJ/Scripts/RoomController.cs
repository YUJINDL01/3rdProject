using System;
using System.Collections.Generic;
using System.Linq;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class RoomController : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkObject objectToOwn; // 소유권을 줄 오브젝트

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
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
