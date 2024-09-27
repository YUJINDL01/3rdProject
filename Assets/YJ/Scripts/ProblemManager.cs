using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Fusion;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.Video;
using Random = UnityEngine.Random;

// [DefaultExecutionOrder(-100)]
public class ProblemManager : MonoBehaviour
{
 
   public TMP_Text problemText;
   public List<Button> optionButtons;
   public List<TMP_Text> buttonTexts;
   public Image problemImage; //?? image 와 sprite의 차이점 ?? 게임 오브젝트와 에셋의 차이 
   public TMP_Text explanationText; 
   public VideoPlayer problemVideo;
   //public List<Button> answerButtons;
   public Button[] moveButton;
   
   [HideInInspector]
   public int problemNumber;

   public int[,] answerSheet;
   private bool[] clickedButtonArray = new bool[5];
   
   private Color ClickColor = Color.blue;
   private Color originalColor = Color.white;
   
   public List<int> clickedButtonNum;

   private bool[] selectedPart;
   private int problemQuantity;

   private string mode; 
   
   public static ProblemManager Instance;
   
   [HideInInspector]
   public List<Problem> problemsList;
   //매개변수를 사용하는 것과 전역변수를 사용하는 것의 차이?? -> 의도에 따라, 매개변수를 사용하는 경우 다른 클래스에서도 조작 가능 
   //매개 변수 너무 많음 꼭 필요한 건지 생각해 볼 것 
   
   
   private void Start()
   {
      Instance = this;
      //버튼의 text에 접근하기
      //optionButtons = optionButtons.Find<"TMP(TMP)">();
      //TMP_Text[] buttonText = optionButtons.GetComponentInChildren<TMP_Text>();
      Debug.Log($"@@@: {RoomManager.Instance.problemNum}"); // 참여하기로 들어왔을 떄 최대 문제 수를 공유 받지 못해 생긴 일 
      
      problemQuantity = RoomManager.Instance.problemNum;
      
      selectedPart = RoomManager.Instance.clickPart;
      mode = RoomManager.Instance.mode;
      
      problemNumber = 0;
      InitializeProblems(problemQuantity, problemNumber);
   }
   
   public void InitializeProblems(int problemQuantity, int problemNumber)
   {
      //Random.InitState(1000);
      //랜덤으로 문제 뽑고 저장 //초기에 1번만 실행 (setting) 
      MRandoms(problemQuantity);
      
      //문제수에 맞게 답안지 버튼 정리 // 초기 1회 
      //MAnswerButton(problemQuantity);

      MAnswerSheet();
      
      //현재 출제하는 문제 번호 확인 // 지속적으로 반복
      CheckCurrentNumber(problemNumber);
      
      //출제 // 지속적으로 반복 
      MProblems(problemNumber);
      
      answerSheet = new int[problemQuantity, clickedButtonArray.Length];
   } // 초기 설정을 위한 거랑 지속적으로 필요한 것을 나누고 전역변수와 매개변수에 대해 다시 생각해볼 것  
   
