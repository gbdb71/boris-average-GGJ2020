using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Private instance to the game manager
    private static GameManager _instance;

    // Property to return the instance
    public static GameManager Instance
    {
        get
        {
            // Logic to create the instance
            if (_instance == null)
            {
                GameObject newGameManager = new GameObject("GameManager");
                newGameManager.AddComponent<GameManager>();
            }

            return _instance;
        }
    }


    public LevelManager levelManager;
    //public AudioManager audioManager;

    void Awake()
    {
        _instance = this;

        // Get access to the rest of the managers        
        levelManager = GetComponent<LevelManager>();
        //audioManager = GetComponent<AudioManager>();
    }
}
