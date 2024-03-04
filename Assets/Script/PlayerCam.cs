using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float senx;
    public float seny;

    public Transform orientation;

    public float xRotation;
    public float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Get Mouse Input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * senx;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * seny;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f ,90f);

        //Rotate Cam and Orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
