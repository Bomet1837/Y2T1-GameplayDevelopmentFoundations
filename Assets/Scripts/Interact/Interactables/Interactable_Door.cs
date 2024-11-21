using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Interactable_Door : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    private Animation anim;

    public string HintInformation => "Press E to open";

    void Start()
    {
        anim = this.GetComponent<Animation>();
    }

    public void Interact()
    {
        if (isOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        isOpen = true;

        anim.CrossFade("DoorOpen", 0.5f);
    }

    private void CloseDoor()
    {
        isOpen = false;
        
        anim.CrossFade("DoorClose", 0.5f);
    }
}
