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
            // Calculate the position for the new stitch using polar coordinates
            float angle = (2 * Mathf.PI * stitches.Count) / (stitches.Count + 1); // Distribute the stitches evenly around the circle

            // If it's the first row, rotate the prefab by 90 degrees along the y-axis
            Quaternion rotation = rowCounter == 1 ? Quaternion.Euler(0f, 90f, 0f) : Quaternion.identity;

            Vector3 newPosition = magicRingCenter + new Vector3(Mathf.Cos(angle) * magicRingRadius, 0f, Mathf.Sin(angle) * magicRingRadius);

            // Instantiate the stitch prefab at the calculated position with the appropriate rotation
            GameObject newStitch = Instantiate(prefabToInstantiate, newPosition, rotation);

            // Add the new stitch to the list
            stitches.Add(newStitch);

            // Update the last stitch reference
            lastStitch = newStitch;
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
