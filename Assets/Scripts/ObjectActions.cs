using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class ObjectActions : MonoBehaviour
{
    //Collision
    public AudioClip collisionAudio;

     //Hovering
    public AudioClip hoverAudio;
    private Color controllerPointerColor;

    //Coloring
    private bool isHovered = false;
    public AudioClip colorChangeAudio;
    public InputActionReference rightTrigger;
    Color currentColor;

    //Resize Object
    public InputActionReference scaleInputReference = null;

    private float scaleSpeed = 0.8f; 
    private float maxScale = 4f; 
    private float minScale = 0.5f; 
    private Vector3 initialScale;
    private float currentScale;

    void Start()
    {
        currentColor = gameObject.GetComponent<MeshRenderer>().material.color;
        initialScale = transform.localScale;
        currentScale = initialScale.x;
    }


    private void OnTriggerEnter(Collider collider){
        GameObject controllerPointer = collider.gameObject;
        
        if (controllerPointer != null && isTouchingRightPointer(controllerPointer)){
            
            isHovered = true;
            controllerPointerColor = GetControllerPointerColor(controllerPointer);

            AudioSource.PlayClipAtPoint(hoverAudio, transform.position);
            
            if (controllerPointerColor != null){
                gameObject.GetComponent<MeshRenderer>().material.color = controllerPointerColor;
            }
            
        }

    }

    private void OnTriggerExit(Collider collider){
        GameObject controllerPointer = collider.gameObject;
        if (controllerPointer != null && isTouchingRightPointer(controllerPointer)){
            isHovered = false;
            gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
        }
    }

    private bool isTouchingRightPointer(GameObject controllerPointer){
        return (controllerPointer.name == "right_controller_pointer");
    }

    void Update()
    {
        //Update Object Color
        if (isHovered && rightTrigger.action.triggered)
        {
            if (controllerPointerColor != null){
                currentColor = controllerPointerColor;
                AudioSource.PlayClipAtPoint(colorChangeAudio, transform.position);
                gameObject.GetComponent<MeshRenderer>().material.color = controllerPointerColor;
            }
        }

        // Scale only if the object is grabbed
        
         if (gameObject.GetComponent<XRGrabInteractable>().isHovered){

            float joystickVerticalFactor = scaleInputReference.action.ReadValue<Vector2>().y;
            float scalingFactor = scaleSpeed * joystickVerticalFactor * Time.deltaTime;
            currentScale = Mathf.Clamp(currentScale + scalingFactor, initialScale.x * minScale, initialScale.x * maxScale);
            
            
            Vector3 newScale = new Vector3(currentScale, currentScale, currentScale);
            gameObject.transform.localScale = newScale;    
        }
         
         
        
    }

    private Color GetControllerPointerColor(GameObject controllerPointer){
        return controllerPointer.GetComponent<MeshRenderer>().material.color;
    }

    public Color GetObjectColor(){
        return currentColor;
    }

    private void OnCollisionEnter(Collision collision){
        
        if (collisionAudio){
            AudioSource.PlayClipAtPoint(collisionAudio, transform.position);
        }
        
    }

    
}
