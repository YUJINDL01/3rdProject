using System;
using UnityEngine;

public class RoomData : MonoBehaviour
{
   public static RoomData Instance;

   private void Start()
   {
      Instance = this; 
   }
   
   // 확인 누르면 데이터 저장, 그리고 그 걸 바탕으로 문제, 인터페이스 조작 
}
