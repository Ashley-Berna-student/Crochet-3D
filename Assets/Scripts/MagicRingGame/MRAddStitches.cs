using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace CrochetingMR
{
    public class MRAddStitches : MonoBehaviour
    {
        public GameObject prefabToInstantiate;
        public Vector3 instantiationPosition = Vector3.zero;
        public GameObject lastStitch;
        private List<GameObject> stitches = new List<GameObject>();

        public float rowHeight = 0.01921516f; // Added for row height control

        public GameObject increasePrefabToInstantiate;
        public GameObject decreasePefabToInstantiate;

        public int rowCounter = 1;

        public GameObject LastStitch => lastStitch;
        public List<GameObject> Stitches => stitches;

        private bool usingMagicRing = true;
        private Vector3 magicRingCenter = Vector3.zero;
        private float magicRingRadius = 1.0f; // Adjust as needed

        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(CreateSingleCrochetMR);
        }

        
        public void DeleteStitchMR(GameObject stitchToDelete)
        {
            if (stitches.Contains(stitchToDelete))
            {
                // Get the index of the stitch to delete
                int index = stitches.IndexOf(stitchToDelete);

                // Remove the stitch from the list
                stitches.RemoveAt(index);

                // Update the last stitch reference if necessary
                if (stitchToDelete == lastStitch)
                {
                    lastStitch = stitches.Count > 0 ? stitches[stitches.Count - 1] : null;
                }

                // If we are using the magic ring, adjust the magic ring radius
                if (usingMagicRing)
                {
                    magicRingRadius -= 0.1f; // Decrease radius by 0.1 for each deleted stitch
                }

                // Destroy the stitch
                Destroy(stitchToDelete);
            }
        }
        public void CreateSingleCrochetMR()
        {
            // Variables for position and rotation
            Vector3 newPosition;
            Quaternion rotation = Quaternion.identity;

            // Parameters for the circular pattern
            float stitchRadius = 0.1f; // Adjust based on the desired distance between stitches
            float angleIncrement = (2 * Mathf.PI) / (stitches.Count + 1);

            if (stitches.Count == 0)
            {
                // For the first stitch, place it at the instantiation position
                newPosition = instantiationPosition;
            }
            else
            {
                // Calculate angle for the new stitch
                float angle = stitches.Count * angleIncrement;

                // Calculate the new position using polar coordinates
                newPosition = instantiationPosition + new Vector3(Mathf.Cos(angle) * stitchRadius, 0f, Mathf.Sin(angle) * stitchRadius);

                // Adjust the position based on row height if needed
                if (rowCounter > 1)
                {
                    newPosition.y += rowHeight * (rowCounter - 1);
                }

                // Incrementally adjust the Y-axis rotation for the circular effect
                float rotationY = stitches.Count * angleIncrement * Mathf.Rad2Deg; // Convert radians to degrees
                rotation = Quaternion.Euler(0f, rotationY, 0f);
            }

            // Rotate the first row stitches by 90 degrees along the z-axis if needed
            if (rowCounter == 1)
            {
                rotation *= Quaternion.Euler(0f, 0f, 90f);
            }

            // Instantiate the stitch prefab at the calculated position with the appropriate rotation
            GameObject newStitch = Instantiate(prefabToInstantiate, newPosition, rotation);

            // Add the new stitch to the list
            stitches.Add(newStitch);

            // Update the last stitch reference
            lastStitch = newStitch;

            // Debug logs to verify positions and rotations
            Debug.Log($"Stitch {stitches.Count} position: {newPosition}");
            Debug.Log($"Stitch {stitches.Count} rotation: {rotation.eulerAngles}");
        }

        public void CreateIncreaseStitchMR()
        {
            // Calculate the position for the new stitch using polar coordinates
            float angle = (2 * Mathf.PI * stitches.Count) / (stitches.Count + 1); // Distribute the stitches evenly around the circle
            Vector3 newPosition = magicRingCenter + new Vector3(Mathf.Cos(angle) * magicRingRadius, 0f, Mathf.Sin(angle) * magicRingRadius);

            // Instantiate the increase stitch prefab at the calculated position
            GameObject newStitch = Instantiate(increasePrefabToInstantiate, newPosition, Quaternion.identity);

            // Add the new stitch to the list
            stitches.Add(newStitch);

            // Update the last stitch reference
            lastStitch = newStitch;
        }

        public void CreateDecreaseStitchMR()
        {
            // Calculate the position for the new stitch using polar coordinates
            float angle = (2 * Mathf.PI * stitches.Count) / (stitches.Count + 1); // Distribute the stitches evenly around the circle
            Vector3 newPosition = magicRingCenter + new Vector3(Mathf.Cos(angle) * magicRingRadius, 0f, Mathf.Sin(angle) * magicRingRadius);

            // Instantiate the decrease stitch prefab at the calculated position
            GameObject newStitch = Instantiate(decreasePefabToInstantiate, newPosition, Quaternion.identity);

            // Add the new stitch to the list
            stitches.Add(newStitch);

            // Update the last stitch reference
            lastStitch = newStitch;
        }

    }
}