   private List<Problem> MRandoms(int problemQuantity)
   {
         problemsList = new List<Problem>();

         switch (mode)
         {
            case "TestMode":
               int[] problemRange = new int[7]{0, 549, 680, 780, 865, 965, 1000};
               int[] numbers = new int[6]; 
                  
               switch (problemQuantity)
               {
                  case 5:
                     numbers = new int[] { 1, 1, 1, 1, 1, 0 };
                     break;
                  case 10:
                     numbers = new int[] { 4, 2, 2, 1, 1, 0 }; // 총합 25
                     Debug.Log(numbers[0]);
                     break;
                  case 20:
                     numbers = new int[] { 9, 2, 3, 3, 2, 1 }; // 총합 50점
                     Debug.Log(numbers[0]);
                     break;
                  case 40:
                     numbers = new int[] { 17, 4, 7, 6, 5, 1 }; // 총합 100점  17,4,7,6,5,1
                     Debug.Log(numbers[0]);
                     break;
               }
                  
                  // EProblemType.i 인 type을 numbers[i] 만큼 리스트에 랜덤으로 추가
               for (int i = 0; i < numbers.Length; i++)
               {
                  int j = 0;

                  while (j < numbers[i]) // 번호가 존재할 경우에만 추가 
                  {
                     int no = Random.Range(problemRange[i] + 1, problemRange[i + 1]); //번호  
                     Problem
                        problems = Resources
                           .Load<Problem>($"{no}"); // 그 번호의 문제  // Resources.load -> addressable asset 사용 추천 

                     var containProblem = problemsList.Find(p => p.number == no);

                     if (problems != null && containProblem == null) // 만약에 no 가 problem 안에 있으면 추가 아니면 다시 
                     {
                        problemsList.Add(problems);
                        Debug.Log($"Type : {problems.type}, Number : {no}, {containProblem}");
                        j++;
                     }
                  }
               } 
               break;
            
            case "SurvivalMode": 
               break;
            
            case "QuizMode":

               /*
               for (int i = 0; i < selectedPart.Length; i++)
               {
                  if (selectedPart[i] == true)
                  {
                     // 풀고 싶다고 한 문제분야의 
                     EProblemType type = (EProblemType)(i);
                     string problemType = type.ToString();
                     Debug.Log($"problemType: {problemType}");
                     
                  }
               }
               */
               
               problemRange = new int[7]{0, 549, 680, 780, 865, 965, 1000};
               numbers = new int[6]; 
                  
               switch (problemQuantity)
               {
                  case 5:
                     numbers = new int[] { 1, 1, 1, 1, 1, 0 };
                     break;
                  case 10:
                     numbers = new int[] { 4, 2, 2, 1, 1, 0 }; // 총합 25
                     Debug.Log(numbers[0]);
                     break;
                  case 20:
                     numbers = new int[] { 9, 2, 3, 3, 2, 1 }; // 총합 50점
                     Debug.Log(numbers[0]);
                     break;
                  case 40:
                     numbers = new int[] { 17, 4, 7, 6, 5, 1 }; // 총합 100점  17,4,7,6,5,1
                     Debug.Log(numbers[0]);
                     break;
               }
                  
               // EProblemType.i 인 type을 numbers[i] 만큼 리스트에 랜덤으로 추가
               for (int i = 0; i < numbers.Length; i++)
               {
                  int j = 0;

                  while (j < numbers[i]) // 번호가 존재할 경우에만 추가 
                  {
                     int no = Random.Range(problemRange[i] + 1, problemRange[i + 1]); //번호  
                     Problem
                        problems = Resources
                           .Load<Problem>($"{no}"); // 그 번호의 문제  // Resources.load -> addressable asset 사용 추천 

                     var containProblem = problemsList.Find(p => p.number == no);

                     if (problems != null && containProblem == null) // 만약에 no 가 problem 안에 있으면 추가 아니면 다시 
                     {
                        problemsList.Add(problems);
                        Debug.Log($"Type : {problems.type}, Number : {no}, {containProblem}");
                        j++;
                     }
                  }
               } 
               break;

         }
         
         Debug.Log("randoms end");
         
        // ScoreManager.Instance.ProblemSave();
      return problemsList; 
   }

   /*
   private void MAnswerButton(int problemQuantity)
   {
      for (int i = 0; i < answerButtons.Count; i++)
      {
         if(i < problemQuantity)
         {
            answerButtons[i].gameObject.SetActive(true);
            
         }
         else
         {
            answerButtons[i].gameObject.SetActive(false);
         }
      }
      
   }
*/
   public void CheckCurrentNumber(int problemNumber)
   {
      Debug.Log($"현재 문제 인덱스 : {this.problemNumber}");
      
      if (problemNumber == 0)
      {
         problemNumber = 0; 
         
         moveButton[0].gameObject.SetActive(false);
         moveButton[1].gameObject.SetActive(true);
         moveButton[2].gameObject.SetActive(false);
         Debug.Log("처음 문제");
      }
      else if (problemNumber == problemsList.Count - 1)
      {
         problemNumber = problemsList.Count - 1;
      
         moveButton[0].gameObject.SetActive(true);
         moveButton[1].gameObject.SetActive(false);
         moveButton[2].gameObject.SetActive(true); 
         
         Debug.Log("시험 종료");
      }
      else
      {
         moveButton[0].gameObject.SetActive(true);
         moveButton[1].gameObject.SetActive(true);
         moveButton[2].gameObject.SetActive(false);
      }
      
      MProblems(problemNumber);
   }
   
   private void MProblems(int probleNumber) 
   {
      Debug.Log($"Index: {problemNumber}");
      // 해당 번호의 문제 
      Problem problem = problemsList[problemNumber];
         
      //문제 
      MProblemTitle(problemNumber, problemsList[problemNumber]);
      //선택지 
      MProblemOption(problemsList[problemNumber]);
      //이미지 
      MProblemImage(problemsList[problemNumber]);
      //이미지 설명 
      MProblemExplanation(problemsList[problemNumber]);
      //비디오 
      MProblemVideo(problemsList[problemNumber]);
      
      Debug.Log($"{problem.type}의 {problem.number}을 출제하였습니다.");
      
      Debug.Log("문제출제 끝");
   } 

   private void MProblemTitle(int problemNumber, Problem problem)
   {
      problemText.text = $"{problemNumber+1}. {problem.problem}"; 
   }

