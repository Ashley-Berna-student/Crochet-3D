using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class DraggableParent : MonoBehaviour
{
    private Camera mainCamera; // Main camera
    private Camera camera2;    // Camera with tag "Camera2"
    private Camera camera3;    // Camera with tag "Camera3"
    private Camera camera4;    // Camera with tag "Camera4"

    private Vector3 initialMousePosition;
    private Vector3 initialObjectPosition;

    // Enum to specify the axis of movement
    public enum MovementAxis
    {
        X,
        Y,
        Z
    }

    // Current movement axis
    private MovementAxis currentAxis = MovementAxis.X;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void InitializeCameras()
    {
        // Find the parent holding all the cameras
        Transform cameraParent = GameObject.Find("Static/Cameras").transform; // Adjust with the correct path to the parent holding cameras

        // Find all cameras under the parent
        Camera[] cameras = cameraParent.GetComponentsInChildren<Camera>();

        // Separate the cameras by their tags
        foreach (Camera camera in cameras)
        {
            switch (camera.tag)
            {
                case "MainCamera":
                    mainCamera = camera;
                    break;
                case "Camera2":
                    camera2 = camera;
                    break;
                case "Camera3":
                    camera3 = camera;
                    break;
                case "Camera4":
                    camera4 = camera;
                    break;
                default:
                    Debug.LogWarning("Unknown camera tag: " + camera.tag);
                    break;
            }
        }

        // Debug
        if (mainCamera != null)
        {
            Debug.Log("Main camera found: " + mainCamera.name);
        }
        else
        {
            Debug.LogError("Main camera not found in hierarchy...");
        }

        // Debug other cameras...
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeCameras();

        if (scene.name == "FinalScene")
        {
            // Find buttons in the loaded scene based on tags
            GameObject[] upDownButtons = GameObject.FindGameObjectsWithTag("UpDown");
            GameObject[] leftRightButtons = GameObject.FindGameObjectsWithTag("LeftRight");
            GameObject[] forwardBackButtons = GameObject.FindGameObjectsWithTag("ForwardBAck");

            // Assign listeners to each button
            AssignButtonListeners(upDownButtons, MovementAxis.Y);
            AssignButtonListeners(leftRightButtons, MovementAxis.Z);
            AssignButtonListeners(forwardBackButtons, MovementAxis.X);
        }
    }

    private void AssignButtonListeners(GameObject[] buttons, MovementAxis axis)
    {
        foreach (GameObject buttonObject in buttons)
        {
            // Add a new EventTrigger component if it doesn't exist
            EventTrigger eventTrigger = buttonObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = buttonObject.AddComponent<EventTrigger>();
            }

            // Create a new trigger for PointerClick event
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;

            // Create a new delegate for the event callback
            entry.callback.AddListener((data) => { OnButtonClick(axis); });

            // Add the trigger to the EventTrigger component
            eventTrigger.triggers.Add(entry);
        }
    }

    private void OnButtonClick(MovementAxis axis)
    {
        SetCurrentAxis(axis);
    }

    private void OnMouseDown()
    {
        // Store the initial mouse position in world space
        initialMousePosition = GetMouseWorldPosition();

        // Store the initial object position
        initialObjectPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        // Calculate the new position of the object based on the difference in mouse position
        Vector3 offset = GetMouseWorldPosition() - initialMousePosition;

        // Apply the offset to the object's position based on the current movement axis
        Vector3 newPosition = initialObjectPosition;
        switch (currentAxis)
        {
            case MovementAxis.Y:
                newPosition.y += offset.y;
                print("moving along y");
                break;
            case MovementAxis.Z:
                newPosition.z += offset.z;
                print("moving along x");
                break;
            case MovementAxis.X:
                newPosition.x += offset.x;
                print("moving along z");
                break;
        }

        // Update the object's position
        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera reference is null in DraggableParent script.");
            return Vector3.zero;
        }

        // Convert mouse position from screen space to world space using the main camera
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    // Method to set the current movement axis
    private void SetCurrentAxis(MovementAxis axis)
    {
        currentAxis = axis;
    }

    // Method to switch the active camera
    public void SwitchCamera(Camera newCamera)
    {
        mainCamera = newCamera;
    }
}
