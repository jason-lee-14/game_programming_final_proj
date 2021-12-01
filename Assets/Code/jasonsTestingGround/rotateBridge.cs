using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateBridge : MonoBehaviour
{
    // Start is called before the first frame update
    float flapTime = 10;
    Quaternion rotOpened; // Rotation when fully opened.
    Quaternion rotClosed; // Rotation when full closed.
    bool isTurning = false;
    bool isClosed = true;
    float changeSign;
    void Start()
    {
        rotOpened = Quaternion.Euler(0, -135, 0);
        rotClosed = Quaternion.Euler(0, 44, 0);
    }
    public void turnBridge(){
        StartCoroutine(BridgeTurn());
    }
    IEnumerator BridgeTurn(){
        
        if (isTurning)
        {
            isClosed = !isClosed;
            changeSign = changeSign *-1;
            yield break;
        }
        isTurning = true;
              float interpolationParameter;

        // Set this according to whether we are going from zero
        // to one, or from one to zero.

        

        // Set lerp parameter to match our state, and the sign
        // of the change to either increase or decrease the
        // lerp parameter during animation.

        if (isClosed)
        {
            
            interpolationParameter = 0;
            changeSign = 1;
        }
        else
        {
            interpolationParameter = 1;
            changeSign = -1;
        }

        while (isTurning)
        {
            // Change our "lerp" parameter according to speed and time,
            // and according to whether we are opening or closing.

            interpolationParameter = interpolationParameter + changeSign * Time.deltaTime / flapTime;

            // At or past either end of the lerp parameter's range means
            // we are on our last step.

            if (interpolationParameter >= 1 || interpolationParameter <= 0)
            {
                // Clamp the lerp parameter.

                interpolationParameter = Mathf.Clamp(interpolationParameter, 0, 1);

                isTurning = false; // Signal the loop to stop after this.
            }

            // Set the X angle to however much rotation is done so far.

            transform.localRotation = Quaternion.Lerp(rotClosed, rotOpened, interpolationParameter);

            // Tell Unity to start us up again at some future time.

            yield return null;
        }

        // Toggle our open/closed state.

        isClosed = !isClosed;

    }
}
