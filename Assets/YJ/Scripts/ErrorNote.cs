using UnityEngine;

public class ErrorNote : MonoBehaviour
{
  public GameObject result;
  public GameObject anlyButton;
  public GameObject retryButton;

  public void errorNote()
  {
    result.SetActive(true);
    anlyButton.SetActive(false);
    //retryButton.SetActive(true);
    retryButton.SetActive(true);
  }
}
