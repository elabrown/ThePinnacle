using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // The target character to follow
    public float distance = 5.0f; // Default distance from the target
    public float height = 3.0f; // Height above the target
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public float zoomSensitivity = 10.0f; // Sensitivity of the zoom
    public float minDistance = 2.0f; // Minimum zoom distance
    public float maxDistance = 10.0f; // Maximum zoom distance
    public float fixedXAngle = 45.0f; // Fixed X angle

    private void LateUpdate()
    {
        if (!target) return;

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentHeight = transform.position.y;

        // Dampen the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Adjust the distance if the scroll wheel is used
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity, minDistance, maxDistance);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        Vector3 position = target.position - Quaternion.Euler(0, wantedRotationAngle, 0) * Vector3.forward * distance;

        // Set the height of the camera
        position = new Vector3(position.x, currentHeight, position.z);

        // Apply the position
        transform.position = position;

        // Apply the rotation with a fixed X angle and Y angle following the target
        transform.rotation = Quaternion.Euler(fixedXAngle, wantedRotationAngle, 0);
    }
}
