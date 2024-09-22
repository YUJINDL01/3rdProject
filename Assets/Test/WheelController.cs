using UnityEngine;
using UnityEngine.InputSystem;

public class WheelController : MonoBehaviour
{
    public void OnWheelMoved(InputAction.CallbackContext context)
    {
        Debug.Log($"OnWheelMoved : {context.ReadValue<Vector2>()}");
    }

    public void OnLeftPedal(InputAction.CallbackContext context)
    {
        Debug.Log($"OnLeftPedal : {context.ReadValue<float>()}");        
    }

    public void OnRightPedal(InputAction.CallbackContext context)
    {
        Debug.Log($"OnRightPedal : {context.ReadValue<float>()}");        
    }
}
