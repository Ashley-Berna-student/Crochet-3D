using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crocheting
{
    public class Delete : MonoBehaviour
    {
        public SingleCrochet singleCrochet;
        public AddRow addRowScript; // Reference to the AddRow script

        void Start()
        {
            singleCrochet = FindObjectOfType<SingleCrochet>();
            addRowScript = FindObjectOfType<AddRow>(); // Assign the reference to AddRow script
            Button deleteButton = GetComponent<Button>();

            deleteButton.onClick.AddListener(DeleteLastStitch);
        }

        public void UpdateLastStitch(GameObject newLastStitch)
        {
            singleCrochet.lastStitch = newLastStitch;
        }

        public void DeleteLastStitch()
        {
            if (singleCrochet != null && singleCrochet.LastStitch != null)
            {
                // Cache a reference to the last stitch before deletion
                GameObject previousLastStitch = singleCrochet.LastStitch;

                // Notify SingleCrochet script of deletion
                singleCrochet.DeleteStitch(previousLastStitch);

                // Destroy the last stitch
                Destroy(previousLastStitch);
            }
            else
            {
                Debug.LogWarning("No stitches left in the scene. Resetting to original position.");

                // Reset the position to the original position defined in the AddRow script
                singleCrochet.instantiationPosition = addRowScript.originalPosition;
            }
        }
    }
}
