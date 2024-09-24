using System;
using Fusion;
using UnityEngine;

public class ScoreManager : NetworkBehaviour
{
    public static ScoreManager Instance;

    public override void Spawned()
    {
        Instance = this;
    }

    public void AnswerCheck()
    {
        /*
         *  현재 문제의 번호에 해당하는 파일을 차아서 정답을 비교한다.
         * 1. 현재 문제의 번호를 가져온다.
         * 2. 정답을 비교한다.
         * 3. 화면에 반영
         * 4. 공유한다. 
         */
    }

}
