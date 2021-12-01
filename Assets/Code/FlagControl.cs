using System.Collections;
using UnityEngine;

public class FlagControl : MonoBehaviour
{
    public bool canHold = true;
    public GameObject flag;
    public Transform guide;

    Vector3 flagStartingPosition;
    Quaternion flagStartingRotation;

    /*this is for the bouncing flag coroutine*/
    bool isMovingUp = false, isMovingDown = false;

    Vector3 startPos, endPos;

    float inc;

    private void Start()
    {
        inc = 0;
        /*record starting position, rotation, and scale for when a player "drops" a flag*/
        flagStartingPosition = transform.position;
        flagStartingRotation = transform.rotation;

        startPos = guide.localPosition;
        endPos = guide.localPosition + new Vector3(0, 0.5f, 0);
    }

    void Update()
    {
        if (!canHold && flag) {
            flag.transform.position = guide.position;

            inc += (360f * Time.deltaTime * 0.1f) % 360f;
            flag.transform.rotation = Quaternion.Euler(0,inc,0);

            StartCoroutine(Bounce());
            if (flag && GetComponent<Player>().dead) //player drops flag
                ResetFlag();
        }
    }

    /*reset flag back to starting position if player drops it or scores a point*/
    public void ResetFlag()
    {
        if (flag)
        {
            flag.transform.position = flagStartingPosition;
            flag.transform.rotation = flagStartingRotation;
            flag.transform.localScale = new Vector3(1, 1, 1);
            flag.GetComponentInChildren<Cloth>().enabled = true;
            flag = null;
            canHold = true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 10) //flag layer
        {
            /*don't pick up your own flag*/
            if (GetComponent<Player>().team.Equals("blue") && other.gameObject.tag == "BlueFlag")
                return;
            else if (GetComponent<Player>().team.Equals("red") && other.gameObject.tag == "RedFlag")
                return;

            if (!flag)
            { //enemy player has picked up the flag
                flag = other.gameObject;
                canHold = false;

                //log current position to spawn back when player dies
                flagStartingPosition = other.transform.position;
                flagStartingRotation = other.transform.rotation;

                //scale down flag and remove cloth physics
                other.gameObject.transform.localScale = new Vector3(0.33277f, 0.33277f, 0.33277f);
                other.gameObject.GetComponentInChildren<Cloth>().enabled = false;
            }
        }
    }

    /*this coroutine bounces flag above head of player*/
    IEnumerator Bounce()
    {
        if (isMovingUp || isMovingDown)
            yield break;

        isMovingUp = true;
        float interpolationParameter = 0;

        /* Bridge is raise back to starting position (in given parameter of seconds)*/
        while (isMovingUp)
        {
            interpolationParameter += Time.deltaTime / 3;

            if (interpolationParameter >= 1)
            {
                // Clamp the lerp parameter.
                interpolationParameter = Mathf.Clamp(interpolationParameter, 0, 1);

                isMovingUp = false; // Signal the loop to stop after this.
                isMovingDown = true;
            }

            guide.transform.localPosition = Vector3.Lerp(startPos, endPos, interpolationParameter);
            yield return null;
        }

        interpolationParameter = 0;

        /* Bridge falls into place code (in given parameter of seconds)*/
        while (isMovingDown)
        {
            interpolationParameter += Time.deltaTime / 3;

            if (interpolationParameter >= 1)
            {
                // Clamp the lerp parameter.
                interpolationParameter = Mathf.Clamp(interpolationParameter, 0, 1);

                isMovingDown = false; // Signal the loop to stop after this.
            }

            guide.transform.localPosition = Vector3.Lerp(endPos, startPos, interpolationParameter);
            yield return null;
        }

    }
}
