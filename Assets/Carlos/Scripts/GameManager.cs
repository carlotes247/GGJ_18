using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    protected GameManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    private void Awake()
    {
        // Your initialization code here
        Debug.Log("GameManager launched");
        // The Singleton will create the instance of the gameManager! Awesome!
    }





}
