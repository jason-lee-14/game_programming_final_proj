using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public string team;

    TeamManagement tm;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        tm = GameObject.Find("TeamManager").GetComponent<TeamManagement>();

        healthBar = GameObject.FindGameObjectWithTag("Health").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.M))
            Damage(20);*/

        if ((currentHealth <= 0 || transform.position.y <= -15) && !dead)
        {// is the player dead?
            dead = true;
            currentHealth = 0; // if player falls off side, set health to 0
            healthBar.SetHealth(currentHealth);
            StartCoroutine(Dead());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<FlagControl>().canHold) //we are holding flag
        {
            if ((team == "red" && other.gameObject.name == "RedPointArea") || (team == "blue" && other.gameObject.name == "BluePointArea")) //returning flag to our base
            {
                tm.incTeamPoints(team);
                GetComponent<FlagControl>().ResetFlag();
            }
        }

    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    /*handle player death and actions taken*/
    IEnumerator Dead()
    {
        Renderer[] r = GetComponentsInChildren<Renderer>();
        CamControl mv = GetComponent<CamControl>();

        foreach (Renderer ren in r)
            ren.enabled = false;
        
        mv.enabledMove = false;

        yield return new WaitForSeconds(5); // pause for some amount of time

        /*reset health, position, and other characteristics as necessary*/
        if (team.Equals("red"))
            tm.RedReSpawn(transform);
        else
            tm.BlueReSpawn(transform);

        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);

        foreach (Renderer ren in r)
            ren.enabled = true;

        mv.enabledMove = true;
        dead = false;
    }

}
