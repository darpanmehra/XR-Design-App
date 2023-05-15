using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomColor : MonoBehaviour
{
    public AudioClip _audioClipOnColorChange;
    public TextMeshProUGUI colorBoxLabel;
    //public string colorName;
    private Color parentBlockColor;

    private void Start(){
        gameObject.GetComponent<MeshRenderer>().material.color = GetRandomColor();
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
        gameObject.GetComponent<MeshRenderer>().material.color = GetRandomColor();
    }

    private void ChangeBlockBackgroundColor(Color color)
    {
        transform.parent.GetComponent<Renderer>().material.color = color;
    }

    private void ChangeText(){
        colorBoxLabel.text = "Randomized Color";
    }

    private bool isTouchingRightPointer(GameObject gameObject){
        return (gameObject.name == "right_controller_pointer");
    }

    public Color getColor(){
        return gameObject.GetComponent<MeshRenderer>().material.color;
    }
    
    private Color GetRandomColor()
    {
        float r = UnityEngine.Random.Range(0f, 1f);
        float g = UnityEngine.Random.Range(0f, 1f);
        float b = UnityEngine.Random.Range(0f, 1f);

        return new Color(r, g, b);
    }
}
