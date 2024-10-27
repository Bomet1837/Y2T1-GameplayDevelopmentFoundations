using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 5f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    public Transform playerBody;

    private float rotationX = 0f;
    private float rotationY = 0f;

    private bool isFrozen;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Locking the cursor to the center of the screen
        Cursor.visible = false; //Hiding the cursor
    }

    void Update()
    {
        if (!isFrozen) // If not frozen, move camera
        {
            // Accumulate the mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            rotationX += mouseY;
            rotationY += mouseX;

            // Limit the accumulated rotation
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);

            // Apply the accumulated rotation
            transform.localRotation = Quaternion.Euler(-rotationX, 0f, 0f);
            playerBody.localRotation = Quaternion.Euler(0f, rotationY, 0f);

            // Reset the accumulated rotation for the next frame
            mouseX = 0f;
            mouseY = 0f;
        }
    }

    public void FreezeToggle(bool toggleState)
    {
        isFrozen = toggleState;

        EnableCursor(toggleState);
    }

    public void EnableCursor(bool toggleState)
    {
        if (toggleState)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
