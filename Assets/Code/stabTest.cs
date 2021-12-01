using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stabTest : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
private void OnTriggerEnter(Collider other) {
    Debug.Log(other.gameObject.name);
}
}
