using System;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{
   public GameObject redLight;
   public GameObject yellowLight;
   public GameObject greenLight;

   private void Start() // 반복되며 실행되도록
   {
      //  초기 상태 설정: 빨간불 켜기
      SetRedLight();
      StartCoroutine(TrafficLightManagerCycle());
   }

   private void SetRedLight()  // 빨간불 설정
   {
      redLight.SetActive(true);
      yellowLight.SetActive(false);
      greenLight.SetActive(false);
   }

   private void SetYellowLight() // 노란불 설정
   {
      yellowLight.SetActive(true);
      redLight.SetActive(false);
      greenLight.SetActive(false);
   }

   private void SetGreenLight() // 초록불 설정
   {
      greenLight.SetActive(true);
      redLight.SetActive(false);
      yellowLight.SetActive(false);
   }

   public System.Collections.IEnumerator TrafficLightManagerCycle() // 색 바뀌는 초 설정
   {
      while (true)
      {
         yield return new WaitForSeconds(4f); // 빨간불 4초
         SetYellowLight();
         yield return new WaitForSeconds(3f); // 노란불 3초
         SetGreenLight();
         yield return new WaitForSeconds(5f); // 초록불 5초
         SetRedLight();
      }
   }
   
   
   
   
}
