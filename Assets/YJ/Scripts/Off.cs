using System;
using UnityEngine;

public class Off : MonoBehaviour
{
  public GameObject startImage;

  void On()
  {
    startImage.SetActive(true);
  }

  public void OffButton()
  {
    startImage.SetActive(false);
  }
}
