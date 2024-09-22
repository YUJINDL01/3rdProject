using System;
using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{
    public Button[] modeButton = new Button[3];
    public GameObject problemPartBlock;

    //public Scrollbar problemNum;
    
    private Color _clickColors = new Color(0f/255f, 146f/255f, 200f/255f, 255f/255f);
    private Color _originalColors = new Color(16f/255f, 186f/255f, 250f/255f, 255f/255f);
    
    Image[] _buttonImage = new Image[3];

    private void Start()
    {
        for (int i = 0; i < modeButton.Length; i++)
        {
            _buttonImage[i] = modeButton[i].GetComponent<Image>();
        }
    }

    public void GameMode(string modeName)
    {
        GameModeManager(modeName);
    }

    private void GameModeManager(string modeName)
    {
        int num = 0;
        
        Debug.Log($"GameMode : {modeName}");
        
        switch (modeName)
        {          
            case "TestMode":
                // 각 모드에 해당하는 버튼과 숫자 활성화 그리고 각 정보를 저장후 씬을 넘어가도 있게 활성화 
                // TestMode.Instance.Setting(); // 겟 컴펀너트 안 해도 돼서 좋음 ?? 일단 모드 설정은 따로 저장해야하면 파괴되지 않게 하거나. 다른 방법을 구해볼것
                problemPartBlock.gameObject.SetActive(true);
                num = 0; 
                //스크롤바의 단위 조절 10, 20, 40 그냥 문제수 입력으로 갈까? 
                //이 모드에서 출제 분야쪽도 컨트롤 추가 필요 
                break;
            case "SurvivalMode":
                problemPartBlock.gameObject.SetActive(false);
                num = 1;
                // 문제 무제한으로 출제 -> 최대 40문제 
                break;
            
            case "QuizMode":
                problemPartBlock.gameObject.SetActive(false);
                num = 2;
                // 최대 40문제
                break;
        }
        
        Debug.Log("색 바꿈");
        for (int i = 0; i < _buttonImage.Length; i++)
        {
            if (i == num)
            {
                _buttonImage[i].color = _clickColors;
            }
            else
            {
                _buttonImage[i].color = _originalColors;
            }
        }
    }
}
