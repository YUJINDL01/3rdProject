using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Problem", menuName = "Problem/Create New Problem")]
public class Problem : ScriptableObject
{
    
    //문제 공통 요소 
    public int number;
    public EProblemType type;
    public string problem;
    public string[] options;
   
    //문제 비 공통 요소 
    public Sprite image;
    public string[] explanation;
    public VideoClip video;
   
    //채점 공통요소   
    public int[] answer;
    public string solution;
    public int score;
   
}


public enum EProblemType
{
    S1,
    S2,
    Picture,
    Illustration,
    Signal,
    Video
}

//(1~ 680)
// 1번 문장형 4지 1답 (number,problem, options, answer,solution) / 1~ 549 // 17문제 2점 
// 2번 문장형 4지 2답 (number,problem, options, answer,solution) //4문제 3점 

//
// 3번 안전표지형 4지 1답 (number, problem, image, option, answer, solution) / 866 ~ 966 // 5문제 2점 
// 4번 사진형 5지 2답 (number, problem, image, option, answer, solution) / 681 ~ 780 // 7문제 3점 
// 5번 일러스트형 5지 2답 (number, problem, image, option, answer, solution) / 781 ~ 865 // 6문제 3점 

// 6번 동영상형 4지 1답 혹은 4지 2답 (number, problem, video, option, answer, solution) / 966 ~ 1000 // 1문제 5점 

// 1답형 - 2점 , 2답형 - 3점, 동영상 문제 - 5점 
//1100 x 748

//1. 용도에 맞게 scriptableObject 제작 
//2. 문제 20개 출제하기
//+ 3. 답안지를 통해 왔다갓다 기능 추가 