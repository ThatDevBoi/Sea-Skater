using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_PlayerController : MonoBehaviour
{
    // Array of Transform targets the player will switch back and forwards from
    public Transform[] collums;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        // this is just a simple set up and will be changed later
        // When the correct input is pressed for the chosen keys
        // The players position will changed to the Transform component position in the array which i have manualy assigned
        if (Input.GetKeyDown(KeyCode.Q))
            gameObject.transform.position = collums[0].position;

        if(Input.GetKeyDown(KeyCode.W))
            gameObject.transform.position = collums[1].position;


        if (Input.GetKeyDown(KeyCode.E))
            gameObject.transform.position = collums[2].position;
    }
}
