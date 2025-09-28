using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] clips; // Add sounds in inspector
    public int skippedIndex = -1; // Index of sound to skip (-1 if none)
    public AudioSource source; // Add/fetch in inspector

    private int _currentIndex = 0;

    public void Play()
    {
        if (clips.Length == 0 || source == null) return;

        int index = Random.Range(0, clips.Length);
        source.PlayOneShot(clips[index]);
    }
}


