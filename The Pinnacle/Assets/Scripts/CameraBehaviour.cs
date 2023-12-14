using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Update()
    {
        // Check if right mouse button is held down
        if (Input.GetMouseButtonDown(1)) // Right mouse button is pressed
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        }
        else if (Input.GetMouseButtonUp(1)) // Right mouse button is released
        {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
        }

        if (Input.GetMouseButton(1)) // Right mouse button is held down
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            // Rotate the camera up and down, but clamp the rotation to avoid flipping
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // Apply the rotation to the camera
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Rotate the entire player body left and right
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
    public void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor
    }
}
