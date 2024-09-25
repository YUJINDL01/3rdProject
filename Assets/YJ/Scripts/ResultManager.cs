using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ResultManager : MonoBehaviour
{

    private List<Problem> problemList;
    private List<string> answerOXSheet;
    private List<string> answerType;
    
    public static ResultManager Instance;
    void Start()
    {
        Instance = this; // 싱글톤
        DontDestroyOnLoad(this.gameObject); // 파괴하지마 
       
        answerType = new List<string>(); //무슨 타입인지 저장 
    }

    public void AnswerOXSave()
    {
        problemList = ProblemManager.Instance.problemsList;
        answerOXSheet = ScoreManager.Instance.answerOX; // 정답 여부 저장 
        answerType.Clear();
        
        for (int i = 0; i < problemList.Count; i++)
        {
            Debug.Log("답안 저장할 준비 끝냄");
            var type = problemList[i].type;
            //answerTypes.Add(type); // 분야 저장
            
            string typeString = type.ToString();
            answerType.Add(typeString); // 답의 분야 저장
         
            Debug.Log("필요한 거 다 저장함");
        }
     
        Debug.Log($"answerType : {string.Join(", ", answerType)}");
        
        Debug.Log("답안지 만들어 볼게예");
    }
    
    private void AnswerSheet()
    {
        // 오답노트 -> 문제 추천
    }

    private void ResultAnaly()
    {
        // 결과분석(오갹형)
    }

}
