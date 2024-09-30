using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupController_FirstPerson : MonoBehaviour
{
    [SerializeField] private LayerMask pickupLayer;

    private Camera playerCamera;

    public float rayLength = 5f;

    [Header("UI")]
    public GameObject pickupHint;
    public TMP_Text TitleHintText;
    public TMP_Text InstructionHintText;

    private void Start()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        CheckRay();
    }

    void CheckRay()
    {
        //Checking for pickupable object
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hit.collider.name == "Key")
            {
                pickupHint.SetActive(true);
                TitleHintText.text = "Demo Key";
                InstructionHintText.text = "Press E to take";

                if (Input.GetButtonDown("Interact"))
                {
                    this.GetComponent<Inventory_FirstPerson>().AddItemToInventory(hitObject.name);
                    Destroy(hitObject);
                }
            }
            else if (hit.collider.name == "Money")
            {
                pickupHint.SetActive(true);
                TitleHintText.text = "£10";
                InstructionHintText.text = "Press E to take";

                if (Input.GetButtonDown("Interact"))
                {
                    this.GetComponent<Inventory_FirstPerson>().AddItemToInventory(hitObject.name);
                    Destroy(hitObject);
                }
            }
        }
        else
        {
            pickupHint.SetActive(false);
        }
    }
}
