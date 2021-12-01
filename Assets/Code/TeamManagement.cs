using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamManagement : MonoBehaviour
{
    public List<GameObject> teamRed;
    public List<GameObject> teamBlue;

    public int pointsToWin;

    public Material teamRedColor;
    public Material teamBlueColor;

    public PointBarRed pointBarRed;
    public PointBarBlue pointBarBlue;

    public TextMeshProUGUI red;
    public TextMeshProUGUI blue;

    int teamRedPoints, teamBluePoints;

    GameObject[] redSpawnPoints;
    GameObject[] blueSpawnPoints;

    
    // Start is called before the first frame update
    void Start()
    {
        redSpawnPoints = GameObject.FindGameObjectsWithTag("RedSpawnPoints");
        blueSpawnPoints = GameObject.FindGameObjectsWithTag("BlueSpawnPoints");

        teamRedPoints = 0;
        teamBluePoints = 0;

        /*set maxes and starting values*/
        pointBarRed.SetMaxPoints(pointsToWin);
        pointBarRed.SetPoints(0);

        pointBarBlue.SetMaxPoints(pointsToWin);
        pointBarBlue.SetPoints(0);
    }

    public void JoinRed(GameObject p)
    {
        if (teamRed.Count < 3)
        {
            teamRed.Add(p);
            p.GetComponent<Player>().team = "red";
            p.GetComponentInChildren<Renderer>().material = teamRedColor;

            int spawnChoice = Random.Range(0, 3);
            GameObject.Instantiate(p, redSpawnPoints[spawnChoice].transform.position, redSpawnPoints[spawnChoice].transform.rotation);
        } else
        {
            Debug.Log("Error: red team is full, please choose blue team");
            //error message
        }
    }

    public void JoinBlue(GameObject p)
    {
        if (teamBlue.Count < 3)
        {
            teamBlue.Add(p);
            p.GetComponent<Player>().team = "blue";
            p.GetComponentInChildren<Renderer>().material = teamBlueColor;

            int spawnChoice = Random.Range(0, 3);
            GameObject.Instantiate(p, blueSpawnPoints[spawnChoice].transform.position, blueSpawnPoints[spawnChoice].transform.rotation);
        } else
        {
            Debug.Log("Error: blue team is full, please choose red team");
            //error message
        }
    }

    public void RedReSpawn(Transform t)
    {
        int spawnChoice = Random.Range(0, 3);
        t.position = redSpawnPoints[spawnChoice].transform.position;
        t.rotation = redSpawnPoints[spawnChoice].transform.rotation;
    }

    public void BlueReSpawn(Transform t)
    {
        int spawnChoice = Random.Range(0, 3);
        t.position = blueSpawnPoints[spawnChoice].transform.position;
        t.rotation = blueSpawnPoints[spawnChoice].transform.rotation;
    }

    public void incTeamPoints(string team)
    {
        if (team.Equals("red")) {
            teamRedPoints++;
            pointBarRed.SetPoints(teamRedPoints);
        } 
        else if (team.Equals("blue")) {
            teamBluePoints++;
            pointBarBlue.SetPoints(teamBluePoints);
        }
        else
            Debug.Log("Error: team doesn't exist!");
    }


    // Update is called once per frame
    void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.R))
            incTeamPoints("red");

        if (Input.GetKeyDown(KeyCode.B))
            incTeamPoints("blue");*/

        /*display point values*/
        red.text = teamRedPoints.ToString();
        blue.text = teamBluePoints.ToString();


        if (teamBluePoints == pointsToWin)
            WinState("blue");
        else if (teamRedPoints == pointsToWin)
            WinState("red");
    }

    void WinState(string team)
    {
        //TODO: do something for win state
    }
}
