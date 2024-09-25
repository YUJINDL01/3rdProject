using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class CartControllerTest : MonoBehaviour
{
    public enum State
    {
        WaitSeatBelt,
        SeatBelt,
        
        
        WaitReady,
        Ready,
        WaitOffReady,
        OffReady,
        
        
        WaitP,
        P,
        WaitR,
        R,
        WaitN,
        N,
        WaitD,
        D,
        
        WaitHeadLight,
        HeadLight,
        WaitOffHeadLight,
        OffHeadLight,
        WaitUpLight,
        UpLight,
        WaitOffUpLight,
        OffUpLight,
        WaitDownLight,
        DownLight,
        WaitOffDownLight,
        OffDownLight,
        
        
        WaitLeftSignal,
        LeftSignal,
        WaitOffLeftSignal,
        OffLeftSignal,
        WaitRightSignal,
        RightSignal,
        WaitOffRightSignal,
        OffRightSignal,
        WaitWarning,
        Warning,
        WaitOffWarning,
        OffWarning,
       
        WaitWiper,
        Wiper,
        WaitOffWiper,
        OffWiper,
        
        WaitSideBreak,
        SideBreak,
        WaitOffSideBreak,
        OffSideBreak,
        
        WaitStart,
        Start,
        
        
        WaitAccelPedal,
        AccelPedal,
        WaitBreakPedal,
        BreakPedal,
    }

    public enum FourState
    {
        NoSelect,
        Gear,
        HeadLight,
        Signal,
        Wiper,
    }
    ///// 차량 내부 조작 기능 테스트
    
    /*
    1. 안전벨트 버튼 (버튼 누르면 3초 동안 UI 나타나기)
    2. 시험 시작 안내 음성
    3. 5초 내에 시동 (버튼 누르면 브레이크 시동 걸리는 소리 재생)
    4. P → D → P 브레이크 (버튼 P D 두 개)
    5. 전조등 상향등 하향등 (그냥 버튼 누르면 불 켜지는 효과)
    6. 방향지시등 좌깜 키고 끄기 (버튼 누르면 깜빡이는 소리 재생)
    7. 와이퍼 키고 끄기 (와이퍼 움직이는 UI 나타내기)
    8. P 해제 후 사이드 풀고 좌깜 키고 출발 악셀 안 눌러도 앞으로 감 *10초 내 출발
    기어 D 로 두면 무조건 조금씩 움직이게 구현
    
    음성 안내 중 버튼 조작 시 감점(5초 내에 실행하시오 이때 실행 안 하면 감점인 코드 있으면 될 듯)
    음성 재생 중에 다른 거 여러 개가 동시에 재생돼야 함
    -안내 음성 듣고 그 행동 실행했을 때 맞고 틀리고 판단하여 감점과 실격 여부를 나누고, 
    */
    
    // 특정 버튼 누르면 특정 이미지 UI 띄우기

    // 타이머 5초 동안 뜨고 5초 안에 안 하면 감점
    // 처음 시작 음성 듣고 안전벨트 말고 다른 거 누르면 바로 실격
    // 시작할 때 사이드 당겨져 있고 나중에 누르면 풀어지는 기능으로 돼야 함

    public TMP_Text seatBeltHeper;
    public TMP_Text timerText;
    public TMP_Text penaltyText;
    public TMP_Text scoreText;
    public TMP_Text outText;

    public int score = 100;
    
    public float timer = 5.0f;
    private bool timerActive = false;
    
    

    public GameObject seatBeltImage;
    public GameObject wiperImage;
    public GameObject upLightImage;
    public GameObject downLightImage;


   // ?? AudioSource를 한번만 가져와서 클립을 여러 개를 연결할 수 없을까???
   // 일단 때려박고 내일 물어보자
   // headLightSound = gearSound 기어사운드 헤드라이트 사운드로 박음

    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;

    public AudioClip bbiSound;
    
    public AudioClip readySound1;
    public AudioClip readySound2;
    public AudioClip signalSound;
    public AudioClip headLightSound;
    public AudioClip upDownLightSound;
    public AudioClip sideBreakSound;
    public AudioClip seatBeltSound;


    public AudioClip readySoundGo;
    public AudioClip randomSoundGo;  // 4개 중 2개 랜덤사운드 안내
    public AudioClip gearSoundGo;
    public AudioClip headLightSoundGo;
    public AudioClip upLightSoundGo;
    public AudioClip downLightSoundGo;
    public AudioClip signalSoundGo;
    public AudioClip leftSignalSoundGo;
    public AudioClip rightSignalSoundGo;
    public AudioClip offSignalSoundGo;
    public AudioClip wiperSoundGo;
    public AudioClip offWiperSoundGo;
    public AudioClip startSoundGo;


    public AudioClip minusSoundGo;
    public AudioClip outSoundGo;
    
    
    


    public State state = State.WaitSeatBelt; // 이 값을 가지고 시작하게 변수 지정
    public FourState fourState1 = FourState.NoSelect;  // 이 값을 가지고 시작하게 변수 지정 
    public FourState fourState2 = FourState.NoSelect;  // 이 값을 가지고 시작하게 변수 지정 

    private Coroutine timerCoroutine;

    private bool isOkay;
    private bool isReady2;
    private bool signalLeft;
    private bool signalRight;
    private bool sideBreakLock = true;  // true 안 돼 있으면 false로 시작하니까 bool이 암것도 없는 상태에서 true하면 ㅂ뭐 무슨 코드가 실행되게 이런 식이라서

    public Animator wiperAnimator;

    public Animator wiperAnimator2;
     // 5초 안에 하라는 안내 음성 후 타이머 떠야 함 - 코루틴으로 음성 실행되는 밑에 코드 있으면 될 듯
     /// <summary>
     /// //////////////////////////////////////////////////////////////////////////////////////
     /// </summary>


     public void MinusScoreCount(int amount)
     {
         score += amount; // 매개변수로 -5 하고 있으니까
         scoreText.text = "점수: " + score;
         Debug.Log("현재 점수"+score);

         if (score < 80)
         {
             TestOut(); // 실격 처리
         }
     }
     

     public void MinusScoreTextUI()
     {
         scoreText.text = "점수: " + score; // UI 텍스트 업데이트
     }

     private void TestOut()
     {
         outText.text = "기준 점수 미달, 실격입니다.";
         audioSource4.clip = outSoundGo;
         audioSource4.loop = false;
         audioSource4.Play();
         
         outText.gameObject.SetActive(true);
         Debug.Log("실격입니다.");
         
         Invoke("GoFailScene", 3f);

         
     }

     private void GoFailScene()
     {
         SceneManager.LoadScene("Fail"); // 불합격씬
     }
         

     


     #region Timer


     // 타이머가 실행돼야 될 때 켜고 뭐 특정행동을 하고 나면 꺼지는 코드가 있어야 됨

     IEnumerator TimerTextCoroutine(float time, int minusScore,
             Func<bool> isComplete) // 델리게이트 통해서 타이머 코루틴 만들기 델리게이트 => 메소드 저장하는 공간
     {
         isOkay = false;
         timer = time;
         timerText.gameObject.SetActive(true);
         penaltyText.gameObject.SetActive(false);

         while (true) // 타이머가 5초 동안 줄어드는 반복문
         {
             // 종료 조건을 충족했을때
             if (isComplete())
             {
                 timerText.gameObject.SetActive(false);
                 isOkay = true;
                 break;
             }

             timer -= Time.deltaTime;
             // 타이머가 완료되었을 때
             if (timer <= 0f)
             {
                 timer = 0f;
                 UpdateTimerText();

                 ///// 스코어 감소 minusScore;
                 timerText.gameObject.SetActive(false);
                 penaltyText.text = $"제한시간 내 지시사항 불이행 감점입니다. \n {minusScore}"; // 실행한 마이너스스코어가 들어감
                 PlaySound(minusSoundGo, false);
                 penaltyText.gameObject.SetActive(true); // 타이머 종료 후 감점 메시지 표시
                 yield return new WaitForSeconds(2f);
                 penaltyText.gameObject.SetActive(false); // 타이머 종료 후 감점 메시지 표시

                 MinusScoreCount(minusScore);

                 break;
             }

             UpdateTimerText();
             yield return null;
         }
     }

     private void UpdateTimerText() // 이건 타이머 텍스트 UI를 띄움
     {
         timerText.text = $"{timer:F2}"; // 타이머 텍스트 업데이트
         
     }

     
     #endregion
     

     // 정답 시 재생돼야 하는 상황 1 => 지금 만들어둔 게 정답 시 재생돼야 되는 걸로 재생되는 거고 오답 시 재생되어야 되는 시퀀스가 또 있어야 됨
     // 오답 시 재생돼야 하는 상황 2 => 오답 시엔 그냥 다음 상황에 재생되어야 되는 소리만 재생되고 기능을 눌렀을 시 적용되는 사운드는 ㄴㄴ
     
     // bool 타입으로 뭐 정답 오답 가려서 한 코루틴 내에서 두 가지의 갈래로 적용되게
     
     
    #region Coroutine

    IEnumerator SeatBeltSoundCorutine() // 시동 켜라 음성 재생하기
    {
        yield return StartCoroutine(PlaySoundProcess(seatBeltSound, false)); // 달칵 소리 재생
        seatBeltHeper.gameObject.SetActive(false);
        yield return StartCoroutine(PlaySoundProcess(readySoundGo, false)); // 시동 켜라 음성 재생
        yield return StartCoroutine(PlaySoundProcess(bbiSound, false));

        state = State.WaitReady;
        yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.Ready));

        StartCoroutine(ReadySoundCorutine());
    }

    //private bool M() => state == State.Ready;
    // {
    //     return state == State.Ready;
    // }
    
    IEnumerator ReadySoundCorutine() // 시동 코루틴 만들기
    {
        if (isOkay) // 정답일 때만 실행되게
        {
            PlaySound(readySound1, false);
            yield return new WaitForSeconds(2f); // 시동 소리 끝나면  ??? 이게 왜 자꾸 딜레이되노
            audioSource.Stop();
            
            // 시동 2번 소리는 안내음성 나오는 시간과 관계없이 1번 소리 끝나면 바로 루프되게 재생해야 됨
            audioSource2.clip = readySound2;
            audioSource2.loop = true;
            audioSource2.Play();
        }

        yield return StartCoroutine(PlaySoundProcess(randomSoundGo, false)); // 시동 1번 소리 기다리고 끝나면 바로 재생되게 
      

        // 4 of 2 선택하기
        // 1, 2, 3, 4
        // int i = Random.Range(1, 5); // 5는 빼고 1234 중에 하나 추출
        fourState1 = (FourState)Random.Range(1, 5); // int 형 i를 fourState 타입으로 변환 //FourState.Wiper 이런 식으로 테스트 해 볼 거 입력
        
        
        // 두 번째 뽑는 건 중복 안 돼야 돼서 반복문과 조건문이 필요
        for (int limit = 100; limit > 0; limit--) // 이건 그냥 100번 반복해라 뭐 100번 인엔 나오겠지
        {
            fourState2 = FourState.Wiper; //(FourState)Random.Range(1, 5);
            if (fourState2 != fourState1)  // 만약에 뽑은 값이 중복되면 다시 뽑기 즉 진짜 조건문 뽑아라~~~  
            {
                break;
            }
        }

        switch (fourState1) // 네 개 다 실행되는데 네 개 중에 하나 뽑은 거 실행할 거야
        {
            case FourState.Gear:
                yield return StartCoroutine(GearTest()); // 기어 상태 일 때 기어 코루틴을 실행하도록
                break;
            case FourState.HeadLight:
                yield return StartCoroutine(HeadLightTest());
                break;
            case FourState.Signal:
                yield return StartCoroutine(SignalTest());
                break;
            case FourState.Wiper:
                yield return StartCoroutine(WiperTest());
                break;
        }
        
        switch (fourState2) // 네 개 중에 하나 뽑은 거 실행할 거야인데 forState1이랑 2랑 중복 안 되게 하는 코드가 있으니까 이거는 그냥 실행만 하고 중복은 안 될 거임
        {
            case FourState.Gear:
                yield return StartCoroutine(GearTest()); // 기어 상태 일 때 기어 코루틴을 실행하도록
                break;
            case FourState.HeadLight:
                yield return StartCoroutine(HeadLightTest());
                break;
            case FourState.Signal:
                yield return StartCoroutine(SignalTest());
                break;
            case FourState.Wiper:
                yield return StartCoroutine(WiperTest());
                break;
        }

        yield return StartCoroutine(PlaySoundProcess(startSoundGo, false));
        yield return StartCoroutine(PlaySoundProcess(bbiSound, false));

        state = State.WaitStart;
        yield return StartCoroutine(TimerTextCoroutine(10f, -5, () => state == State.Start));
        
        

        Debug.Log("기능 조작 시험 끝");

    }
    
    
    

    // 각각의 상태 4개에 맞게 작동하는 코루틴
    // 순차적으로 잘 작동하는지만 보게 1 2 3 4 디버그 로그해 보고 작성하기
    // for문 밑에서 순서대로 2개를 실행하는 코루틴을 만들기
    
    
    /// 이제 안에 넣어서 실행돼야 하는 코드 작성
    /// 기어는 tts 하나만 실행되고 이후에 동작 실행하는 거
    /// 전조등은 5 6 7 순서대로 실행되고 이후에 동작 실행하는 거
    /// 방향지시등은 8 9 10 11 순서대로 실행되고 이후에 동작 실행하는 거
    /// 와이퍼는 와이퍼 12 13 순서대로 실행되고 동작 실행하는 거
    
     
    IEnumerator GearTest()
    {
        Debug.Log("1");
        yield return StartCoroutine(PlaySoundProcess(gearSoundGo, false));
        yield return StartCoroutine(PlaySoundProcess(bbiSound, false));
        
        state = State.WaitD;
        yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.D));
        if (isOkay)
        {
            state = State.WaitP;
            yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.P));; // D가 되면 P가 될 때까지 기다리는 거
        }
    }

    // !!! 전조등 상향등 하향등은 중복으로 클릭되면 안 되고, 끄고 키는 기능이 있어야 됨 / 전조등 상향등 소리가 안 나고 하향등 소리만 남
    IEnumerator HeadLightTest()
    {
        Debug.Log("2");
        yield return StartCoroutine(PlaySoundProcess(headLightSoundGo, false));
        yield return StartCoroutine(PlaySoundProcess(bbiSound, false));

        state = State.WaitHeadLight;
        yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.HeadLight));
        if (isOkay)
        {
            yield return StartCoroutine(PlaySoundProcess(headLightSound, false));
        }

        yield return StartCoroutine(PlaySoundProcess(upLightSoundGo, false));
        yield return StartCoroutine(PlaySoundProcess(bbiSound, false));

        state = State.WaitUpLight;
        yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.UpLight));
        if (isOkay)
        {
            yield return StartCoroutine(PlaySoundProcess(upDownLightSound, false));
        }
        
        yield return StartCoroutine(PlaySoundProcess(downLightSoundGo, false));
        yield return StartCoroutine(PlaySoundProcess(bbiSound, false));

        state = State.WaitDownLight;
        yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.DownLight));
        if (isOkay)
        {
            yield return StartCoroutine(PlaySoundProcess(upDownLightSound, false));
        }
        
        state = State.WaitOffDownLight;
        yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.OffDownLight));
        if (isOkay)
        {
            yield return StartCoroutine(PlaySoundProcess(upDownLightSound, false));
        }
        
        downLightImage.gameObject.SetActive(false);
        upLightImage.gameObject.SetActive(false);
    }

    IEnumerator SignalTest()
    {
         Debug.Log("3");
         yield return StartCoroutine(PlaySoundProcess(signalSoundGo, false));
       

         yield return StartCoroutine(PlaySoundProcess(leftSignalSoundGo, false));
         yield return StartCoroutine(PlaySoundProcess(bbiSound, false));

         
         
         state = State.WaitLeftSignal;
         yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.LeftSignal));// 순서대로 진행되는 거니까 좌깜 먼저
         if (isOkay)
         {
             yield return StartCoroutine(PlaySoundProcess(upDownLightSound, false));
         }

         
         yield return StartCoroutine(PlaySoundProcess(offSignalSoundGo, false));
         yield return StartCoroutine(PlaySoundProcess(bbiSound, false));
         
         state = State.WaitOffLeftSignal;
         yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.OffLeftSignal));
         if (isOkay)
         {
             yield return StartCoroutine(PlaySoundProcess(upDownLightSound, false));
         }
         
         
         yield return StartCoroutine(PlaySoundProcess(rightSignalSoundGo, false));
         yield return StartCoroutine(PlaySoundProcess(bbiSound, false));

         state = State.WaitRightSignal;
         yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.RightSignal));
         if (isOkay)
         {
             yield return StartCoroutine(PlaySoundProcess(upDownLightSound, false));
         }


         
         yield return StartCoroutine(PlaySoundProcess(offSignalSoundGo, false));
         yield return StartCoroutine(PlaySoundProcess(bbiSound, false));

         
         state = State.WaitOffRightSignal;
         yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => state == State.OffRightSignal));
         if (isOkay)
         {
             yield return StartCoroutine(PlaySoundProcess(upDownLightSound, false));
         }
    }

    IEnumerator WiperTest()
    {
        Debug.Log("4");
        
        yield return StartCoroutine(PlaySoundProcess(wiperSoundGo, false));
        yield return StartCoroutine(PlaySoundProcess(bbiSound, false));
        state = State.WaitWiper;
        yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => wiperAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move")));
        if (isOkay)
        {
            yield return StartCoroutine(PlaySoundProcess(offWiperSoundGo, false));
            yield return StartCoroutine(PlaySoundProcess(bbiSound, false));
            
            state = State.WaitOffWiper;
        
            //while은 뒤가 false일 때까지 진행 , until은 true일 때까지
            //while은 뒤가 false일 때까지 진행 , until은 true일 때까지
            yield return StartCoroutine(TimerTextCoroutine(5f, -5, () => wiperAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle")));
            
        }
        
    }
    

    #endregion

    
    
    ///// 장치조작 기능 설명하기
    /// 설명하고 기어변속으로 넘어가고
    /// 그 후에 전조등 방향지시등 깜빡이 와이퍼 네 개 중에 두 개 랜덤으로 나오기
    /// 출발신호 받고 출발
    /// 버튼 한 번 더 누르면 초기화

    
    
    
    // 기어변속, 전조등(상향등, 하향등), 방향지시등(좌깜, 우깜), 와이퍼(켜기)
    // 이거 중에 랜덤으로 두 개 출제
    // 4개 중 2개 안내음성 후 두 개 랜덤으로 출제
    // 특정 소리가 재생되고 난 후에 랜덤으로 두 매서드만 실행되게끔






    /// 밑에 클래스들은 다 온클릭이벤트로 누르면 실행되게 했으니까
    /// 음성만 랜덤으로 4개 중에 2개를 출제하면 되겠네
    /// 하나의 음성이 나오면 특정 행동을 실행하고 다음 음성이 나오게 (틀려도 다음 문제가 나오니까 음성이 실행되고 나면 어떤 버튼을 누르든 다음 문제로 넘어가면 됨)

    public NewCarControl newCarControl;  // 차 움직이는 스크립트 가져오기
    public void SideBraeak()
    {
        
            PlaySound(sideBreakSound, false);
        //sidebreak true false 바꿔놓아서 시작할 때 끈 채로 시작하고
        if (sideBreakLock == true) // 락이 걸려 있는 상태 즉 멈춰있는 상태 즉 스크립트를 켜야 됨
        {
            sideBreakLock = false;
            newCarControl.enabled = true;

        }
        else                                      // 얘를 실행하면? 멈추는 걸 안 해야 됨 즉 누르면 차량 움직이는 스크립트를 꺼야 됨
        {
            if (state != State.WaitSideBreak)
            {
                MinusScoreCount(-5);
                PlaySound(minusSoundGo, false);
                Debug.Log("사이드브레이크 할 때가 아님");
                return;
            }

            sideBreakLock = true;
            
            Debug.Log("사이드");
            state = State.SideBreak;

            newCarControl.enabled = false;
        }
        
    }

    public void SeatBelt()
    {
        if (state != State.WaitSeatBelt) // 안전벨트 할 상황이 아닐 때
        {
            MinusScoreCount(-5);
            PlaySound(minusSoundGo, false);
            Debug.Log("안전벨트 할 때가 아님"); // 다른 상태일 때 안전벨트 누르면 안전벨트 할 상황 아니라고 하는 거임
            // 
            return; // 안전벨트 매고 잇으면 그냥 종료
        }

        Debug.Log("안전벨트");
        state = State.SeatBelt;
        
        seatBeltImage.gameObject.SetActive(true);
        Invoke("HideImage", 1.5f);

        StartCoroutine(SeatBeltSoundCorutine()); // 달칵 소리 난 후에 시동 켜라 음성 재생
    }

    public void Warning()
    {
        if (state != State.WaitWarning)
        {
            MinusScoreCount(-5);
            PlaySound(minusSoundGo, false);
            Debug.Log("비깜 상태가 아님");
            return;
        }
        
        Debug.Log("비깜");
        state = State.Warning;
        PlaySound(signalSound, true);
    }

    public void HeadLight()  //전조등 상향등 하향등 세 아이콘은 동시에 클릭 x 좌우 깜빡이도 동시에 클릭 x 일단 누르면 꺼지는 코드 먼저
    {
        PlaySound(headLightSound, false);
        
        if (downLightImage.gameObject.activeSelf) // activeSelf는 게임오브젝트가 켜지고 껴지고 판단하는 거 켜져 있는 경우 액티브
        {
            // if (state != State.WaitOffHeadLight)
            // {
            //     Debug.Log("전조등 상태가 아님");
            //     return;
            // }

            state = State.OffDownLight;
            downLightImage.gameObject.SetActive(false);
        }
        else
        {
            
            if (state != State.WaitHeadLight)
            {
                MinusScoreCount(-5);
                PlaySound(minusSoundGo, false);
                Debug.Log("전조등 켤 상태가 아님");
                return;
            }
            
            state = State.HeadLight;
            downLightImage.gameObject.SetActive(true);
           
        }
        
    }

    public void DownLight()
    {
        PlaySound(upDownLightSound, false);

        if (downLightImage.gameObject.activeSelf)
        {
            if (state != State.WaitOffDownLight)
            {
                MinusScoreCount(-5);
                PlaySound(minusSoundGo, false);
                Debug.Log("하향등 켤 상태가 아님");
                return;
            }

            state = State.OffDownLight;
            downLightImage.gameObject.SetActive(false);
        }
        else
        {
            if (state != State.WaitDownLight)
            {
                Debug.Log("하향등");
                return;
            }

            state = State.DownLight;
            downLightImage.gameObject.SetActive(true);
            upLightImage.gameObject.SetActive(false);
        }


    }

    public void UpLight()
    {
        
        PlaySound(upDownLightSound, false);
        
        if (upLightImage.gameObject.activeSelf)
        {
            /*if (state != State.WaitOffUpLight)
            {
                Debug.Log("상향등 켤 타임 아님");
                return;
            }*/
            MinusScoreCount(-5);
            PlaySound(minusSoundGo, false);
            upLightImage.SetActive(false);
        }
        else
        {
            if (state != State.WaitUpLight)
            {
                Debug.Log("상황등");
            }
            
            state = State.UpLight;
            upLightImage.gameObject.SetActive(true);
            downLightImage.gameObject.SetActive(false);
        }
    }
    
    public void LeftSignal()
    {
        
        PlaySound(upDownLightSound, false);
        if (signalLeft == true)
        {
            signalLeft = false;
            audioSource3.Stop();

            state = State.OffLeftSignal;

        }
        
        else
        {
            
            if (state != State.WaitLeftSignal)  /// 꺼진 상태에서 실행되는 코드를 짠 거니까 꺼져있을 때 트루가 돼서 작동되는 코드를 써야 됨 반대로 켜진 상태 즉 트루일 땐 그걸 끄는 코드
            {
                MinusScoreCount(-5);
                PlaySound(minusSoundGo, false);
                Debug.Log("좌깜 켤 타임 아님");
                return;
            }
            
            state = State.LeftSignal;
            
            Debug.Log("좌깜");
            signalLeft = true;
            
            audioSource3.clip = signalSound;
            audioSource3.loop = true;
            audioSource3.Play();
        }
    }

    public void RightSignal()
    {
        PlaySound(upDownLightSound, false);
        if (signalRight == true)
        {
            signalRight = false;
            audioSource3.Stop();

            state = State.OffRightSignal;

        }
        else
        {
            if (state != State.WaitRightSignal)
            {
                MinusScoreCount(-5);
                PlaySound(minusSoundGo, false);
                Debug.Log("우깜 켤 떄 아님");
                return;
            }
            
            signalRight = true;
            
            Debug.Log("우깜");
            state = State.RightSignal;
            audioSource3.clip = signalSound;
            audioSource3.loop = true;
            audioSource3.Play();
        }
        
    }

    public void Ready()
    {
       
        if (isReady2 == true)  // 위에 코드가 시동 안 켤 때 키라고 작동한 코드니까 트루로  // 시동2 코루틴 자체를 끄기
        {
            isReady2 = false;  // 버튼 눌렀을 때 트루 펄스 왔다 갔다
            audioSource2.Stop();
            StopAllCoroutines();

            state = State.OffReady;

        }
        else
        {
            if (state != State.WaitReady)
            {
                MinusScoreCount(-5);
                PlaySound(minusSoundGo, false);
                Debug.Log("시동 걸 상태가 아님"); // 시동 걸면 걍 종료
                return;
            }
            
            isReady2 = true;
            Debug.Log("시동");
            state = State.Ready;
        }
    }

    public void Wiper()
    {
        bool isIdleState = wiperAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle");
        if (isIdleState)
        {
            if (state != State.WaitWiper)
            {
                MinusScoreCount(-5);
                PlaySound(minusSoundGo, false);
                Debug.Log("와이퍼 켤 때 아님");
                return;
            }
            
            Debug.Log("와이퍼 켬");
            state = State.Wiper;

            wiperAnimator.SetTrigger("ToMove");
            wiperAnimator2.SetTrigger("ToMove");
        }
        else
        {
            if (state != State.WaitOffWiper)
            {
                MinusScoreCount(-5);
                PlaySound(minusSoundGo, false);
                Debug.Log("와이퍼 끌 때 아님");
                return;
            }
            
            Debug.Log("와이퍼 끔");
            state = State.OffWiper;

            wiperAnimator.SetTrigger("ToIdle");
            wiperAnimator2.SetTrigger("ToIdle");
            
        }
    }

    public void P()
    {
        if (state != State.WaitP)
        {
            MinusScoreCount(-5);
            PlaySound(minusSoundGo, false);
            Debug.Log("P누르는 타임 아님");
            return;
        }
        
        Debug.Log("P");
        state = State.P;
        
        PlaySound(headLightSound, false);
    }
    
    public void R()
    {
        if (state != State.WaitR)
        {
            MinusScoreCount(-5);
            PlaySound(minusSoundGo, false);
            Debug.Log("R 누르는 타임 아님");
            return;
        }
        
        Debug.Log("R");
        state = State.R;
            
        PlaySound(headLightSound, false);
    }
    
    public void N()
    {
        if (state != State.WaitN)
        {
            MinusScoreCount(-5);
            PlaySound(minusSoundGo, false);
            Debug.Log("N 누르는 타임 아님");
            return;
        }
        
        Debug.Log("N");
        state = State.N;
        
        PlaySound(headLightSound, false);
    }
    
    public void D()
    {
        if (state == State.WaitStart)  // waitStart일 때 Start로 상태를 바꿔줌
        {
            state = State.Start;
            return;
        }
     
        if (state != State.WaitD)
        {
            MinusScoreCount(-5);
            PlaySound(minusSoundGo, false);
            Debug.Log("D 누르는 타임 아님");
            return;
        }
   
        Debug.Log("D");
        state = State.D;
        StartCoroutine(GearTest());
        
        PlaySound(headLightSound, false);
    }

    #region Internal

    private void HideImage() // 사진 사라지게 하는 코드
    {
        seatBeltImage.gameObject.SetActive(false);
    }

    private void PlaySound(AudioClip clip, bool loop) // 소리 재생하는 메소드 (클립은 다른데 기능은 같으니까 하나로 묶고 클립만 바꾸면 실행되도록)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }
    
    private IEnumerator PlaySoundProcess(AudioClip clip, bool loop) // 플레이사운드 안의 클립이 재생되고 또 소리가 끝나길 기다렸다가 밑에 일드 코루틴을 새로 하면 그 코루틴에 있는 클립을 다른 재생하는 코루틴
    {
        PlaySound(clip, loop);
        yield return new WaitForSeconds(clip.length);
    }
    
    #endregion
}
