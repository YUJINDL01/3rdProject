using System;
using System.Collections;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public NetworkPrefabRef sharedGameDataPrefabs;
   public NetworkPrefabRef playerPrefab;
   //public NetworkPrefabRef gameCanvas;


   public Button readyButton;
   public GameObject readyCanvas;
   public TMP_Text readyText;
   //public TMP_Text countdownText;

   public GameObject quizCanvas;

   public int countdown = 3;

   private TickTimer startTimer;
   
   public NetworkObject parentObject;
   private NetworkObject _spawnedPlayer;
   private bool _isReady;
   
   public static GameManager Instance;

   private void Awake()
   {
      Instance = this;
     
      readyCanvas.SetActive(true);
      quizCanvas.SetActive(true);
   }

   public void Start()
   {
      if (sharedGameDataPrefabs == null)
      {
         Debug.Log("sharedGameDataPrefabs is null");
      }
      
      StartCoroutine(Process());
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
      Debug.Log("초기화 완료");

      yield return null;
      
      //SharedGameData 스폰 
      var dataOp = RunnerController.Runner.SpawnAsync(sharedGameDataPrefabs); // RunnerController가 없음 
      Debug.Log($"dataOp: {dataOp}");
      // yield return new WaitUntil(() => dataOp.Status == NetworkSpawnStatus.Spawned);
      while (dataOp.Status != NetworkSpawnStatus.Spawned)
      {
         yield return null;
      }
      
      // 닉네임 추가
      //dataOp.Object.name = $"{nameof(SharedGameData)}: {dataOp.Object.Id}";

      // 모든 플레이어가 레디할 때까지 대기 
      var wfs = new WaitForSeconds(0.5f);
      while (true)
      {
         var totalCount = RunnerController.Runner.SessionInfo.MaxPlayers;
         var currentCount = SharedGameData.ReadyCount;
         Debug.Log($"totalCount: {totalCount}, currentCount: {currentCount}");
         readyText.text = $"Ready?\n({currentCount}/{totalCount})";

         yield return wfs;
         
         if (currentCount == totalCount)
            break;
      }

      readyText.text = $"Start!\n({SharedGameData.ReadyCount}/{RunnerController.Runner.SessionInfo.PlayerCount})";
      yield return wfs; 
      
      
      /*
      // 플레이어 스폰 
      var op = RunnerController.Runner.SpawnAsync(playerPrefab);
      while (op.Status != NetworkSpawnStatus.Spawned)
      {
         yield return null;
      }
      _spawnedPlayer = op.Object;

      Debug.Log("Player Spawned!");
      
      //GameObject parentObject = GameObject.Find("Players"); // 부모 오브젝트의 이름 -> 생성된 게임 오브젝트의 프리팹, 그리고 생성된 오브젝트를 다른 변수에 저장하고 클론을 부모로 배정
      if (parentObject != null)
      {
         Debug.Log($"parentObject : {parentObject}");
         _spawnedPlayer.transform.SetParent(parentObject.transform);
      }
      else
      {
         Debug.Log("부모 오브젝트를 찾을 수 없습니다.");
      }
      
      //_spawnedPlayer.name = $"Player: {_spawnedPlayer.Id}";

      var playerController = _spawnedPlayer.GetComponent<PlayerController>();
      //playerController.Off(); // 움직이지 못 하게하는 기능 구현 
      */
      /*
      // 카운트다운 UI 활성화 
      readyButton.gameObject.SetActive(false);
      readyCanvas.gameObject.SetActive(false);
      countdownText.gameObject.SetActive(true);
      
      // 카운트다운 진행 
      startTimer = TickTimer.CreateFromSeconds(RunnerController.Runner, countdown + 1.1f);

      for (var i = countdown; i > 0; i--)
      {
         countdownText.text = $"{i}";
         yield return new WaitForSeconds(1f);
         countdownText.text = string.Empty;
         yield return new WaitForSeconds(0.1f);
      }

      //네트워크 상의 시간이 완료될 때까지 대기 
      yield return new WaitUntil(() => startTimer.Expired(RunnerController.Runner));
      */
      
      //게임 시작 
      // 플레이어 동작 
      //RunnerController.Runner.SpawnAsync(gameCanvas);
      //quizCanvas.SetActive(true);
      PlayerControll.Instance.RpcSetNickname(LoginManager.Value);
      readyCanvas.SetActive(false);
      StartCoroutine(ProblemTimer.Instance.Timers()); //코루틴 실행하는 법
      Debug.Log("레디 끝남");
   }
   
}









































