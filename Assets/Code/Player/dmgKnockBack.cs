using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgKnockBack : MonoBehaviour
{
    // Start is called before the first frame update
    CamControl player;

    public AudioClip oof;
    AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        audioSource.clip = oof;
        audioSource.spatialBlend = 1;
        player = GetComponent<CamControl>();
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "sword"){
            player.lowerGravity();
            audioSource.Play();
            GetComponent<Player>().Damage(10);
        }
    }
}
