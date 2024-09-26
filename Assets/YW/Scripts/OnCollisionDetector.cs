
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OnCollisionDetector : MonoBehaviour
{
   
    // Collider 설정하고
    // On Collision Enter, OnTriggerEnter 통해서 작성

    public AudioSource audioSource;
    public AudioClip stoneSound;
    
    public TMP_Text collisionText; // 불러올 텍스트 변수 지정
    
    public float timeThreshold = 5f; // 체크하려는 시간 (초 단위)
    private float timeInside = 0f;   // 트리거 안에 머문 시간
    private bool isInside = false;   // 객체가 트리거 안에 있는지 여부

    private void Start()
    {
        if (collisionText != null)  // 텍스트가 비어있지 않을 떄
        {
            collisionText.text = "";  // 게임 시작 시 텍스트 비워두기
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("YellowLine")) // 만약 노란선이랑 부딪치면
        {
            Debug.Log("차선 이탈");
            collisionText.text = "차선 이탈, -15점"; // 차선 이탈 Text 띄우기
            Invoke("ClearText", 3f); // 2초 뒤에 텍스트를 지우는 함수 호출
        }
        else if (other.gameObject.CompareTag("ParkSensor")) // 주차 검지선
        {
            Debug.Log("검지선 접촉");
            collisionText.text = "주차 검지선 접촉, -10점";
            Invoke("ClearText", 3f);
        }
        // 적색 신호 받을 시에만 실행 되도록 -> 선 두 개 깔아놓고 RedLight 선이랑 GreenLight 선 나눠서 껐다가 켜지게 인식하게 해도 될 듯
        else if (other.gameObject.CompareTag("RedLine")) // 신호위반 감지 출발선
        {
            Debug.Log("신호 위반");
            collisionText.text = "신호 위반 실격입니다";
            Invoke("ClearText", 3f);
        }
        else if (other.gameObject.CompareTag("ParkingLine")) // 주차선 밟았을때 통과
        {
            Debug.Log("주차 확인");
            collisionText.text = "확인 되었습니다.";
            Invoke("ClearText", 3f);
        }
        else if (other.gameObject.CompareTag("UPTest"))
        {
            Debug.Log("언덕 입니다.");
            isInside = true; // 들어왔는지 확인
            timeInside = 0f; // 시간 초기화
            collisionText.text = "3초이상 정지 후 출발 하십시오.";
            Invoke("ClearText", 3f);
            
        }
        
      
        
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("UPTest"))
        {
            Debug.Log("언덕입니다");
            timeInside += Time.deltaTime;
            
            if (timeInside >= timeThreshold)
            {
                Debug.Log("확인되었습니다.");
            }

        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stone"))  // 연석 탑승
        {
            Debug.Log("연석 탑승");
            collisionText.text = "바퀴가 연석에 접촉할 시 실격입니다.";
            audioSource.PlayOneShot(stoneSound);
            
            Invoke("ClearText", 2f);
        }
        
        
        
    }


    private void ClearText()
    {
        if (collisionText != null)
        {
            collisionText.text = ""; // 텍스트를 빈 문자열로 설정하여 지우기
            SceneManager.LoadScene("Fail");
        }
    }
    
    
    
    
    
    
    
    
    
    // Switch 써서 각 Case별로 나눠서 태그에 해당하는 UI 띄우기
    // trigger 통해서 나와야 하는 태그들
    // "안전벨트 미착용 시 실격입니다"
    // "바퀴가 연석에 접촉할 시 실격입니다." o
    // "신호 위반 실격입니다." ㅁ
    
    
    // "차선 이탈 -15점" o 
    
    // 돌발 시 급정지 미이행 -10점
    // 가속 미이행 -10점
    // (주차)검지선 접촉 -10점, 주차브레이크 미이행 -10점 o , x
    
    // 음성지시 미종료 시 차량 조작 -5점
    
    // 과속 -3점 속도 n 이상 받으면 과속으로 측정 -> 악셀 키(f) 하나 더 만들어서  
  
   
    
    
    
    
    
}
