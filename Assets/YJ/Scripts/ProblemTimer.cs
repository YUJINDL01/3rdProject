using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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
        timerSec = 11; 
        int currentTime = timerSec;
        timer.color = _originColor;

        while (currentTime > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            currentTime--;

            if (currentTime <= 5)
            {
                timer.color = _iColor;
            }

            timer.text = $"{00:00}:{currentTime:00}";
        }

        yield return null;
        
        TimeDone();
    }

    void TimeDone()
    {
        timeOver++;
        
        if (timeOver == 5)
        {
            TestEnd.Instance.OnEnd();   
        }
        
        ProblemManager.Instance.MMButton(+1);


        StartCoroutine(Timers());
    }
}
