using System;
using System.Collections;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestEnd : NetworkBehaviour
{
    public Button endButton;
    //public Slider loadingBar;
    //public TMP_Text loadingText;

    public GameObject scoremanager;
    private Score score;

    public GameObject problemManagerO;
    private ProblemManager problemManager;

    public static TestEnd Instance;

    //private static NetworkRunner Runner;

    public override void Spawned()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        /*
        if (Runner == null) //러너가 아직 생성되지 않았다면
        {
            //runnerPrefab을 인스턴스화하여 새 네트워크 러너를 생성
            var runnerGo = Instantiate(runnerPrefab);
            Runner = runnerGo.GetComponentInChildren<NetworkRunner>();

            var controller = Runner.GetComponent<RunnerController>();
            controller.Init();
        }
        */
    }

    public void OnEnd()
    {
        //scoremanager.SetActive(true); //모르겠땅 이때 활성화시키면 안되나? 그리고 이렇게 활성화 시킬 수 없나? 매소드를 어떨

        //버튼 한번 누르면 인터랙팅 안 되게 하기 
        problemManager = problemManagerO.GetComponent<ProblemManager>();
        problemManager.MAnswerSheet();

        score = scoremanager.GetComponent<Score>();
        score.Scoring();

        StartCoroutine(TransitionNextScene());
    }

    private IEnumerator TransitionNextScene()
    {
        if (Runner != null)
        {
            if (Runner.IsSceneAuthority)
            {
                Debug.Log("네트워크 씬 바꾸는 중");
                Runner.LoadScene(SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath("Written_Result")));
                yield return null;
                //RankManager.Instance.Ranking(); // 안되면 순서 바꾸기
                Debug.Log("랭킹이 소환");
            }
        }
        else
        {
            Debug.Log("씬 바꿀려고 하는데 러너 없음!");
        }

        
        /*
        else
        {
            Debug.Log("로딩 시작");
            AsyncOperation ao = SceneManager.LoadSceneAsync("Written_Result");

            ao.allowSceneActivation = false;

            while (!ao.isDone)
            {
                float p = ao.progress / 0.9f;
                //loadingBar.value = p;
                //loadingText.text = $"{p * 100f}";

                if (p >= 0.99999f)
                {
                    ao.allowSceneActivation = true;
                }

                yield return null;
            }
            Debug.Log("로딩 끝");

        }
        */

    }
}
