using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class MenuObjectsActions : MonoBehaviour
{
    //Hovering
    public Color highlightColor;
    public AudioClip hoverAudio;
    public string objectName;
    public TextMeshProUGUI objectBoxLabel;

    //Spawning
    public GameObject _particleEffect;
    private bool isHovered = false;
    public AudioClip spawnAudio;
    public GameObject prefabToInstantiate;
    public InputActionReference rightTrigger;

    public GameObject tableCenterReferenceObject;
    
    Color currentColor;

    void Start()
    {
        currentColor = gameObject.GetComponent<MeshRenderer>().material.color;
    }


    private void OnTriggerEnter(Collider collider){
        
        GameObject rightControllerPointer = collider.gameObject;
        
        if (rightControllerPointer != null && isTouchingRightPointer(rightControllerPointer)){
            
            ChangeTextToHoveredObject();
            
            isHovered = true;

            AudioSource.PlayClipAtPoint(hoverAudio, transform.position);

            gameObject.GetComponent<MeshRenderer>().material.color = highlightColor;
        }

    }

    private void OnTriggerExit(Collider collider){
        GameObject menuObject = collider.gameObject;
        if (menuObject != null && isTouchingRightPointer(menuObject)){
            ChangeTextToDefault();
            isHovered = false;
            gameObject.GetComponent<MeshRenderer>().material.color = currentColor;
            
        }
    }

    private bool isTouchingRightPointer(GameObject gameObject){
        return (gameObject.name == "right_controller_pointer");
    }

    void Update()
    {
        if (isHovered && rightTrigger.action.triggered)
        {
            AudioSource.PlayClipAtPoint(spawnAudio, transform.position);
            SpawnObject();
        }
    }

    private void SpawnObject(){
        Vector3 tableCenter = tableCenterReferenceObject.transform.position;
        tableCenter.y += prefabToInstantiate.transform.localScale.y / 2; // Offset the Y position of the prefab so that it's on top of the table
        
        //Show particle effect
        GameObject particleEffect = Instantiate(_particleEffect,tableCenter, Quaternion.identity);
        particleEffect.GetComponent<ParticleSystem>().Play();
        
        //Create object
        Instantiate(prefabToInstantiate, tableCenter, Quaternion.identity);
    }

    private void ChangeTextToHoveredObject(){
        objectBoxLabel.text = "Object: " + objectName;
    }

    private void ChangeTextToDefault(){
        objectBoxLabel.text = "Select a shape";
    }
    
}
