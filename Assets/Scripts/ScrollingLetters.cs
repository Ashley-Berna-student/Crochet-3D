using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }
}
