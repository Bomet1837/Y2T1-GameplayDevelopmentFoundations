using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float rotationSpeed = 100f;
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            spawnPoint.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            spawnPoint.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
    }
}
