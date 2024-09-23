using System.Linq;
using Fusion;
using UnityEngine;
using TMPro;

public class ProblemController : NetworkBehaviour
{
    private ProblemManager problemManager;

    private void Start()
    {
        problemManager = ProblemManager.Instance;

        /*
        if (Object.HasStateAuthority)
        {
            // 문제 출제 로직을 수정
        }
        */

        /*
        if (Runner.ActivePlayers.ToList().Count == 1) // 오직 호스트만 문제를 초기화함
        {
            InitializeProblems();
        }
        */
    }

    private void InitializeProblems()
    {
        Debug.Log("일단 문제 시작~");
        // 여기에서 원하는 문제 수를 설정
        int problemQuantity = 10; // 설정에서 가져올 수도 있음
        int problemNumber = 0;
        problemManager.InitializeProblems(problemQuantity, problemNumber);
        RPC_SetInitialProblem();
    }

    // RPC: 초기 문제를 모든 클라이언트에 동기화
    [Rpc]
    public void RPC_SetInitialProblem()
    {
        // 클라이언트에서 상태를 동기화
        problemManager.CheckCurrentNumber(problemManager.problemNumber);
    }

    //
    public void OnAnswerButtonClicked(int buttonIndex)
    {
        if (Object.HasStateAuthority)
        {
            problemManager.MClickedButton(buttonIndex);
            //RPC_SelectAnswer(buttonIndex);
        }
    }
    //
/*
    [Rpc]
    public void RPC_SelectAnswer(int buttonIndex)
    {
        problemManager.MClickedButton(buttonIndex);
    }
*/
    /*
    public void GoToNextProblem()
    {
        if (Object.HasStateAuthority)
        {
            problemManager.MMButton(1);
            RPC_MoveToNextProblem();
        }
    }

    [Rpc]
    public void RPC_MoveToNextProblem()
    {
        problemManager.MMButton(1);
    }

    public void GoToPreviousProblem()
    {
        if (Object.HasStateAuthority)
        {
            problemManager.MMButton(-1);
            RPC_MoveToPreviousProblem();
        }
    }

    [Rpc]
    public void RPC_MoveToPreviousProblem()
    {
        problemManager.MMButton(-1);
    }
    */
}