using UnityEngine;
using UnityEngine.UI;

namespace CrochetingMR
{
    public class MRIncrease : MonoBehaviour
    {
        public MRAddStitches stitches; // Reference to the SingleCrochet script

        // Start is called before the first frame update
        void Start()
        {
            Button button = GetComponent<Button>();
            if (button != null)
            {
                // Remove any existing listeners
                button.onClick.RemoveAllListeners();

                // Add listener to the button
                button.onClick.AddListener(stitches.CreateIncreaseStitchMR);
            }
        }
    }

}
