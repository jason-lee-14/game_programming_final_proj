using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public GameObject bridgeCent;
    rotateBridge bridge;
    void Start()
    {
        gameObject.tag = "Interactable";
        bridge = bridgeCent.GetComponent<rotateBridge>();
    }

    // Update is called once per frame
    public void Interact(Interact playerScript)
    {
        bridge.turnBridge();
    }
}
