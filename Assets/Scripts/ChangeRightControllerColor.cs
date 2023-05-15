
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class ChangeRightControllerColor : MonoBehaviour
{
    public AudioClip _audioClipOnColorChange;
    public TextMeshProUGUI colorBoxLabel;
    public string colorName;
    private Color parentBlockColor;

    private void Start(){
        parentBlockColor = transform.parent.GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter(Collider collider){
        
        GameObject rightControllerPointer = collider.gameObject;
        
        if (rightControllerPointer != null && isTouchingRightPointer(rightControllerPointer)){
            
            ChangeBlockBackgroundColor(Color.white);
            
            AudioSource.PlayClipAtPoint(_audioClipOnColorChange, transform.position);    
            
            rightControllerPointer.GetComponent<MeshRenderer>().material.color = gameObject.GetComponent<MeshRenderer>().material.color;
            
            ChangeText();
        }
    }

    private void OnTriggerExit(Collider collider){
        ChangeBlockBackgroundColor(parentBlockColor);
    }

    private void ChangeBlockBackgroundColor(Color color)
    {
        transform.parent.GetComponent<Renderer>().material.color = color;
    }

    private void ChangeText(){
        colorBoxLabel.text = "Selected: " + colorName;
    }

    private bool isTouchingRightPointer(GameObject gameObject){
        return (gameObject.name == "right_controller_pointer");
    }

    public Color getColor(){
        return gameObject.GetComponent<MeshRenderer>().material.color;
    }
    
}
