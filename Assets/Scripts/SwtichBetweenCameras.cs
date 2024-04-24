using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwtichBetweenCameras : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera[] cameras;
    [SerializeField] private Button[] buttons;

    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int cameraIndex = i;
            buttons[i].onClick.AddListener(() => SwitchCamera(cameraIndex));
        }
    }

    void SwitchCamera(int index)
    {
        foreach (Camera camera in cameras)
        {
            camera.enabled = false;
        }

        cameras[index].enabled = true;
    }
}
