using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerWorldInteractions.inDialogue)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //playerBody.GetComponentInChildren<Camera>().transform.Rotate(-mouseY, 0, 0);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
