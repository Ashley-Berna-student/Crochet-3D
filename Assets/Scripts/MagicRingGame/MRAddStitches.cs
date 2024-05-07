using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Crocheting
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

        private bool usingMagicRing = false;
        private Vector3 magicRingCenter = Vector3.zero;
        private float magicRingRadius = 1.0f; // Adjust as needed

        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(CreateSingleCrochet);
        }

        public void StartMagicRing()
        {
            usingMagicRing = true;
            magicRingCenter = CalculateMagicRingCenter();
            CreateFirstStitchInMagicRing();
        }

        private Vector3 CalculateMagicRingCenter()
        {
            // Calculate the center of the magic ring based on player input or default position
            // For example, you might use the position where the player clicked to start the magic ring
            return Vector3.zero; // Placeholder, replace with actual calculation
        }

        private void CreateFirstStitchInMagicRing()
        {
            // Initialize the first stitch at the center of the magic ring
            lastStitch = Instantiate(prefabToInstantiate, magicRingCenter, Quaternion.identity);
            stitches.Add(lastStitch);
        }

        public void CreateSingleCrochet()
        {
            Vector3 newPosition;

            if (usingMagicRing)
            {
                // Calculate position using polar coordinates around the magic ring
                float angle = CalculateAngleForNextStitch();
                newPosition = magicRingCenter + new Vector3(Mathf.Cos(angle) * magicRingRadius, 0f, Mathf.Sin(angle) * magicRingRadius);
            }
            else
            {
                // Calculate position as before, relative to the previous stitch
                newPosition = CalculatePositionForNextStitch();
            }

            // Instantiate the stitch prefab at the calculated position
            GameObject newStitch = Instantiate(prefabToInstantiate, newPosition, Quaternion.identity);

            // Add the new stitch to the list
            stitches.Add(newStitch);

            // Update the last stitch reference
            lastStitch = newStitch;
        }

        private Vector3 CalculatePositionForNextStitch()
        {
            Vector3 newPosition = Vector3.zero;

            if (lastStitch != null)
            {
                // Offset the position based on the size of the prefab along the Z-axis
                Vector3 offset = new Vector3(0f, 0f, prefabToInstantiate.transform.localScale.z);
                newPosition = lastStitch.transform.position + offset;
            }
            else
            {
                // If there's no last stitch, use the instantiation position
                newPosition = instantiationPosition;
            }

            return newPosition;
        }

        private float CalculateAngleForNextStitch()
        {
            // Calculate the angle for the next stitch around the magic ring
            // This can be adjusted based on the number of stitches already placed
            // For example, for a circular arrangement, you might use:
            // angle = (2 * Mathf.PI * (stitches.Count + 1)) / totalStitchesInCircle;
            return 0f; // Placeholder, replace with actual calculation
        }

        public void DeleteStitch(GameObject stitchToDelete)
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

                // Update the instantiation position if the deleted stitch was not the last one
                if (index < stitches.Count)
                {
                    instantiationPosition = stitches[0].transform.position;
                }

                // Destroy the stitch
                Destroy(stitchToDelete);
            }
        }

        public void CreateIncreaseStitch()
        {
            Vector3 newPosition = CalculatePositionForNextStitch();

            // Instantiate the increase stitch prefab at the calculated position
            GameObject newStitch = Instantiate(increasePrefabToInstantiate, newPosition, Quaternion.identity);

            // Add the new stitch to the list
            stitches.Add(newStitch);

            // Update the last stitch reference
            lastStitch = newStitch;
        }

        public void CreateDecreaseStitch()
        {
            Vector3 newPosition = CalculatePositionForNextStitch();

            // Instantiate the decrease stitch prefab at the calculated position
            GameObject newStitch = Instantiate(decreasePefabToInstantiate, newPosition, Quaternion.identity);

            // Add the new stitch to the list
            stitches.Add(newStitch);

            // Update the last stitch reference
            lastStitch = newStitch;
        }
    }
}
