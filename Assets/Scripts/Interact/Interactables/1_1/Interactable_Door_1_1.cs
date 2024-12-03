using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_Door_1_1 : MonoBehaviour, IInteractable
{
    public string HintInformation => "Press E to open";

    public void Interact()
    {
        StartCoroutine(interactCoroutine());
    }

    IEnumerator interactCoroutine()
    {
        //Fade out
        FirstPersonMovement.instance.FreezePlayer();
        FadeController.instance.FadeOut(null);
        
        //Hide the interact UI
        RaycastShooter.Instance.ForceHideInteractUI();
        
        yield return new WaitForSeconds(2f);
        
        //Switch scenes
        LoadingController.instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
