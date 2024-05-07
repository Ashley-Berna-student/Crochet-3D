using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CrochetingMR
{
    public class MRDelete : MonoBehaviour
    {
        public MRAddStitches stitches;
        public MRAddRow addRowScript; // Reference to the AddRow script

        void Start()
        {
            stitches = FindObjectOfType<MRAddStitches>();
            addRowScript = FindObjectOfType<MRAddRow>(); // Assign the reference to AddRow script
            Button deleteButton = GetComponent<Button>();

            deleteButton.onClick.AddListener(DeleteLastStitch);
        }

        public void UpdateLastStitch(GameObject newLastStitch)
        {
            stitches.lastStitch = newLastStitch;
        }

        public void DeleteLastStitch()
        {
            if (stitches != null && stitches.LastStitch != null)
            {
                // Cache a reference to the last stitch before deletion
                GameObject previousLastStitch = stitches.LastStitch;

                // Notify MRAddStitch script of deletion
                stitches.DeleteStitchMR(previousLastStitch);

                // Destroy the last stitch
                Destroy(previousLastStitch);
            }
            else
            {
                Debug.LogWarning("No stitches left in the scene. Resetting to original position.");

                // Reset the position to the original position defined in the AddRow script
                stitches.instantiationPosition = addRowScript.originalPosition;
            }
        }
    }
}
