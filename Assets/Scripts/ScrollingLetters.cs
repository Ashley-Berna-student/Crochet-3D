using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace WordsForTutorial
{
    public class ScrollingLetters : MonoBehaviour
    {
        public float letterInterval = 1.0f;
        private Text textComponent;
        private string fullText;
        private string displayedText = "";

        // Start is called before the first frame update
        void Start()
        {
            textComponent = GetComponent<Text>();
            fullText = textComponent.text;
            textComponent.text = "";
            StartCoroutine(AnimateText());
        }

        IEnumerator AnimateText()
        {
            for (int i = 0; i < fullText.Length; i++)
            {
                displayedText += fullText[i];
                textComponent.text = displayedText;
                yield return new WaitForSeconds(letterInterval);
            }

            // Find all GameObjects with the tag "KeyboardAudio"
            GameObject[] keyboardAudioObjects = GameObject.FindGameObjectsWithTag("KeyboardAudio");
            foreach (GameObject obj in keyboardAudioObjects)
            {
                // Check if the GameObject is active before destroying
                if (obj.activeInHierarchy)
                {
                    Destroy(obj); // Destroy each active GameObject with the tag "KeyboardAudio"
                }
            }
        }
    }
}
