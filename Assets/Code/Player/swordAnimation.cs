using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class swordAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sword;//sword object
    Vector3 handPos;//in hand pos
    Quaternion handRot; //in hand rotation
    Vector3 startPos; //start of teh animation
    Vector3 endPos;// end of the animation
    Quaternion startRot;
    Quaternion endRot;

    float atkSpd = .75f;
    bool isAnimating;

    public AudioClip[] swordSound = new AudioClip[3];
    AudioSource[] audioSource = new AudioSource[3];

    void Start()
    {

        isAnimating = false;
        handPos = new Vector3(1.742f,-1.289f,0.54f);
        handRot = Quaternion.Euler(-180,0,0);
        sword.transform.localPosition=handPos;
        sword.transform.localRotation=handRot;
        //startPos = new Vector3(1.7f,1f,0.5f);
        //startRot = Quaternion.Euler(-270,0,0);

        startPos = handPos;
        startRot = handRot; 

        endPos = new Vector3(.9f,.2f,3f);
        endRot = Quaternion.Euler(-180,-20,0);

        audioSource[0] = gameObject.AddComponent<AudioSource>() as AudioSource;
        audioSource[0].clip = swordSound[0];
        audioSource[0].spatialBlend = 1;

        audioSource[1] = gameObject.AddComponent<AudioSource>() as AudioSource;
        audioSource[1].clip = swordSound[1];
        audioSource[1].spatialBlend = 1;

        audioSource[2] = gameObject.AddComponent<AudioSource>() as AudioSource;
        audioSource[2].clip = swordSound[2];
        audioSource[2].spatialBlend = 1;
    }
    void Update(){
         if (Input.GetKeyDown(KeyCode.Mouse0)){
           StartCoroutine(swordAnim());

        }
    }    
    IEnumerator swordAnim(){
        if(isAnimating){
            yield break;
        }
        isAnimating = true;
        float interpolationParameter=0;
        bool isGoingDown = true;
        System.Random rnd = new System.Random();

        audioSource[rnd.Next(0,2)].Play();
        while (isGoingDown){
            interpolationParameter = interpolationParameter+(Time.deltaTime /atkSpd);
            if (interpolationParameter > 1){
                interpolationParameter = Mathf.Clamp(interpolationParameter, 0, 1);
                isGoingDown = false;
            }
            sword.transform.localRotation = Quaternion.Lerp(startRot, endRot, interpolationParameter);
            sword.transform.localPosition = Vector3.Lerp(startPos,endPos,interpolationParameter);
            yield return null;
        }

        //sleep

        //go UP
        interpolationParameter=1;
        bool isGoingUp = true;

        while (isGoingUp){
            interpolationParameter = interpolationParameter + (-1) * (Time.deltaTime / atkSpd);

            if (interpolationParameter > 1 || interpolationParameter < 0){
                interpolationParameter = Mathf.Clamp(interpolationParameter, 0, 1);

                isGoingUp = false;
            }
            sword.transform.localRotation = Quaternion.Lerp(startRot, endRot, interpolationParameter);
            sword.transform.localPosition = Vector3.Lerp(startPos,endPos,interpolationParameter);
            yield return null;
        }
        
        isAnimating=false;
        sword.transform.localPosition=handPos;
        sword.transform.localRotation=handRot;

    }
}
