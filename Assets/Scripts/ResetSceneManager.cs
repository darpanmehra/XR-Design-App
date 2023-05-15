using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetSceneManager : MonoBehaviour
{
   
   public string sceneName;
   public AudioClip hoverAudio;

    // Reset the scene
   public void ResetScene(string sceneName){
        SceneManager.LoadScene(sceneName);
   }


   public void ResetScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   private void OnTriggerEnter(Collider other)
   {
      GameObject rightControllerPointer = other.gameObject;

      if (rightControllerPointer != null && isTouchingRightPointer(rightControllerPointer)){
         ResetScene();
      }

    }

   private bool isTouchingRightPointer(GameObject gameObject)
   {
      return (gameObject.name == "right_controller_pointer");
   }
   
}
