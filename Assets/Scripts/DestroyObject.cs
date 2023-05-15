using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DestroyObject : MonoBehaviour
{
    
    public InputActionReference _destroyObjectReference;
    public AudioClip _audioClipOnDestroy;
    public GameObject _particleEffect;
    private bool wasDestroyed;
    private float distanceThreshold = 10f;

    void Awake()
    {
        // add Cloned and Detached events to action's .started and .canceled states
        _destroyObjectReference.action.started += DestroyObjectOnButtonClick;
        _destroyObjectReference.action.canceled += Detached;
    }

    private void DestroyObjectOnButtonClick(InputAction.CallbackContext context){
        if (gameObject.GetComponent<XRGrabInteractable>().isSelected && !wasDestroyed){
            wasDestroyed = true;
            DestroyGameObject();
        }

    }


    private void Detached(InputAction.CallbackContext context)
    {
        // can specify custom behaviors for the original object when detached
    }

    private void Update()
    {
        GameObject playerRightHandController = GameObject.Find("RightHand Controller");

        if (playerRightHandController == null)
        {
            return;
        }

        // Calculate the distance between the object and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerRightHandController.transform.position);
        // Check if the distance is greater than the threshold
        if (!wasDestroyed && distanceToPlayer >= distanceThreshold)
        {   
            wasDestroyed = true;
            DestroyGameObject();
        }
    }

    private void DestroyGameObject(){

            AudioSource.PlayClipAtPoint(_audioClipOnDestroy, transform.position);
            
            Destroy(gameObject, 0.3f);

            GameObject particleEffect = Instantiate(_particleEffect, transform.position, transform.rotation);
            particleEffect.GetComponent<ParticleSystem>().Play();
    }
    

}
