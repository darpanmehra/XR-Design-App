using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ScaleObject : MonoBehaviour
{
    public InputActionReference scaleReference = null;
    public InputActionProperty scaleProperty;
    public InputActionProperty triggerProperty;
    public AudioClip soundSFX;
    public Vector2 scaleUp = Vector2.one;

    Vector3 originalScale;

    void Awake()
    {
//        scaleReference.action.started += Scale;
        originalScale = transform.localScale;

    }
    private void OnDestroy()
    {
//        scaleReference.action.started -= Scale;
    }

    // Update is called once per frame
    void Update()
    {
        scaleUp = scaleProperty.action.ReadValue<Vector2>();

        if (gameObject.GetComponent<XRGrabInteractable>().isHovered)
        {
            if (scaleUp != null)
            {
                if (transform.localScale.y <= 0.5f && transform.localScale.y >= 0.05f)
                {
                    //AudioSource.PlayClipAtPoint(soundSFX, transform.position, 0.5f);

                    Vector3 newScale = transform.localScale + new Vector3(scaleUp.y, scaleUp.y, scaleUp.y);
                    newScale.x = Mathf.Clamp(newScale.x, originalScale.x / 2f, originalScale.x * 4f);
                    newScale.y = Mathf.Clamp(newScale.y, originalScale.y / 2f, originalScale.y * 4f);
                    newScale.z = Mathf.Clamp(newScale.z, originalScale.z / 2f, originalScale.z * 4f);

                    transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * 5);
                }



                //transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale + new Vector3(scaleUp.y, scaleUp.y, scaleUp.y), Time.deltaTime);
            }
        }
        
        
    }

    private void Scale(InputAction.CallbackContext context)
    {
        
    }
}
