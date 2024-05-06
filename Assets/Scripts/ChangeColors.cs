using UnityEngine;
using UnityEngine.UI;

namespace Crocheting
{
    public class ChangeColors : MonoBehaviour
    {
        public GameObject descendPrefab;
        public Material targetMaterial; // The material to switch to when the button is pressed

        void Start()
        {
            // Add a listener to the button's onClick event
            Button button = GetComponent<Button>();
            button.onClick.AddListener(ChangeColor);
        }

        void ChangeColor()
        {
            // Change the color of descend prefab
            ChangeColorOfPrefab(descendPrefab);
        }


        void ChangeColorOfPrefab(GameObject prefab)
        {
            // Get the child objects of the prefab
            Transform[] children = prefab.GetComponentsInChildren<Transform>();

            // Iterate through the child objects
            foreach (Transform child in children)
            {
                // Skip the parent object itself
                if (child.gameObject == prefab)
                    continue;

                // Get the renderer component of the child object
                Renderer renderer = child.GetComponent<Renderer>();

                // Change the material of the child object to the target material
                if (renderer != null)
                {
                    renderer.material = targetMaterial;
                }
            }
        }
    }
}