   private void MProblemOption(Problem problem)
   {
      int optionsLength = problem.options.Length;
      int buttonsLength = optionButtons.Count;
      
      //선택지 
      for (int i = 0; i < buttonsLength; i++)
      {
         Image buttonImage = optionButtons[i].GetComponent<Image>(); 
         if (i < optionsLength)
         {
            optionButtons[i].gameObject.SetActive(true);
            buttonTexts[i].gameObject.SetActive(true);
         }
         else
         {
            optionButtons[i].gameObject.SetActive(false);
            buttonTexts[i].gameObject.SetActive(false);
         }

         buttonImage.color = originalColor;
      }
      
      //선택지 출력 
      for (int j = 0; j < problem.options.Length; j++)
      {
         string s = problem.options[j];
         
         if (s != null)
         {
            buttonTexts[j].text = $"{s}";
         }
      }
   }

   private void MProblemImage(Problem problem)
   {
      if (problem.image != null)
      {
         problemImage.gameObject.SetActive(true);
         problemImage.sprite = problem.image; // 이미지 // 종횡비 맞게 설정하는 방법 혹은 종횡비 맞게 자르는 방법 
      }
      else
      {
         problemImage.gameObject.SetActive(false);
      }
   }

   private void MProblemExplanation(Problem problem)
   {
      if (problem.explanation != null && problem.explanation.Length > 0) // !string.IsNullOrEmpty(problem.explanation[0]) 는 string을 비교하는 것, 비교할 스트링을 가져올 때 존재하지 않으면 에러
      {
         foreach (var explain in problem.explanation)
         {
            explanationText.gameObject.SetActive(true);
            explanationText.text = $"* {explain}\n";
         }
      }
      else
      {
         explanationText.gameObject.SetActive(false);
      }
   }

   private void MProblemVideo(Problem problem)
   {
      if (problem.video != null)
      {
         problemVideo.gameObject.SetActive(true);
         problemVideo.clip = problem.video; // 동영상 //동영상 재생하는 방법 
      }
      else
      {
         problemVideo.gameObject.SetActive(false);
      }
   }
   
   /*
   public void MButton(int buttonNumber)
   {
      //MAnswerButtonChange();
      problemNumber = buttonNumber;
      CheckCurrentNumber(problemNumber);
   }
*/
   
   public void MMButton(int buttonNumber)
   {
      //MAnswerButtonChange();
      problemNumber += buttonNumber;
      CheckCurrentNumber(problemNumber);
   }

   public void MClickedButton(int num)
   {
      int clickedButtonNumber = 0; 
      Image buttonImage = optionButtons[num - 1].GetComponent<Image>(); // 버튼이 image 컴퍼넌트에서 색상 바꿈
      
      for (int i = 0; i < clickedButtonArray.Length; i++)
      {
         if (clickedButtonArray[i] == true)
         {
            clickedButtonNumber++;
         }
      }

      if (clickedButtonArray[num-1] == true)
      {
         buttonImage.color = originalColor;
         
         clickedButtonArray[num-1] = false;
      }
      else
      {
         if (clickedButtonNumber < problemsList[problemNumber].answer.Length)
         {
            buttonImage.color = ClickColor;
            
            clickedButtonArray[num-1] = true;
         }
      }
   }

   public void MAnswerSheet()
   {
      clickedButtonNum.Clear();
      
      // true인 값만 확인하여서 답 번호 저장 
      for (int j = 0; j < clickedButtonArray.Length; j++)
      {
         
         if (clickedButtonArray[j] == true)
         {
            answerSheet[problemNumber, j] = j + 1;
            Debug.Log($"No: {problemNumber+1}, Answer: {j + 1}"); // 몇 번에 답이 몇 이니 ~ // 보이는 번호랑 문제랑 동일하게 해 놈 // 답 비교할 땐 수정 
            
            clickedButtonNum.Add(j+1);
         }
      }
      
      //저장 후 초기화 
      for (int i = 0; i < clickedButtonArray.Length; i++) // foreach는 일기 전용이라서 값을 변경하거나 할 수는 없음 
      {
         clickedButtonArray[i] = false;
      }
      
      //이거 실행하면 위에 버튼에 답안 저장된거 표시되게 하기 
   }
   
   

    /*
   private void MAnswerButtonChange()
   {
      //답안 저장 후 문제 번호에 색깔 변하게 하기 
      //현재 번호의 색은 다른 걸로 표현되게 하기 
      int answerLength = 0;
      //보류 !
   }
   */
    
   /*
   private void Answer()
    {

    }

    private void Score()
    {
       int[] scores = { 2, 3, 3, 3, 2, 5 };
    }
    */
}
