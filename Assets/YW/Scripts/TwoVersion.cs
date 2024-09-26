using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoVersion : MonoBehaviour
{
   public void ToTwoVersion()
   {
      SceneManager.LoadScene("TwoVersionScene");
   }
   
   public void ToWheelMode()
   {
      SceneManager.LoadScene("WheelMode");
   }

   public void ToButtonMode()
   {
      SceneManager.LoadScene("ButtonMode");
   }
}
