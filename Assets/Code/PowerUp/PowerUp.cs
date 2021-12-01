using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    // Capsule variables
    public GameObject pickupEffect;
    public float duration = 5;
    public float speedMultiplier = 2f;
    public float jumpSpeedMultiplier = 1.2f;

    // Float animation variables
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start and Update are to control the floating animation
    // OnTriggerEnter and Coroutine are to control the capsule logic
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    private void Update()
    {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    void OnTriggerEnter(Collider other)
    {
        // Only true when if a player collide with the capsule
        if (other.CompareTag("Player"))
        {
            System.Random random = new System.Random();
            int value = random.Next(0, 4);
            StartCoroutine(Pickup(other, value));
        }
    }

    // 1: More speed
    // 2. HP++
    // 3. Higher Jump
    // 4. More damage
    IEnumerator Pickup(Collider player, int ability)
    {
        // Create the pickup effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        // Make the capsule disappear
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        // Decrement currPowerups.

        // Apply effect to the player
        if (ability == 0)
        {
            Debug.Log("Run player, run!");

            CamControl user = player.GetComponent<CamControl>();
            user.speedUp();
        } 

        if (ability == 1)
        {
            Debug.Log("More JUUUUICE!");

            // Get the player, set the health to 100 and update the health bar animation
            Player playerStats = player.GetComponent<Player>();
            playerStats.currentHealth = 100;
            playerStats.healthBar.SetHealth(100);
        }

        if (ability == 2)
        {
            Debug.Log("I believe I can fly~");

            // Give the player power
            CamControl user = player.GetComponent<CamControl>();
            user.jumpUp();
        }

        if (ability == 3)
        {
            Debug.Log("DESTORYYYYY!");
            CamControl user = player.GetComponent<CamControl>();
            user.dashUp();
        }

        // Wait for duration amount of time
        yield return new WaitForSeconds(5);

        // Destory the object
        Destroy(gameObject);
    }
}
