using TMPro;
using UnityEngine;

public class ScorePop : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text pass;

    private int currentScore;
    
    void Start()
    {
        currentScore = PlayerPrefs.GetInt("Score", 0);
        score.text = $"{currentScore}";

        if (currentScore < 25 * 0.6)
        {
            pass.text = "2종 보통 필기에 불합격했습니다.";
        }
        else
        {
            pass.text = "2종 보통 필기에 합격했습니다.";
        }
        //최고 점수 평균 점수 등수 부분 추가 
    }
    
}
