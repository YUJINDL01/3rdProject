using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text timer;
    public TestEnd testEnd;
    public bool gameEnd = false; // 이 변수를 가져가서 조작
        
    private void Start()
    {
        int problemQuantity = 10; // 초기값 
        StartCoroutine(TimeTimer(problemQuantity));
    }

     IEnumerator TimeTimer(int problemQuantity)
     {
         int times = 0; 
         int currentMIn = problemQuantity;
         int currentSec = 60;
         
         while (currentMIn != 0 || currentSec != 0)
         { 
            currentMIn = problemQuantity - ((times / 60) + 1);
            currentSec = 59 - (times % 60);
            timer.text = $"{currentMIn:00}:{currentSec:00}"; // 두자리수까지 출력 
            
            yield return new WaitForSecondsRealtime(1f);
            
            times++;
         }
         
         MTimeOver();
         gameEnd = true;
     }

     private void MTimeOver()
     {
         testEnd.OnEnd();
     }
     //끝나면 타임오버하게 하기 
}
