using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(Interact script);
}

public class Interact : MonoBehaviour
{
    private RaycastHit m_RaycastFocus;
    private bool m_CanInteract = false;//interactable object with right click


    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        // Is interactable object detected in front of player?
        if (Physics.Raycast(ray, out m_RaycastFocus, 20) && m_RaycastFocus.collider.transform.tag == "Interactable")
        {
            m_CanInteract = true;
        }
        else
            m_CanInteract = false;

        // Has interact button been pressed whilst interactable object is in front of player?
        if (Input.GetButtonDown("Fire1") && m_CanInteract == true)
        {
            IInteractable interactComponent = m_RaycastFocus.collider.transform.GetComponent<IInteractable>();
            if (interactComponent != null)
            {
                Debug.Log("interacting");
                interactComponent.Interact(this);  // Perform object's interaction
            }
        }
    }
}
