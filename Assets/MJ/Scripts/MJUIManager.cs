using System;
using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class MJUIManager : MonoBehaviour
{
    public GameObject leftBlinker;
    public GameObject rightBlinker;
    public GameObject wiper;
    public GameObject key;
    public GameObject emerygencyLights;
    public GameObject headLight;
    public GameObject lowLight;
    public GameObject highLight;
    public GameObject gear;
    public GameObject axel;
    public GameObject sideBreak;
    public GameObject bbreak;
    public GameObject seatBelt;

    public Button eButton;
    private void Awake()
    {
        leftBlinker.SetActive(false);
        rightBlinker.SetActive(false);
        wiper.SetActive(false);
        key.SetActive(false);
        headLight.SetActive(false);
        lowLight.SetActive(false);
        highLight.SetActive(false);
        
        axel.SetActive(false);
        bbreak.SetActive(false);
        gear.SetActive(false);
        sideBreak.SetActive(false);
        emerygencyLights.SetActive(false);
        seatBelt.SetActive(true);
        
    }

    public void LeftBlinker()
    {
        Debug.Log("좌깜불렸다!");
        leftBlinker.SetActive(true);
    }

    public void RightBlinker()
    {
        Debug.Log("우깜불렸다!");
        rightBlinker.SetActive(true);
    }

    public void Wiper()
    {
        Debug.Log("와이퍼 불렸다!");
        wiper.SetActive(true);
    }
    
    public void Key()
    {
        Debug.Log("키!");
        key.SetActive(true);
    }
    
    public void HeadLight()
    {
        Debug.Log("전조등빔!");
        headLight.SetActive(true);
       
    }

    public void EmerygencyLights()
    {
        Debug.Log("비상등빔!");
        emerygencyLights.SetActive(true);
        
       // eButton.onClick.AddListener(HideButton);
    }

    public void HighBeam()
    {
        Debug.Log("상향등빔!");
        highLight.SetActive(true);
    }

    public void LowBeam()
    {
        Debug.Log("하향등빔");
        lowLight.SetActive(true);
    }

    public void Gear()
    {
        Debug.Log("기어뿅!");
        gear.SetActive(true);
    }

    /*public static void P()
    {
        Debug.Log("주차함해볼까");
    }
    
    public static void R()
    {
        Debug.Log("후진간다");
    }
    
    public static void N()
    {
        Debug.Log("나는중립이양");
    }
    
    public static void D()
    {
        Debug.Log("다부숴");
    }*/
    
    public void Axel()
    {
        Debug.Log("속도함즐겨볼까");
    }
    
    public void Break()
    {
        Debug.Log("함만서봐");
    }

    public void SideBreak()
    {
        Debug.Log("막내브레이크");
    }

    public void SeatBelt()
    {
        Debug.Log("안전벨트단디매라잉");
    }

    void HideButton()
    {
        gameObject.SetActive(false);
    }
}
