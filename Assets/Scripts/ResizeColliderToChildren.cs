using UnityEngine;
using UnityEngine.SceneManagement;

public class ResizeColliderToChildren : MonoBehaviour
{
    private bool childrenAdded = false;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the scene loaded event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FinalScene" && !childrenAdded)
        {
            ResizeBoxCollider();
            childrenAdded = true;
        }
    }

    private void ResizeBoxCollider()
    {
        // Get or add a BoxCollider component
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider>();
        }

        // Get all child colliders
        Collider[] childColliders = GetComponentsInChildren<Collider>();

        // If there are no child colliders, return
        if (childColliders.Length == 0)
        {
            return;
        }

        // Initialize the combined bounds with the first child's bounds
        Bounds combinedBounds = childColliders[0].bounds;

        // Encapsulate all child bounds into the combined bounds
        for (int i = 1; i < childColliders.Length; i++)
        {
            combinedBounds.Encapsulate(childColliders[i].bounds);
        }

        // Calculate center relative to the parent's transform
        Vector3 centerRelativeToParent = transform.InverseTransformPoint(combinedBounds.center);

        // Set center of the BoxCollider to the calculated center
        boxCollider.center = centerRelativeToParent;

        // Set size of the BoxCollider to the size of the combined bounds
        boxCollider.size = combinedBounds.size;
    }
}
