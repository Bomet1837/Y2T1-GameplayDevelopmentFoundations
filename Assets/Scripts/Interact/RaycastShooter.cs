using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastShooter : MonoBehaviour
{
    public static RaycastShooter Instance;
    private GameObject hitObject;
    private IInteractable interactable;

    private GameObject interactUi;
    private TMP_Text interactText;

    [SerializeField] private GameObject playerCam;
    [SerializeField] private float interactDistance = 5f;

    private bool canShowInteractUI = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //Find the UI
        interactUi = GameObject.Find("/UI/InteractUI");
        interactText = GameObject.Find("/UI/InteractUI/Background/InteractText").GetComponent<TMP_Text>();
        
        //Disable the UI since the GameManager sets it to true on Awake
        interactUi.SetActive(false);
    }

    void Update()
    {
        //Input
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            RunInteractable();
        }

        if (!canShowInteractUI && interactUi.gameObject.activeInHierarchy)
        {
            interactUi.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (playerCam.activeInHierarchy)
        {
            ShootRay();
        }
    }

    private void ShootRay()
    {
        //Create the raycast
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        
        //Check if the ray hits something
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Interactable"))
        {
            if ((hit.distance <= interactDistance) && canShowInteractUI)
            {
                //Debug.Log("Hit");
                hitObject = hit.collider.gameObject;

                //Set the interactable
                interactable = hitObject.GetComponent<IInteractable>();

                //Set the interact UI information
                interactUi.SetActive(true);
                interactText.text = interactable.HintInformation;
            }
        }
        else
        {
            hitObject = null;
            interactable = null;

            interactText.text = null;
            interactUi.SetActive(false);
        }
    }

    void RunInteractable()
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
    
    //Unsure if this is used
    public GameObject GetHitObject()
    {
        return hitObject;
    }

    public void ForceHideInteractUI()
    {
        canShowInteractUI = false;
    }
}
