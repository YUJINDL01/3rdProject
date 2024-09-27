
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class OnCollisionDetector : MonoBehaviour
{
   
    // Collider 설정하고
    // On Collision Enter, OnTriggerEnter 통해서 작성

    public AudioSource audioSource;

    public AudioClip bbiSound;
    public AudioClip passSound;
    public AudioClip okSound;
    public AudioClip dolbal;
    
    
    public TMP_Text collisionText; // 불러올 텍스트 변수 지정
    
    public bool isFail;

    public CartControllerTest carControllerTest;

    public Collider Warn1;
    public Collider Warn2;
    public Collider Warn3;
    public Collider Warn4;
    public MJUIManager mjUIManager;
        
    private String[] tags = { "Warn1", "Warn2", "Warn3", "Warn4"};
    private string _randomTag;
    public void Off()
    {
        audioSource.Stop();
        collisionText.text = "";
    }
    public string GetRandomTag()
    {
        int randomIndex = Random.Range(0, tags.Length);
        return tags[randomIndex];
    }
/// <summary>
/// 4개중에 지금 하나 뽑아서
/// 태그 확인해서 만약에 태그1이면 태그1이 실행되게.
/// </summary>

    private void Start()
    {
        if (collisionText != null)  // 텍스트가 비어있지 않을 떄
        {
            collisionText.text = "";  // 게임 시작 시 텍스트 비워두기
        }
        _randomTag = GetRandomTag();
        Debug.Log("선택된 태그: " + _randomTag);
        
        Warn1.enabled = false;
        Warn2.enabled = false;
        Warn3.enabled = false;
        Warn4.enabled = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("선택된 태그: " + _randomTag);
        
        if (other.gameObject.CompareTag("YellowLine")) // 만약 노란선이랑 부딪치면
        {
            Debug.Log("차선 이탈");
            collisionText.text = "차선 이탈, -15점"; // 차선 이탈 Text 띄우기
            carControllerTest.PlaySound(bbiSound, false);
            isFail = false;
            carControllerTest.MinusScoreCount(-15);
            Invoke("ClearText", 3f); // 2초 뒤에 텍스트를 지우는 함수 호출
        }
        else if (other.gameObject.CompareTag("ParkSensor")) // 주차 검지선
        {
            Debug.Log("검지선 접촉");
            collisionText.text = "주차 검지선 접촉, -10점";
            carControllerTest.PlaySound(bbiSound, false);
            carControllerTest.MinusScoreCount(-10);
            isFail = false;
            Invoke("ClearText", 3f);
        }
        // 적색 신호 받을 시에만 실행 되도록 -> 선 두 개 깔아놓고 RedLight 선이랑 GreenLight 선 나눠서 껐다가 켜지게 인식하게 해도 될 듯
        else if (other.gameObject.CompareTag("RedLine")) // 신호위반 감지 출발선
        {
            Debug.Log("신호 위반");
            isFail = true;
            collisionText.text = "신호 위반 실격입니다";
            Invoke("ClearText", 3f);
        }
        else if (other.gameObject.CompareTag("ParkingLine")) // 주차선 밟았을때 통과
        {
            Debug.Log("주차 확인");
            isFail = false;
            collisionText.text = "확인되었습니다.";
            carControllerTest.PlaySound(okSound, false);
            Invoke("ClearText", 3f);
        }
        else if (other.gameObject.CompareTag("UpTest1"))
        {
            Debug.Log("언덕입니다.");
            isFail = false;
            collisionText.text = "3초 이상 정지 후 출발하십시오.";
            carControllerTest.PlaySound(bbiSound, false);
            Invoke("ClearText", 2f);

            StartCoroutine(Timer());
        }
        else if (other.gameObject.CompareTag("Speed1"))
        {
            Debug.Log("가속구간1");
            isFail = false;
            collisionText.text = "20km/h 이상 주행하십시오.";
            Invoke("ClearText", 3f);
        }
        else if (other.gameObject.CompareTag("Speed2"))
        {
            Debug.Log("가속구간2");
            isFail = false;
            collisionText.text = "확인되었습니다.";
            Invoke("ClearText", 3f);
        }
        else if (other.gameObject.CompareTag("GoalLine"))
        {
            Debug.Log("합격입니다");
            isFail = false;
            carControllerTest.PlaySound(bbiSound, false);
            collisionText.text = "합격입니다.";
            Invoke("PlayPassSound", 1f);
            Invoke("Pass", 3f);
        }

        if (_randomTag == "Warn1")
        {
            Warn1.enabled = true;
            if(other.gameObject.CompareTag("Warn1"))
            {
                Debug.Log("warn1");
                isFail = false;
                
                mjUIManager.EmerygencyLights();
                Dolbal();
                StartCoroutine(WarningTimer());
            }
        }
        else if(_randomTag == "Warn2")
        {
            Warn2.enabled = true;
            if(other.gameObject.CompareTag("Warn2"))
            {
                Debug.Log("warn2");
                isFail = false;
                mjUIManager.EmerygencyLights();
                Dolbal();
                StartCoroutine(WarningTimer());
            }
        }
        else if(_randomTag == "Warn3")
        {
            Warn3.enabled = true;
            if(other.gameObject.CompareTag("Warn3"))
            {
                Debug.Log("warn3");
                isFail = false;
                mjUIManager.EmerygencyLights();
                Dolbal();
                StartCoroutine(WarningTimer());
            }
        }
        else if(_randomTag == "Warn4")
        {
            Warn4.enabled = true;
            if(other.gameObject.CompareTag("Warn4"))
            {
                Debug.Log("warn4");
                isFail = false;
                mjUIManager.EmerygencyLights();
                Dolbal();
                StartCoroutine(WarningTimer());
            }
        }
        
        
        
    }
    


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stone"))  // 연석 탑승
        {
            Debug.Log("연석 탑승");
            collisionText.text = "바퀴가 연석에 접촉할 시 실격입니다.";
            
            Invoke("ClearText", 2f);
        }
        
        
        
    }


    private void ClearText()
    {
        if (collisionText != null)
        {
            collisionText.text = ""; // 텍스트를 빈 문자열로 설정하여 지우기
            if (isFail)
            {
                SceneManager.LoadScene("Fail");
            }
        }
    }

    private void Pass()
    {
        SceneManager.LoadScene("Success");
    }

    private void PlayPassSound()
    {
        carControllerTest.PlaySound(passSound, false);
    }


    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("언덕 확인되었습니다.");
        collisionText.text = "확인되었습니다.";
        Invoke("ClearText", 3f);
    }

    IEnumerator WarningTimer()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log("비상등 확인되었습니다.");
        collisionText.text = "확인되었습니다.";
        mjUIManager.emerygencyLights.SetActive(false);
        CartControllerTest cartControllerTest = FindAnyObjectByType<CartControllerTest>();
        carControllerTest.PlaySound(null, false);
        Invoke("ClearText", 3f);
    }


    private void Dolbal()
    {
        audioSource.volume = 03f;
        audioSource.PlayOneShot(dolbal);
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
