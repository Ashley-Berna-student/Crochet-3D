using UnityEngine;

public class ResizeColliderToChildren : MonoBehaviour
{
    private void Start()
    {
        ResizeBoxCollider();
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
        Bounds combinedBounds = childColliders[0].bounds;
        for (int i = 1; i < childColliders.Length; i++)
        {
            combinedBounds.Encapsulate(childColliders[i].bounds);
        }

        // Resize the BoxCollider to encompass all child colliders
        boxCollider.center = combinedBounds.center - transform.position;
        boxCollider.size = combinedBounds.size;
    }
}
