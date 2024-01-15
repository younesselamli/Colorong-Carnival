using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Awake : MonoBehaviour
{

    public static Music_Awake Instance; // Singleton instance



    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Ensure only one instance of MusicManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make the GameObject persistent between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
