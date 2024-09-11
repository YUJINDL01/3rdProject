using System;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private GameObject problemManger;
    private ProblemManager problemManagerScript;
    private int[,] answerSheet;
    private List<Problem> problemList;
    public int scorePoint = 0;
    private bool[] correctOrIncorrect;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Debug.Log("시작햇디");
        
        problemManger = GameObject.Find("ProblemManager");
        if (problemManger == null)
        {
            Debug.LogError("ProblemManager GameObject not found.");
            return;
        }

        problemManagerScript = problemManger.GetComponent<ProblemManager>();
        if (problemManagerScript == null)
        {
            Debug.LogError("ProblemManager script not found on ProblemManager GameObject.");
            return;
        }
        
        problemList = problemManagerScript.problemsList;
        if (problemList == null)
        {
            Debug.LogError("ProblemList is not initialized.");
            return;
        }

        correctOrIncorrect = new bool[problemList.Count];
    }
    

    public void Scoring() // 게임 종료하기 누르는 순간 실행되고 Score를 PlayerPrefs에 저장 
    {
        
        answerSheet = problemManagerScript.answerSheet; // 왜 안 될까?? -> 일단 시작시에 받아오면 안됨 제출하기 눌렀을떄 되어야함
        
        if (answerSheet == null)
        {
            Debug.LogError("AnswerSheet is not initialized.");
            return;
        }
        
        Debug.Log("채점 시작");
        
        List<int> selectedAnswer = new List<int>(); // 선택된 정답지에서 숫자만 뽑을 코드 
        for (int problemNum = 0; problemNum < answerSheet.GetLength(0); problemNum++)//문제번호 처음부터 끝까지  
        {
            int correct = 0; //처음은 맞은 수 0 
            selectedAnswer.Clear(); // 처음은 답안 저장 리스트도 초기화 
            
            for (int answerNum = 0; answerNum < answerSheet.GetLength(1); answerNum++) // 저장된 답안을 처음주터 끝까지 순회하는 코드, 5의 길이를 가짐 
            {
                int answer = answerSheet[problemNum, answerNum]; // 정답은 해당문제와 정답 번호를 저장. 

                if (answer != 0) // 0이 아니라면 1~5까지 존재한다
                {
                    selectedAnswer.Add(answer);// 선택된 숫자만 추가 
                }
            }
            
            Debug.Log("맞는지 아닌지 비교");
            
            for (int i = 0; i < problemList[problemNum].answer.Length; i++) // 문제의 기본 설정된 정답 길이까지 순회 
            {
                if (selectedAnswer.Count == problemList[problemNum].answer.Length) // 답안의 길이가 다르다면 실행할 필요가 없다. 
                {
                    if (selectedAnswer[i] == problemList[problemNum].answer[i]) //문제 생기는 부분 왤깜 // 답안의 길이와 동일할 때만 실행되게 해야 할 듯 
                    {
                        correct++;
                    }
                }
            }
                
            if (correct == problemList[problemNum].answer.Length)
            {
                scorePoint += problemList[problemNum].score;
                Debug.Log($"{problemNum}: {scorePoint}"); //마지막 번호가 안나옴??? 해결!
                correctOrIncorrect[problemNum] = true; // 이 배열을 씬이 넘어가도 유지되게 하기 
            }
        }
        
        PlayerPrefs.SetInt("Score", scorePoint); // PlaywePrefs에 점수 저장 
        PlayerPrefs.Save(); // 저장된 값을 디스크에 쓰기
        
        Debug.Log($"currentscore: {scorePoint}");
    }
}
