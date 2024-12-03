using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] models;
    private GameObject currentModel;
    
    [SerializeField] private Transform spawnPoint;
    
    private int currentModelIndex = 0;

    void Start()
    {
        SpawnModel();
    }

    void SpawnModel()
    {
        //Reset the spawn point's transform to face the model forward
        spawnPoint.rotation = Quaternion.Euler(0, 180, 0);
        
        if (currentModelIndex >= models.Length)
        {
            // Set currentModelIndex to 0 if it exceeds the maximum index
            currentModelIndex = 0;
        }
        else if (currentModelIndex < 0)
        {
            // Set currentModelIndex to the last index if it's less than 0
            currentModelIndex = models.Length - 1;
        }
        
        //Destroy the previous model
        Destroy(currentModel);
        
        //Spawn model
        currentModel = Instantiate(models[currentModelIndex], spawnPoint.position, Quaternion.identity, spawnPoint);
        currentModel.transform.localScale *= 10f / Mathf.Max(currentModel.GetComponentInChildren<Renderer>().bounds.size.x, currentModel.GetComponentInChildren<Renderer>().bounds.size.y, currentModel.GetComponentInChildren<Renderer>().bounds.size.z);
        currentModel.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void ShowNextModel(bool forward)
    {
        if (forward)
        {
            Debug.Log("Forward" + currentModelIndex);
            currentModelIndex++;
        }
        else
        {
            Debug.Log("Backward" + currentModelIndex);
            currentModelIndex--;
        }

        SpawnModel();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
