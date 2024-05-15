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

        // Calculate center relative to the parent's transform
        Vector3 centerRelativeToParent = transform.InverseTransformPoint(combinedBounds.center);

        // Set center of the BoxCollider to the calculated center
        boxCollider.center = centerRelativeToParent;

        // Set size of the BoxCollider to the size of the combined bounds
        boxCollider.size = combinedBounds.size; //add some debugs to find out what size it is!!!!
    }

}
