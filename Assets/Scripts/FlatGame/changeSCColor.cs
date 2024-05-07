using UnityEngine;
using UnityEngine.UI;

namespace Crocheting
{
    public class changeSCColor : MonoBehaviour
    {
        public GameObject ChangeSCColor;
        public GameObject ChangeINCColor;
        public Material targetMaterial; // The material to switch to when the button is pressed

        private Renderer scRenderer; // Renderer component for ChangeSCColor
        private Renderer incRenderer; // Renderer component for ChangeINCColor

        void Start()
        {
            // Get the renderer components of the prefabs
            scRenderer = ChangeSCColor.GetComponent<Renderer>();
            incRenderer = ChangeINCColor.GetComponent<Renderer>();

            // Add a listener to the button's onClick event
            Button button = GetComponent<Button>();
            button.onClick.AddListener(ChangeColor);
        }

        void ChangeColor()
        {
            // Change the material of each prefab to the target material
            scRenderer.material = targetMaterial;
            incRenderer.material = targetMaterial;
        }
    }
}
