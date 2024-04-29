using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crocheting
{
    public class AddRow : MonoBehaviour
    {
        public GameObject singleCrochetGameObject;
        public float rowHeight = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(AddNewRow);
        }

        public void AddNewRow()
        {
            SingleCrochet singleCrochet = singleCrochetGameObject.GetComponent<SingleCrochet>();

            if (singleCrochet == null)
            {
                print("SingleCrochet reference is missing");
                return;
            }

            Vector3 lastStitchPosition = singleCrochet.LastStitch != null ? singleCrochet.LastStitch.transform.position : singleCrochet.instantiationPosition;

            // Calculate the new Y position for the next row
            float newYPosition = lastStitchPosition.y + rowHeight;

            // Use the existing X and Z positions, but update the Y position
            Vector3 newPosition = singleCrochet.LastStitch != null ? singleCrochet.LastStitch.transform.position : singleCrochet.instantiationPosition;
            newPosition.y = newYPosition;

            // Create a new stitch at the updated position
            singleCrochet.instantiationPosition = newPosition;

            // Create a new stitch at the updated position
            singleCrochet.CreateStitch();
        }
    }
}
