using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableParent : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 initialMousePosition;
    private Vector3 initialObjectPosition;
    private Vector3 initialObjectRotation;
    private bool isDragging = false;

    public enum MovementAxis
    {
        X,
        Y,
        Z
    }

    public enum RotationAxis
    {
        X,
        Y,
        Z
    }

    private MovementAxis currentAxis = MovementAxis.X;
    private RotationAxis currentRotationAxis = RotationAxis.X;

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
        Camera[] cameras = FindObjectsOfType<Camera>();

        foreach (Camera camera in cameras)
        {
            // Handle camera initialization
        }

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            // Handle main camera not found
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitializeCameras();

        if (scene.name == "FinalScene")
        {
            GameObject[] upDownButtons = GameObject.FindGameObjectsWithTag("UpDown");
            GameObject[] leftRightButtons = GameObject.FindGameObjectsWithTag("LeftRight");
            GameObject[] forwardBackButtons = GameObject.FindGameObjectsWithTag("ForwardBAck");
            GameObject[] rotateForwardBackButton = GameObject.FindGameObjectsWithTag("RForwardBack");
            GameObject[] rotateLeftRightButton = GameObject.FindGameObjectsWithTag("RLeftRight");
            GameObject[] rotateSideToSideButton = GameObject.FindGameObjectsWithTag("RSide");

            AssignButtonListeners(upDownButtons, MovementAxis.Y);
            AssignButtonListeners(leftRightButtons, MovementAxis.Z);
            AssignButtonListeners(forwardBackButtons, MovementAxis.X);

            AssignRotationButtonListeners(rotateForwardBackButton, RotationAxis.Z);
            AssignRotationButtonListeners(rotateLeftRightButton, RotationAxis.X);
            AssignRotationButtonListeners(rotateSideToSideButton, RotationAxis.Y);
        }
    }

    private void AssignButtonListeners(GameObject[] buttons, MovementAxis axis)
    {
        foreach (GameObject buttonObject in buttons)
        {
            EventTrigger eventTrigger = buttonObject.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = buttonObject.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnButtonClick(axis); });
            eventTrigger.triggers.Add(entry);
        }
    }

    private void AssignRotationButtonListeners(GameObject[] buttons, RotationAxis axis)
    {
        foreach (GameObject button in buttons)
        {
            EventTrigger eventTrigger = button.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = button.AddComponent<EventTrigger>();
            }

            EventTrigger.Entry pointerDownEntry = new EventTrigger.Entry();
            pointerDownEntry.eventID = EventTriggerType.PointerDown;
            pointerDownEntry.callback.AddListener((data) => { StartDragRotation(axis); });
            eventTrigger.triggers.Add(pointerDownEntry);

            EventTrigger.Entry pointerUpEntry = new EventTrigger.Entry();
            pointerUpEntry.eventID = EventTriggerType.PointerUp;
            pointerUpEntry.callback.AddListener((data) => { StopDragRotation(); });
            eventTrigger.triggers.Add(pointerUpEntry);
        }
    }

    private void OnButtonClick(MovementAxis axis)
    {
        SetCurrentAxis(axis);
    }

    private void StartDragRotation(RotationAxis axis)
    {
        initialMousePosition = Input.mousePosition;
        initialObjectRotation = transform.eulerAngles;
        isDragging = true;
        SetCurrentRotationAxis(axis);
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - initialMousePosition;
            switch (currentRotationAxis)
            {
                case RotationAxis.X:
                    transform.rotation = Quaternion.Euler(initialObjectRotation.x - mouseDelta.y, initialObjectRotation.y, initialObjectRotation.z);
                    break;
                case RotationAxis.Y:
                    transform.rotation = Quaternion.Euler(initialObjectRotation.x, initialObjectRotation.y + mouseDelta.x, initialObjectRotation.z);
                    break;
                case RotationAxis.Z:
                    transform.rotation = Quaternion.Euler(initialObjectRotation.x, initialObjectRotation.y, initialObjectRotation.z - mouseDelta.x);
                    break;
            }
        }
    }

    private void StopDragRotation()
    {
        isDragging = false;
    }

    private void OnMouseDown()
    {
        initialMousePosition = GetMouseWorldPosition();
        initialObjectPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 offset = GetMouseWorldPosition() - initialMousePosition;
        Vector3 newPosition = initialObjectPosition;
        switch (currentAxis)
        {
            case MovementAxis.Y:
                newPosition.y += offset.y;
                break;
            case MovementAxis.Z:
                newPosition.z += offset.z;
                break;
            case MovementAxis.X:
                newPosition.x += offset.x;
                break;
        }
        transform.position = newPosition;
    }

    private Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null)
        {
            Debug.LogError("Main camera reference is null in DraggableParent script.");
            return Vector3.zero;
        }
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }

    private void SetCurrentAxis(MovementAxis axis)
    {
        currentAxis = axis;
    }

    private void SetCurrentRotationAxis(RotationAxis axis)
    {
        currentRotationAxis = axis;
    }

    public void SwitchCamera(Camera newCamera)
    {
        mainCamera = newCamera;
    }
}
