using Fusion;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class PlayerSpawn : SimulationBehaviour, IPlayerJoined
{
   public GameObject playerPrefab; 

   private NetworkObject _spawnedPlayer; //실제 오브젝트 
   private GameObject parentObject;
   private Vector3 parentTransform;
   
   public void PlayerJoined(PlayerRef player)
   {
       /*
       var playerOp = RunnerController.Runner.SpawnAsync(playerPrefab);
       //Vector3 parentTransform = parentObject.transform.position;
       
       while (playerOp.Status != NetworkSpawnStatus.Spawned)
       {
           //대기??
           Debug.Log("플레이어 스폰 대기중");
       }
       

       _spawnedPlayer = playerOp.Object;
       _spawnedPlayer.transform.SetParent(parentObject.transform);
       
       Debug.Log("플레이어 스폰 됨");
       */
       parentObject = GameObject.Find("Players");
       parentTransform = parentObject.transform.position;
       
       Debug.Log("플레이서 스폰 할라고");
       
       if (player == Runner.LocalPlayer)
       {
           // 플레이어가 생성되어 있으면 안되게 하기 
           Debug.Log("플레이어 스폰");
           var playerOp = Runner.Spawn(playerPrefab, parentTransform, Quaternion.identity);
           //playerOp.transform.SetParent(parentObject.transform);
           //PlayerControll.Instance.RpcSetNickname(LoginManager.Value);
       }
       
   }

}
