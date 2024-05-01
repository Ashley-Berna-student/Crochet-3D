using UnityEngine;
using UnityEngine.UI;

namespace Crocheting
{
    public class changeSCColor : MonoBehaviour
    {
        public GameObject prefabToChangeColor;
        public Material targetMaterial; // The material to switch to when the button is pressed

        private Renderer prefabRenderer; // Reference to the renderer component of the prefab

        void Start()
        {
            // Get the renderer component of the prefab
            prefabRenderer = prefabToChangeColor.GetComponent<Renderer>();

            // Add a listener to the button's onClick event
            Button button = GetComponent<Button>();
            button.onClick.AddListener(ChangeColor);
        }

        void ChangeColor()
        {
            // Change the material of the prefab to the target material
            prefabRenderer.material = targetMaterial;
        }
    }
}
