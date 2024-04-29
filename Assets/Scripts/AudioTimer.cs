using UnityEngine;

public class AudioTimer : MonoBehaviour
{
    public int durationInFrames = 10;
    private int frameCount = 0;

    void Update()
    {
        frameCount++;

        if (frameCount >= durationInFrames )
        {
            Destroy(gameObject);
        }
    }
}
