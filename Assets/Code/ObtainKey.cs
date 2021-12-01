using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtainKey : MonoBehaviour
{
    public GameObject door;
    public float speed = 0.5f;

    private float collisionTimes = 0;
    bool isShifting = false;

    public AudioClip soundEffect;
    AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            collisionTimes++;
            Debug.Log("Ouch!!!");
            if (collisionTimes == 3)
            {
                audioSource.PlayOneShot(soundEffect);
                door.transform.position += new Vector3(1, 0, 0);
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
            }
        }
    }
}
