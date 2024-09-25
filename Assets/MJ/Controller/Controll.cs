using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controll : MonoBehaviour
{
    public void OnRightPedal(InputAction.CallbackContext context)
    {
        Debug.Log($"OnRightPedal : {context.ReadValue<Vector2>()}");
    }
    public void OnRightBack(InputAction.CallbackContext context)
    {
        Debug.Log($"OnRightBack : {context.ReadValueAsButton()}");
    }
}
