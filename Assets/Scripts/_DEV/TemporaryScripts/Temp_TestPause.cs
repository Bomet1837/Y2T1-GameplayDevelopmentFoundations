using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_TestPause : MonoBehaviour
{
    public float rotationSpeed = 100f;
    private float defaultSpeed;

    void Start()
    {
        if (PauseMenuManager.Instance != null)
        {
            PauseMenuManager.Instance.OnPausedStatusChanged.AddListener(OnPausedChanged);
        }

        defaultSpeed = rotationSpeed;
    }
    void Update()
    {
        //To visually show if it is working
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnPausedChanged(bool isPaused)
    {
        if (isPaused)
        {
            Freeze();
        }
        else
        {
            Unfreeze();
        }
    }

    private void Freeze()
    {
        rotationSpeed = 0;
    }

    private void Unfreeze()
    {
        rotationSpeed = defaultSpeed;
    }
}
