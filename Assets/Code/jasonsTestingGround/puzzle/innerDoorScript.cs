using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class innerDoorScript : MonoBehaviour, IInteractable
{
    // Start is called before the first frame update
    public flipOpenDoor parent;

    public void Start()
    {
        gameObject.tag = "Interactable";
    }

    public void Interact(Interact playerScript) {
        parent.swap();
    }
}
