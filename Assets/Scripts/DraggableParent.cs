using UnityEngine;

public class DraggableParent : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera camera4;

    private Camera currentCamera; // Currently active camera

    private Vector3 initialMousePosition;
    private Vector3 initialObjectPosition;

    private void Start()
    {
        // Set the initial active camera to camera1
        currentCamera = camera1;
    }

    private void OnMouseDown()
    {
        // Store the initial mouse position and object position
        initialMousePosition = GetMouseWorldPosition();
        initialObjectPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        // Calculate the new position of the object based on the initial mouse position
        Vector3 offset = GetMouseWorldPosition() - initialMousePosition;
        transform.position = initialObjectPosition + offset;
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (currentCamera == null)
        {
            Debug.LogError("Current camera reference is null in DraggableParent script.");
            return Vector3.zero;
        }

        // Convert mouse position from screen space to world space using the current camera
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -currentCamera.transform.position.z;
        return currentCamera.ScreenToWorldPoint(mousePosition);
    }

    // Method to switch the active camera
    public void SwitchCamera(int cameraIndex)
    {
        switch (cameraIndex)
        {
            case 1:
                currentCamera = camera1;
                break;
            case 2:
                currentCamera = camera2;
                break;
            case 3:
                currentCamera = camera3;
                break;
            case 4:
                currentCamera = camera4;
                break;
            default:
                Debug.LogError("Invalid camera index provided.");
                break;
        }
    }
}
