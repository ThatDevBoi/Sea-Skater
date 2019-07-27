using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DB_Platform : MonoBehaviour
{
    // platform Manager script
    public DB_Platform_Manager platformManager;
       
    public void OnEnable()
    {
        // Find the plat manager in the scene
        platformManager = GameObject.FindObjectOfType<DB_Platform_Manager>();
    }

    void OnBecameInvisible()
    {
        Debug.Log("I Moved");
        // Recycle this platform
        platformManager.RecyclePlatform(this.gameObject);
    }
}
