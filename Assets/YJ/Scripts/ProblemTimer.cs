using System;
using System.Collections;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using WebSocketSharp;

public class ProblemTimer : MonoBehaviour
{
    public TMP_Text timer;

    private int timerSec;

    public static ProblemTimer Instance;

    private Color _originColor = new Color(50f / 255f, 187f / 255f, 108f/255f, 255f / 255f);
    private Color _iColor = Color.red;
    private int timeOver = 0;

    private void Awake()
    {
        Instance = this;
       // StartCoroutine(Timers());
    }
    
    public IEnumerator Timers()
    {
        Debug.Log("타이머 시작");
        timerSec = 10; 
        int currentTime = timerSec;
        timer.color = _originColor;

        while (currentTime > 0)
        {
            timer.text = $"{00:00}:{currentTime:00}";
            yield return new WaitForSecondsRealtime(1f);
            currentTime--;

            if (currentTime <= 3) // 변수 긴급시간으로 변경 
            {
                timer.color = _iColor;
            }

           // timer.text = $"{00:00}:{currentTime:00}";
        }

        if (currentTime == 0) // 각 문제가 끝나는 이벤트에 매소드 등록하는 방식으로 변경
        {
            timer.text = String.Empty;
        }

        yield return null;
        
        StartCoroutine(TimeDone());
    }

    private IEnumerator TimeDone()
    {
        ScoreManager.Instance.AnswerCheck(timeOver);
        //ProblemManager.Instance.MAnswerSheet();
        yield return new WaitForSecondsRealtime(1f);
        ScoreManager.Instance.OffOX();
        
        timeOver++;

        if (timeOver == 5)
        {
            ResultManager.Instance.AnswerOXSave(); // 정담 여부랑 답안지 만들기 위함
            
            if (TestEnd.Instance != null)
            {

                Debug.Log("네트워크씬 바꾼다");
                TestEnd.Instance.OnEnd();
                //networkSccenManager.OnEnd();   
                //NetworkBehaviour networkSccenManager = sceneManager.GetComponent<NetworkBehaviour>();

                Debug.Log("바꾸기로 함");
            }
            else
            {
                Debug.Log("테스트 엔드 이상함!!");
            }
        }
        
        ProblemManager.Instance.MMButton(+1);


        StartCoroutine(Timers());
    }
}
