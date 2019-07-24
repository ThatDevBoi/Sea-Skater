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
    private float dragDistance;

    // Movement Variables 
    // Used to change how high the player jumps
    public float jumpHeight = 3f;
    // Used to determine how long the sliding animation will be played for
    public float slideTime = 2f;

	// Use this for initialization
	void Start ()
    {
        // Finding the players position
        player = GameObject.Find("Player").GetComponent<Transform>();
        dragDistance = Screen.height * 15 / 100;    // dragDistance is 15% height of the screen

	}
	
	// Update is called once per frame
	void Update ()
    {
        // Functions
        BoatMovement();
        SwipToMove();

        Vector3 clampedPos = transform.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, -2, 2);
        transform.position = clampedPos;


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
        //Debug.Log(TestTimer);
        #endregion
    }

    // Function which allows for the inputs when the player swips the screen in 4 directions
    void SwipToMove()
    {
        // Simple way to move left and right however we need to make sure the game knows when we swip down or up
        // Also we need to declare these areas on the screen of any mobile device.
        #region Old Swip Movement
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //    startTouchPoisition = Input.GetTouch(0).position;

        //if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        //{
        //    endTouchPosition = Input.GetTouch(0).position;

        //    if ((endTouchPosition.x < startTouchPoisition.x) && transform.position.x > -2)
        //        transform.position = new Vector3(transform.position.x - 2, transform.position.y, gameObject.transform.position.z);

        //    if ((endTouchPosition.x > startTouchPoisition.x) && transform.position.x < 2)
        //        transform.position = new Vector3(transform.position.x + 2, transform.position.y, gameObject.transform.position.z);
        //}
        #endregion

        #region New Swip Movement
        if(Input.touchCount == 1)   // Player only touches the screen 1 time
        {
            Touch touch = Input.GetTouch(0);    // Get the touch
            if(touch.phase == TouchPhase.Began) // Check to see if the player has pressed the screen yet
            {
                // set up our Vector3 positions for the start position the player pushed on the screen 
                startTouchPoisition = touch.position;
                // Until the player releases their finger the end touch wont be found
                endTouchPosition = touch.position;
            }
            // Update the last position based on where the player moved
            else if(touch.phase == TouchPhase.Moved)
            {
                endTouchPosition = touch.position;
            }
            // Check if the players finger is removed from the screen
            else if(touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;  // Last toch is found now
                // reset the scale of the player character when a new touch has been omitted
                // We can remove this later when the animation for sliding is made
                player.localScale = new Vector3(player.localScale.x, 1, player.localScale.z);
                player.position = new Vector3(player.position.x, .5f, player.position.z);
                // Check if the drag distance is greater than 20% of the screen hieght
                if (Mathf.Abs(endTouchPosition.x - startTouchPoisition.x) > dragDistance || Mathf.Abs(endTouchPosition.y - startTouchPoisition.y) > dragDistance)
                {
                    // check to see if the swip is vertical or horizontal
                    if(Mathf.Abs(endTouchPosition.x - startTouchPoisition.x) > Mathf.Abs(endTouchPosition.y - startTouchPoisition.y))
                    {
                        // if the horizontal movement is greater than the vertical movement
                        if((endTouchPosition.x > startTouchPoisition.x))
                        {
                            // move right 2 within the X axis as we are swiping right
                            transform.position = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z);
                            Debug.Log("Right Swip"); 
                        }
                        // however if the horizontal movement is less than the vertical movement
                        else
                        {
                            // we move left -2 within the X axis as we are swiping left
                            transform.position = new Vector3(transform.position.x - 2, transform.position.y, transform.position.z);
                            Debug.Log("Swip Left");
                        }
                    }
                    else
                    {
                        // if the Vertical movement is greater than the horizontal movement
                        if(endTouchPosition.y > startTouchPoisition.y)
                        {
                            player.position = new Vector3(player.position.x, player.position.y + jumpHeight, player.position.z);
                            Debug.Log("Up Swip");
                        }
                        // else vertical movement is less than the horizontal movement
                        else
                        {
                            // Make into an animation later on so it returns back to normal. 
                            // This is the sliding effect
                            player.localScale = new Vector3(player.localScale.x, -0.3f, player.localScale.z);
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {
                    // Its a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }
        #endregion
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
        if (player.position.y > .4f)
        {
            transform.position = new Vector3(transform.position.x, .4f, transform.position.z);
        }
        if(player.localScale.y < 1)
        {
            transform.position = new Vector3(transform.position.x, .4f, transform.position.z);
        }

        // Set the distance so the player can move from behind
        player.transform.Translate(0, 0, playerMovementSpeed * Time.deltaTime);
    }
}
