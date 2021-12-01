using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float RPM = 4;
    public bool start = true;

    float degreesPerSecond;

    Camera mainCam;

    private void Start()
    {
        degreesPerSecond = -360 * RPM / 60f;
        mainCam = Camera.main;
        //mainCam.transform.position = new Vector3(0, 8.46f, -39.58f); //just in case cam gets moved
    }
    void Update()
    {
        if(start)
            transform.Rotate(0, degreesPerSecond * Time.deltaTime, 0);
    }

    public void ToggleStart()
    {
        start = !start;
    }
}
