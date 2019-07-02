using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_BoatMovement : MonoBehaviour
{
    // Variables 
    public float boatMovementSpeed = 6f;
    public float playerMovementSpeed = 2f;
    // Distance between boat and the player
    public Transform player;
    // swip to touch Variables 
    public Vector3 startTouchPoisition, endTouchPosition;


	// Use this for initialization
	void Start ()
    {
        // Finding the players position
        player = GameObject.Find("Player").GetComponent<Transform>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        // Functions
        BoatMovement();
        SwipToMove();
        #region Stopping Test
        // This Test shows that when the player stop so does the boat. This will work both ways. 
        // Boat dont move player dont move. Player dont move boat dont move it keeps the value of its distance
        // Through the development Phase animations can be placed in action or different gameplay events
        float TestTimer = 5f;

        TestTimer -= Time.time;
        if(TestTimer <= 0)
        {
            playerMovementSpeed = 0;        
        }
        Debug.Log(TestTimer);
        #endregion
    }

    // Function which allows for the inputs when the player swips the screen in 4 directions
    void SwipToMove()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            startTouchPoisition = Input.GetTouch(0).position;

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            if ((endTouchPosition.x < startTouchPoisition.x) && transform.position.x > -2)
                transform.position = new Vector3(transform.position.x - 2, transform.position.y, gameObject.transform.position.z);

            if ((endTouchPosition.x > startTouchPoisition.x) && transform.position.x < 2)
                transform.position = new Vector3(transform.position.x + 2, transform.position.y, gameObject.transform.position.z);
        }
    }

    void BoatMovement()
    {
        // Allows the boat to move forward
        gameObject.transform.Translate(0, 0, boatMovementSpeed * Time.deltaTime);

        // Figure out the distance between the boat and the player
        float distance = 9f;
        Debug.Log(distance);
        transform.position = (transform.position - player.position).normalized * distance + player.position;
        player.position = new Vector3(gameObject.transform.position.x, player.position.y, player.position.z);

        // Set the distance so the player can move from behind
        player.transform.Translate(0, 0, playerMovementSpeed * Time.deltaTime);
    }
}
