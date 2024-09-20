using System;
using System.Collections;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public NetworkPrefabRef sharedGameDataPrefabs;
   public NetworkPrefabRef playerPrefab;
   public NetworkPrefabRef gameCanvas;

   public Button readyButton;
   public GameObject readyCanvas;
   public TMP_Text readyText;
   public TMP_Text countdownText;

   public int countdown = 3;

   private TickTimer startTimer;
   private NetworkObject _spawnedPlayer;
   private bool _isReady;

   public static GameManager Instance;

   private void Awake()
   {
      Instance = this;
   }

   public void Start()
   {
      //StartCoroutine(Process());
   }

   public void Ready()
   {
      _isReady = true;
      readyButton.interactable = false;
      SharedGameData.Instance.RpcReady();
   }
   
   // 게임을 종료한 것을 다른 사람들과 공유할 것 인지? 
   
   // 게임 종료 어떻게 할 것인지??
   public void ExitGame()
   {
      if (_spawnedPlayer != null)
      {
         _spawnedPlayer.Runner.Shutdown();
         _spawnedPlayer = null; 
      }

      RunnerController.Runner.Shutdown();

      TestEnd.Instance.OnEnd(); // 원래 제출하기 버튼 누르면 실행되던 매소드 , 비동기 방식으로 씬을 전환해준다. 
      // 그냥 씬 로드하는 것도 ㄱㅊ
   }

   
   private IEnumerator Process()
   {
      //초기화 
      _isReady = false;
      //게임창에서 시간 멈추기?? 혹은 스폰 안 하기 
      readyButton.gameObject.SetActive(true);
      Debug.Log("초기화 완료");
      
      //SharedGameData 스폰 
      var dataOp = RunnerController.Runner.SpawnAsync(sharedGameDataPrefabs);
      yield return new WaitUntil(() => dataOp.Status == NetworkSpawnStatus.Spawned);
      // 닉네임 추가
      //dataOp.Object.name = $"{nameof(SharedGameData)}: {dataOp.Object.Id}";

      // 모든 플레이어가 레디할 때까지 대기 
      var wfs = new WaitForSeconds(0.5f);
      while (true)
      {
         var totalCount = RunnerController.Runner.SessionInfo.PlayerCount;
         var currentCount = SharedGameData.ReadyCount;
         readyText.text = $"Ready?\n({currentCount}/{totalCount})";

         yield return wfs;
         
         if (currentCount == totalCount)
            break;
      }

      readyText.text = $"Start!";
      yield return wfs; 
      
      // 플레이어 스폰 
      var op = RunnerController.Runner.SpawnAsync(playerPrefab);
      yield return new WaitUntil(() => dataOp.Status == NetworkSpawnStatus.Spawned);
      _spawnedPlayer = op.Object;
      //_spawnedPlayer.name = $"Player: {_spawnedPlayer.Id}";

      var playerController = _spawnedPlayer.GetComponent<PlayerController>();
      //playerController.Off(); // 움직이지 못 하게하는 기능 구현 
      
      // 카운트다운 UI 활성화 
      readyButton.gameObject.SetActive(false);
      readyCanvas.gameObject.SetActive(false);
      RunnerController.Runner.SpawnAsync(gameCanvas);
      countdownText.gameObject.SetActive(true);
      
   }
   
}









































