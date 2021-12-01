using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flipOpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public flipOpenDoor left;
    public flipOpenDoor right;
    public bool closed;
    void Start()
    {
        closed = true;
    }
    public void swap(){
        left.localSwap();
        right.localSwap();
        if(closed){
            transform.localRotation = Quaternion.Euler(-90,-90,0);
        
        }else{
            transform.localRotation = Quaternion.Euler(-90,0,0);
        }

        closed = !closed;

    }
    public void localSwap(){
        if(closed){
            transform.localRotation = Quaternion.Euler(-90,-90,0);
        
        }else{
            transform.localRotation = Quaternion.Euler(-90,0,0);
        }

        closed = !closed;
    }

}
