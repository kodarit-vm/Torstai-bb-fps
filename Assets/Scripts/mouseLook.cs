using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSensitivity = 150f;
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    private GameObject playerBody;

    [Header("Ylös alas suuntainen liikkuminen")]
    public float minXAngle = -70f;
    public float maxXAngle = 90f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerBody = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minXAngle, maxXAngle);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.transform.Rotate(Vector3.up * mouseX);
    }
}
