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

        // Calculate combined bounds of all child colliders
        Bounds combinedBounds = new Bounds();
        foreach (Collider childCollider in childColliders)
        {
            combinedBounds.Encapsulate(childCollider.bounds);
        }

        // Resize the BoxCollider to encompass all child colliders
        boxCollider.center = combinedBounds.center - transform.position;
        boxCollider.size = combinedBounds.size;
    }
}
