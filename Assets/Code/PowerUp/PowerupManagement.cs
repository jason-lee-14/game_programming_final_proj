using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManagement : MonoBehaviour
{
    public GameObject powerupCapsule;
    public int maxPowerups = 4;
    public int respawnInterval = 4;
    public int currPowerups;
    float xPos;
    float yPos = 1.5f;
    float zPos;
    private bool isSpawning;

    // Start is called before the first frame update
    void Start()
    {
        currPowerups = 0;
        isSpawning = false;
    }

    void Update()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(respawnPowerups());
        }
    }

    IEnumerator respawnPowerups()
    {
        while (currPowerups < maxPowerups)
        {
            yield return new WaitForSeconds(respawnInterval / 2);
            //x = 14 y = 1.5 z = 24
            xPos = transform.position.x;
            zPos = transform.position.z;
            Instantiate(powerupCapsule, new Vector3(xPos, transform.position.y+2, zPos), Quaternion.identity);
            currPowerups++;
            yield return new WaitForSeconds(respawnInterval / 2);
        }
        isSpawning = false;
    }
}
