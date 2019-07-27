using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Platform_Manager : MonoBehaviour
{
    // platform array
    public GameObject[] platformPrefabs;
    public int zedOffset;
	// Use this for initialization
	public void Start ()
    {
        // however many GOs in the variable platformPrefabs
		for(int i = 0; i < platformPrefabs.Length; i++)
        {
            // spawn the array on start
            Instantiate(platformPrefabs[i], new Vector3(0, -1, i * 29), Quaternion.Euler(0, 0, 0));
            // how far forward the different GOS spawn 
            zedOffset += 29;
        }
	}
	
    // Void that moves the old platform forward
    public void RecyclePlatform(GameObject platform)
    {
        // move platform position forward on Z axis
        platform.transform.position = new Vector3(0, -1, zedOffset);
        zedOffset += 29;
    }
}
