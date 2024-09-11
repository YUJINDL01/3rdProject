using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerSheet : MonoBehaviour
{
    // 1. 답안을 넘길때 번호와 함께 순서에 맞게 답안 저장 -> 현재 문제 순서 필요 와 답변 필요함 (완료)
    // 2. 답안 저장 ... -> 저장해야함 -> 버튼 눌릴때 
    // 추후 점수 부분과 연결 -> 답안이 답과 같은 지 봐야할 듯 -> 내가 사용한 문제 - MRandom의 리턴 값 List<Problem>

    private GameObject problem;
    private ProblemManager problemManager;
    private List<Problem> problemsListthis;
    private int problemQuantity;
    
    private int problemNumberthis;
    private int changeNumber;
    private int[][] answerSheet;

    private List<Button> answerButton;
    private bool clickedButton = false;
    private bool[] clickedButtonArray;
    private void Start()
    {
        //초기 설정 
        MSetting(); 
        //답안 저장 
        StartCoroutine(OnSave());
    }
    
    IEnumerator OnSave()
    {
        // 정답 저장
        yield return new WaitUntil(() => this.problemNumberthis != changeNumber); // 문제를 옮기면 

        problemNumberthis = changeNumber;
        answerSheet[problemNumberthis - 1][1] = 1; // //answerNember[1,1]; //문제번호 -1 인 인덱스에 answernember을 저장 

    }
    
    public void MAnswerNumber(int num)
    {
        if (!clickedButton)
        {
            int j = 0; 
            answerSheet[problemNumberthis][j] = num; // 몇 번에 어떻게 받아야 할까 일단 시각화 부터 
        }

    }
   
    private void MSetting()
    {
        problem = GameObject.Find("ProblemManager"); //게임 오브젝트를 찾는 중 
        
        problemManager = problem.GetComponent<ProblemManager>(); // 해당 게임오브젝트 내에서 ProblemManger 형식의 컴포넌트 찾아오는 중 
        
        problemsListthis = problemManager.problemsList; // 변수에 접근 
        problemNumberthis = problemManager.problemNumber; // 변수에 접근 

        changeNumber = 0; //
        
        problemQuantity = 10; // 다음엔 대기방에서 넘겨 받기 
        answerSheet = new int[problemQuantity][]; // 문제당 자신의 정답개수에 맞는  //

        for (int i = 0; i < answerSheet.Length; i++) // 
        {
            answerSheet[i] = new int[problemsListthis[i].answer.Length]; // 문제의 번호에 맞는 길이의 배열 만듬 
        }

        answerButton = problemManager.answerButtons;//바뀐 정보를 가지고 오고 싶은데 어떻게 하지? 
        
        Debug.Log("초기 설정 완료");
    }
    
}
    /*
    private void MAnswerSave()
    {
        answerSheet[problemQuantity] = answernember;
    }
    private void Update()
    {
        if (problemNumberthis != changeNumber)
        {
            problemNumberthis = changeNumber;
            //MAnswerSave();
        }
    }

*/
