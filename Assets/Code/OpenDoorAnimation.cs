using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenDoorAnimation : MonoBehaviour
{
    private bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not

    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him

    enum DoorState
    {
        Closed,
        Opened
    }

    DoorState doorState = new DoorState();      //To check the current state of the door

    private void Start()
    {
        doorOpened = false;                     //Is the door currently opened
        playerInZone = false;                   //Player not in zone
        doorState = DoorState.Closed;           //Starting state is door closed

        doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider player)
    {
        playerInZone = true;
        StartCoroutine(DoorAnimation(player));
    }

    IEnumerator DoorAnimation(Collider player)
    {
        if (doorState == DoorState.Closed && !doorAnim.isPlaying)
        {
            doorAnim.Play("Door_Open");
            doorState = DoorState.Opened;
        }

        yield return new WaitForSeconds(1);
        doorCollider.enabled = false;
        yield return new WaitForSeconds(3);

        if (doorState == DoorState.Opened && !doorAnim.isPlaying)
        {
            doorAnim.Play("Door_Close");
            doorState = DoorState.Closed;
        }
        doorCollider.enabled = true;
    }

    /*private void OnTriggerExit(Collider other)
    {
        playerInZone = false;
    }

    private void Update()
    {
        //To Check if the player is in the zone
        if (playerInZone)
        {
            if (doorState == DoorState.Opened)
            {
                doorCollider.enabled = false;
            }
            else if (doorState == DoorState.Closed)
            {
                doorCollider.enabled = true;
            }
        }

        if (playerInZone)
        {
            doorOpened = !doorOpened;           //The toggle function of door to open/close

            if (doorState == DoorState.Closed && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
            }

            if (doorState == DoorState.Closed && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Open");
                doorState = DoorState.Opened;
            }

            if (doorState == DoorState.Opened && !doorAnim.isPlaying)
            {
                doorAnim.Play("Door_Close");
                doorState = DoorState.Closed;
            }
        }
    }*/
}
