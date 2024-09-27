using UnityEngine;
using UnityEngine.SceneManagement;

public class ErrorNote : MonoBehaviour
{
  public GameObject result;
  public GameObject rank;
  public GameObject anlyButton;
  public GameObject retryButton;

  public void errorNote()
  {
    result.SetActive(true);
    anlyButton.SetActive(false);
    rank.SetActive(false);
    //retryButton.SetActive(true);
    retryButton.SetActive(true);
  }

  public void StartAIQuestion()
  {
    SceneManager.LoadScene("Written_TEST 1");
  }
}