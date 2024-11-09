using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory_FirstPerson : MonoBehaviour
{
    public List<string> inventoryItems = new List<string>();

    public GameObject inventoryUI; // Reference to the UI panel that displays the inventory
    public TMP_Text inventoryText; // Text component to display the inventory content

    private bool isInventoryVisible = false;

    private void Start()
    {
        inventoryUI.SetActive(false); // Initially hide the inventory UI
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            isInventoryVisible = true;
            UpdateInventoryUI();
        }

        if (Input.GetButtonUp("Inventory"))
        {
            isInventoryVisible = false;
            inventoryUI.SetActive(false);
        }
    }

    public void AddItemToInventory(string itemName)
    {
        inventoryItems.Add(itemName);
        Debug.Log(itemName + " added to inventory.");
    }

    private void UpdateInventoryUI()
    {
        if (isInventoryVisible)
        {
            inventoryUI.SetActive(true);
            UpdateInventoryText();
        }
        else
        {
            inventoryUI.SetActive(false);
        }
    }

    private void UpdateInventoryText()
    {
        inventoryText.text = "Inventory:\n";

        foreach (string item in inventoryItems)
        {
            inventoryText.text += "- " + item + "\n";
        }
    }
}
