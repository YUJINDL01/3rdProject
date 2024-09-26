using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    /*public void OnRightPedal(InputAction.CallbackContext context)
    {
        Debug.Log($"OnRightPedal : {context.ReadValue<Vector2>()}");
    }
    public void OnRightBack(InputAction.CallbackContext context)
    {
        Debug.Log($"OnRightBack : {context.ReadValueAsButton()}");
    }*/

    public CartControllerTest carControllerTest;
    public NewCarControl newCarControl;
    
    public void LeftBlinker(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.LeftSignal();
            Debug.Log($"LeftBlinker : {context.ReadValueAsButton()}");
        }
    }
    
    public void RightBlinker(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.RightSignal();
            Debug.Log($"RightBlinker : {context.ReadValueAsButton()}");
        }
    }
    
    public void EmerygencyLight(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.Warning();
            Debug.Log($"EmerygencyLight : {context.ReadValueAsButton()}");
        }
            
    }
    
    public void Wifer(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.Wiper();
            Debug.Log($"Wifer : {context.ReadValueAsButton()}");
        }
    }
    
    public void Key(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.Ready();
            Debug.Log($"key : {context.ReadValueAsButton()}");
        }
    }
    
    public void Headlight(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.HeadLight();
            Debug.Log($"Headlight : {context.ReadValueAsButton()}");
        }
          
    }
    
    public void LowBeam(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.DownLight();
            Debug.Log($"LowBeam : {context.ReadValueAsButton()}");
        }
           
    }
    
    public void HighBeam(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
           carControllerTest.UpLight(); 
           Debug.Log($"HighBeam : {context.ReadValueAsButton()}");
        }
            
    }
    
    public void P(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.P();
            Debug.Log($"P : {context.ReadValueAsButton()}");
        }
            
    }
    
    public void R(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.R();
            Debug.Log($"R : {context.ReadValueAsButton()}");
        }
        
    }
    
    
    public void N(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.N();
            Debug.Log($"N : {context.ReadValueAsButton()}");
        }
           
    }
    
    public void D(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.D();
            Debug.Log($"D : {context.ReadValueAsButton()}");
        }
    }
    
    public void Axel(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        value = 1f - (value + 1f) * 0.5f;

        newCarControl.Accel(value);
        // Debug.Log($"Axel : {context.ReadValue<float>()}");
    }
    
    /*//밸류임
    public void SideBreak(InputAction.CallbackContext context)
    {
        Debug.Log($"SideBreak : {context.ReadValue<float>()}");
    }*/
    
    //버튼
    public void SideBreak(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            carControllerTest.SideBraeak();
            Debug.Log($"SideBreak : {context.ReadValueAsButton()}");
        }
    }
    
    public void Break(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<float>();
        value = (value + 1f) * 0.5f;
        
        newCarControl.Break(value);
        // Debug.Log($"Break : {context.ReadValue<float>()}");
    }
    
    public void Handle(InputAction.CallbackContext context)
    {
        newCarControl.Handle(context.ReadValue<float>());
        // Debug.Log($"Handle : {context.ReadValue<float>()}");
    }
    public void SeatBelt(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            carControllerTest.SeatBelt();
            Debug.Log($"Seatbelt : {context.ReadValueAsButton()}");
        }
           
    }
    
}
