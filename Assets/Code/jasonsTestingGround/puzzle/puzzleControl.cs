using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleControl : MonoBehaviour
{
    public GameObject bridge;
    public GameObject button;
    public GameObject solvedDoorPuzzle;
    public Material newMaterial;

    private void Start()
    {
        // Deactivate the bridge and the button.
        bridge.SetActive(false);
        button.SetActive(false);
        FloatingEffect flottingEffect = solvedDoorPuzzle.GetComponent<FloatingEffect>();
        flottingEffect.enabled = false;
    }

    // Start is called before the first frame update
    public flipOpenDoor[] doors = new flipOpenDoor[5];
    bool once = true;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(once & !doors[0].closed & !doors[1].closed& !doors[2].closed &!doors[3].closed &!doors[4].closed){
            onWin();
            once = !once;
        }
    }
    public void onWin(){
        // Activate the bridge and the button.
        bridge.SetActive(true);
        button.SetActive(true);

        // Make the sphere float to indicate the win condition
        FloatingEffect flottingEffect = solvedDoorPuzzle.GetComponent<FloatingEffect>();
        flottingEffect.enabled = true;

        // Change the material of the sphere
        MeshRenderer sphereMaterial = solvedDoorPuzzle.GetComponent<MeshRenderer>();
        sphereMaterial.material = newMaterial;
    }
}
