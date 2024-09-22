using System;
using UnityEngine;

public class Off : MonoBehaviour
{
  public GameObject startImage;

  public void OffButton()
  {
    startImage.SetActive(false);
  }
}
