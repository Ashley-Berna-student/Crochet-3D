using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crocheting
{
    public class Delete : MonoBehaviour
    {
        public SingleCrochet singleCrochet;

        void Start () 
        {
            singleCrochet = FindObjectOfType<SingleCrochet>();
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
                Debug.LogWarning("No stitches to delete.");
            }
        }
    }
}
