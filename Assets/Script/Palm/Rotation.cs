using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Rotation speed
    public float rotationSpeed = 50.0f;

    // Rotation axis
    public Vector3 rotationAxis = Vector3.up;

    // Control the direction of rotation
    public bool rotateClockwise = true;

    void Update()
    {
        // Determine the rotation direction based on boolean variable
        float direction = rotateClockwise ? 1.0f : -1.0f;

        // Rotate the object around the specified axis
        transform.Rotate(rotationAxis, direction * rotationSpeed * Time.deltaTime);
    }
}

