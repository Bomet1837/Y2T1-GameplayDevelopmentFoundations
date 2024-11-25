using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectOverTime : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    void Update()
    {
        this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
