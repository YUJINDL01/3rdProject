using System;
using System.Collections.Generic;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ScoreManager :NetworkBehaviour
{
    public static ScoreManager Instance;
    private List<Problem> problemList;
    //public List<Button> answerButton;
    private List<int> clickedButton;
    private int score;
    public int currentScore;

    public List<string> answerOX;
    
    private GameObject[] oxImage;

    private List<int> playerScores;
   // public TMP_Text scoreTMP;
   
    public override void Spawned()
    {
        Instance = this;
        answerOX.Clear();
        
        Init();
        DontDestroyOnLoad(this);
    }
    

   private void Init()
   {
       GameObject o = GameObject.Find("Quiz/OX/O");
       GameObject x = GameObject.Find("Quiz/OX/X");
       oxImage = new[] { o, x };
       
       ProblemSave();
   }

   public void ProblemSave()
    {
        var problemInfo =ProblemManager.Instance.problemsList;
        problemList = problemInfo;
    }

    /*
    public void AnswerButton(int answer)
    {
        bool clickedButton = false;
        
        if (answer)
    }
    */

    public void AnswerCheck(int problemNum)
    {
        /*
         *  현재 문제의 번호에 해당하는 파일을 차아서 정답을 비교한다.
         * 1. 현재 문제의 번호를 가져온다.
         * 2. 정답을 비교한다.
         * 3. 화면에 반영
         * 4. 공유한다. 
         */
        Debug.Log("채점시작작작");
        
        int[] answerNum = problemList[problemNum].answer;
        int answerLength = problemList[problemNum].answer.Length;
        int correctAnswer = 0;

        ProblemManager.Instance.MAnswerSheet();
        List<int> clickedAnswer = ProblemManager.Instance.clickedButtonNum;
        clickedAnswer.Sort();
        
        Debug.Log(string.Join(", ", answerNum));
        
        Debug.Log(string.Join(", ", clickedAnswer));

        for (int i = 0; i < clickedAnswer.Count; i++)
        {
            int answer = clickedAnswer[i];

            if (answer == answerNum[i])
            {
                correctAnswer++;
            }
        }

        if (correctAnswer == answerLength)
        {
            score = problemList[problemNum].score;
            currentScore += score;

            Debug.Log($"score: {currentScore}");
            True();
        }
        else
        {
            Debug.Log("틀렸습니당!");
            False();
        }
        
        PlayerControll.Instance.RpcScore(currentScore);
    }

    private void True()
    {
        oxImage[0].gameObject.SetActive(true);
        oxImage[1].gameObject.SetActive(false);
        answerOX.Add("O");

    }

    private void False()
    {
        oxImage[0].gameObject.SetActive(false);
        oxImage[1].gameObject.SetActive(true);
        answerOX.Add("X");
    }

    public void OffOX()
    {
        oxImage[0].gameObject.SetActive(false);
        oxImage[1].gameObject.SetActive(false);
    }

    /*
    public void AddPlayerScore(int score)
    {
        playerScores.Add(score);
    }

    // 특정 플레이어의 점수를 반환하는 메서드
    public int GetPlayerScore(int playerIndex)
    {
        if (playerIndex >= 0 && playerIndex < playerScores.Count)
        {
            return playerScores[playerIndex]; // 점수 반환
        }
        else
        {
            Debug.LogError("Invalid player index"); // 유효하지 않은 인덱스 오류
            return 0; // 기본값 반환
        }
    }
    */
}
