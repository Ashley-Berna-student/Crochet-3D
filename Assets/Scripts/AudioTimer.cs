using UnityEngine;

namespace WordsForTutorial
{
    public class AudioTimer : MonoBehaviour
    {
        private ScrollingLetters scrollingLetters;

        void Start()
        {
            scrollingLetters = FindObjectOfType<ScrollingLetters>();

            if (scrollingLetters == null)
            {
                Debug.Log("ScrollingLetters component is null");
            }
        }

        void Update()
        {
            // No need for any update logic here since we're destroying after text animation
        }
    }
}
