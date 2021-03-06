using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    public AudioClip soundEffect;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(soundEffect);
    }
}
