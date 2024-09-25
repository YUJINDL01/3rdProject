using TMPro;
using UnityEngine;

public class Rank : MonoBehaviour
{
    public TMP_Text posText;
    public TMP_Text playerText;
    public TMP_Text scoreText;
    
    public void Set(int pos, string nickname, int score)
    {
        posText.text = $"{pos}";
        playerText.text = nickname;
        scoreText.text = $"{score}";
    }
}
