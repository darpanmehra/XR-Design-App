using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotateMenu : MonoBehaviour
{
    public InputActionReference inputReference = null;
    private float rotationSpeed = 120.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 joystickInput = inputReference.action.ReadValue<Vector2>();
        float horizontalInput = joystickInput.x;
        GameObject grabPoint = gameObject.transform.GetChild(0).gameObject;
        if (horizontalInput != 0)
        {
            grabPoint.transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
    }
    
}
