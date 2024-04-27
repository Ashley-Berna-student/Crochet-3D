using UnityEngine;

public class AudioTimer : MonoBehaviour
{
    public AudioClip audioClip; // Public variable to hold the audio clip
    public float playDuration = 1.0f; // Public variable to specify the duration of audio playback
    private AudioSource audioSource;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        // Check if the AudioSource component exists
        if (audioSource == null)
        {
            // Add an AudioSource component if not already present
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the AudioClip to the AudioSource component
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
        }
    }

    // Public function to play audio for a specified duration
    public void PlayWithDuration()
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
            print("isPlaying");

            // Stop playing audio after the specified duration
            Invoke("StopAudio", playDuration);
        }
        else
        {
            Debug.LogError("No audio clip assigned to the AudioSource component.");
        }
    }

    // Function to stop audio playback
    private void StopAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
