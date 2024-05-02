using UnityEngine;
using UnityEngine.UI;

namespace Crocheting
{
    public class AddRow : MonoBehaviour
    {
        public SingleCrochet singleCrochet; // Reference to the SingleCrochet script
        public Vector3 originalPosition; // Store the initial position of the first stitch

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

            // Set the instantiation position for the next row
            singleCrochet.instantiationPosition = new Vector3(originalPosition.x, singleCrochet.LastStitch != null ?
                singleCrochet.LastStitch.transform.position.y + singleCrochet.rowHeight : originalPosition.y + singleCrochet.rowHeight, originalPosition.z);

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

