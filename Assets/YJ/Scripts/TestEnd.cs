using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestEnd : MonoBehaviour
{
    public Button endButton;
    //public Slider loadingBar;
    //public TMP_Text loadingText;
    
    public GameObject scoremanager;
    private Score score;

    public GameObject problemManagerO;
    private ProblemManager problemManager;
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
}
