using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_PlayerController : MonoBehaviour
{
    // Array of Transform targets the player will switch back and forwards from
    //  public Transform[] collums;

    // We need to make the Camera Follow the Player Character Model
    // It needs to be the main focus and the shark ahead will be a static GO
    // Camera Variables
    public GameObject waterTrail;
    public Vector3 waterTrailDelay;
    private Vector3 waterVelocity;
    public float trailDelay;

    public GameObject GO_camera;
    public Vector3 offset;
    private Vector3 velocity; 
    public Transform target;
    public float smoothTime = .5f;


	// Use this for initialization
	void Start ()
    {
        GO_camera = GameObject.FindGameObjectWithTag("MainCamera");
        target = this.gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Updated Per Frame so the camera position knows where its gonna be
        CameraFollow();
        WaterTrail();
        #region Tester Code [Not Relevant Now]
        // this is just a simple set up and will be changed later
        // When the correct input is pressed for the chosen keys
        // The players position will changed to the Transform component position in the array which i have manualy assigned
        //if (Input.GetKeyDown(KeyCode.Q))
        //    gameObject.transform.position = collums[0].position;

        //if(Input.GetKeyDown(KeyCode.W))
        //    gameObject.transform.position = collums[1].position;


        //if (Input.GetKeyDown(KeyCode.E))
        //    gameObject.transform.position = collums[2].position;
        #endregion
    }

    void CameraFollow()
    {
        // Cameras position will be aimed at the target Transform and whatever offset position is set in IDE
        Vector3 newPosition = target.position + offset;
        // Where the camera will be each from
        // Using SmoothDamp to allow for more calm and slow effect (Move slow behind the character)
        GO_camera.transform.position = Vector3.SmoothDamp(GO_camera.transform.position, newPosition, ref velocity, smoothTime);

    }

    void WaterTrail()
    {
        waterTrail.transform.position = new Vector3(0, -1f, 0);
        Transform player = GameObject.Find("Player/TrailTarget").GetComponent<Transform>();
        Vector3 newPosition = player.position + waterTrailDelay;
        waterTrail.transform.position = Vector3.SmoothDamp(waterTrail.transform.position, newPosition, ref waterVelocity, trailDelay);
    }
}
