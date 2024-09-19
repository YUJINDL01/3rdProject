using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{
    public Button[] modeButton = new Button[3];
    public GameObject problemPartBlock;
    
    private Color clickColors = new Color(0f/255f, 146f/255f, 200f/255f, 255f/255f);
    private Color originalColors = new Color(16f/255f, 186f/255f, 250f/255f, 255f/255f);
    
    Image[] buttonImage = new Image[3];
    
    public void GameMode(string modeName)
    {
        for (int i = 0; i < modeButton.Length; i++)
        {
            buttonImage[i] = modeButton[i].GetComponent<Image>();
        }

        GameModeCreate(modeName);
    }

    private void GameModeCreate(string modeName)
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
                break;
            case "SurvivalMode":
                problemPartBlock.gameObject.SetActive(false);
                num = 1;
                break;
            
            case "QuizMode":
                problemPartBlock.gameObject.SetActive(false);
                num = 2;
                break;
                
        }
        
        Debug.Log("색 바꿈");
        for (int i = 0; i < buttonImage.Length; i++)
        {
            if (i == num)
            {
                buttonImage[i].color = clickColors;
            }
            else
            {
                buttonImage[i].color = originalColors;
            }
        }
    }
}
