using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Crocheting
{
    public class SingleCrochet : MonoBehaviour
    {
        public GameObject prefabToInstantiate;
        public Vector3 instantiationPosition = Vector3.zero;
        public GameObject lastStitch;
        private List<GameObject> stitches = new List<GameObject>();

        public GameObject LastStitch => lastStitch;
        public List<GameObject> Stitches => stitches;

        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(CreateStitch);
        }

        public void CreateStitch()
        {
            // Calculate the position for the new stitch
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

            // Instantiate the stitch prefab at the calculated position
            GameObject newStitch = Instantiate(prefabToInstantiate, newPosition, Quaternion.identity);

            // Add the new stitch to the list
            stitches.Add(newStitch);

            // Update the last stitch reference
            lastStitch = newStitch;

            print("new position " + newPosition);
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
    }
}
