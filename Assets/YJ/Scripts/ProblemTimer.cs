using System;
using System.Collections;
using Fusion;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class ProblemTimer : MonoBehaviour
{
    public TMP_Text timer;

    private int timerSec;

    public static ProblemTimer Instance;

    private Color _originColor = new Color(46f / 255f, 199f / 255f, 111f/255f, 255f / 255f);
    private Color _iColor = Color.red;
    private int timeOver = 0;
    private int limit;

    private void Awake()
    {
        Instance = this;
       // StartCoroutine(Timers());
       limit = RoomManager.Instance.problemNum;
    }
    
    public IEnumerator Timers()
    {
        Debug.Log("타이머 시작");
        timerSec = 10; 
        int currentTime = timerSec;
        timer.color = _originColor;

        while (currentTime > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            currentTime--;

            if (currentTime <= 3)
            {
                timer.color = _iColor;
            }

            timer.text = $"{00:00}:{currentTime:00}";
        }

        yield return null;
        
        ScoreManager.Instance.AnswerCheck(timeOver);
        yield return new WaitForSecondsRealtime(1f);
        ScoreManager.Instance.OffOX();
        
        TimeDone();
    }

    void TimeDone()
    {
        
        timeOver++;

        if (timeOver == limit)
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
