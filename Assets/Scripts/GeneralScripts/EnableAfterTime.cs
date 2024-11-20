using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterTime : MonoBehaviour
{
    [SerializeField] private float timeToEnable;
    [SerializeField] private GameObject objectToEnable;

    void Start()
    {
        StartCoroutine(EnableObject());
    }

    IEnumerator EnableObject()
    {
        yield return new WaitForSeconds(timeToEnable);
        
        objectToEnable.SetActive(true);
    }
}
