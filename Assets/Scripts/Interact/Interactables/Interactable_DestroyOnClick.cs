using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Interactable_DestroyOnClick : MonoBehaviour, IInteractable
{
    public string HintInformation => "Press E to destroy";

    public void Interact()
    {
        Destroy(this.gameObject);
    }
}