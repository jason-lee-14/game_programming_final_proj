using UnityEngine;
using System.Collections;

public class FlyScript : MonoBehaviour
{
    public float speed = 1f;

    Vector3 newPosition;

    void Start()
    {
        PositionChange();
    }

    void PositionChange()
    {
        newPosition = new Vector3(Random.Range(-44.0f, 44.0f), Random.Range(1f, 5.0f), Random.Range(-145.0f, -95.0f));
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, newPosition) < 1)
        {
            PositionChange();
        }
        
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
    }
}