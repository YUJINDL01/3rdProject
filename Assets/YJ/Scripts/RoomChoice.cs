using UnityEngine;
using UnityEngine.UI;

public class RoomChoice : MonoBehaviour
{
    public GameObject chooseCanvas;
    public GameObject roomCreateCanvas;
   
    public void RoomCreat()
    {
        chooseCanvas.SetActive(false);
        roomCreateCanvas.SetActive(true);
    }

    public void RoomJoined()
    {
        chooseCanvas.SetActive(false);
        roomCreateCanvas.SetActive(false);
    }
    
}
