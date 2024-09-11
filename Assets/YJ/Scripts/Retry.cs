using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Retry : MonoBehaviour
{
    public Button retryButton;

    public void MRetry()
    {
        SceneManager.LoadScene("End");
    }
}
