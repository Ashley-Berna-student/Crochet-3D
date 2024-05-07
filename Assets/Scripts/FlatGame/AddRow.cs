using UnityEngine;
using UnityEngine.UI;

namespace Crocheting
{
    public class AddRow : MonoBehaviour
    {
        public SingleCrochet singleCrochet; // Reference to the SingleCrochet script
        public Vector3 originalPosition; // Store the initial position of the first stitch
        private float originalZPosition; // Store just the Z component of the initial position

        // Start is called before the first frame update
        void Start()
        {
            Button button = GetComponent<Button>();
            if (button != null)
            {
                // Remove any existing listeners
                button.onClick.RemoveAllListeners();

                // Add listener to the button
                button.onClick.AddListener(AddNewRow);
            }

            originalPosition = new Vector3(-2.35f, 2.111f, -1f);
            originalZPosition = originalPosition.z; // Store just the Z component
        }

        public void AddNewRow()
        {
            // Disable the button temporarily to prevent multiple clicks
            GetComponent<Button>().interactable = false;

            if (singleCrochet == null)
            {
                Debug.LogWarning("SingleCrochet reference is missing");
                // Re-enable the button
                GetComponent<Button>().interactable = true;
                return;
            }
            singleCrochet.rowCounter += 1;

            print("row number " + singleCrochet.rowCounter);

            // Check if an increase or decrease stitch is at the original Z position
            bool increaseAtOriginalPos = false;
            bool decreaseAtOriginalPos = false;

            foreach (var stitch in singleCrochet.Stitches)
            {
                // Check if the Z component of the stitch's position matches the original Z position
                if (stitch.transform.position.z == originalZPosition)
                {
                    if (stitch.CompareTag("IncreaseStitch"))
                    {
                        increaseAtOriginalPos = true;
                        print("Increase stitch found at original Z position");
                    }
                    else if (stitch.CompareTag("DecreaseStitch"))
                    {
                        decreaseAtOriginalPos = true;
                        print("Decrease stitch found at original Z position");
                    }
                }
            }

            // Adjust the Z position based on the presence of increase or decrease stitch
            float zOffset = 1f; // Default Z offset for regular stitches
            if (increaseAtOriginalPos)
            {
                originalZPosition -= 0.02735f; // Increase Z position for increase stitch
            }
            else if (decreaseAtOriginalPos)
            {
                originalZPosition += 0.02735f; // Decrease Z position for decrease stitch
            }

            // Set the instantiation position for the next row
            singleCrochet.instantiationPosition = new Vector3(originalPosition.x, singleCrochet.LastStitch != null ?
                singleCrochet.LastStitch.transform.position.y + singleCrochet.rowHeight : originalPosition.y + singleCrochet.rowHeight, originalZPosition);

            // Reset lastStitch to null to place new row at instantiation position
            singleCrochet.lastStitch = null;

            // Create a new row of stitches at the updated position
            //singleCrochet.CreateStitch();
            //Debug.Log("Added row");

            // Re-enable the button after row addition is completed
            GetComponent<Button>().interactable = true;
        }
    }
}