using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRTamagotchiGameLogic : GameLogicController {

    [SerializeField]
    private bool m_LoseFlag;
    [SerializeField]
    private bool m_WinFlag;


    public override bool LoseFlag
    {
        get
        {
            return m_LoseFlag;
        }
    }

    public override bool WinFlag
    {
        get
        {
            return m_WinFlag;
        }
    }

    public override void Lose()
    {
        Debug.Log("Game Lost!");
    }

    public override void SetPlayerAtInitialPosition(Vector3 pos)
    {
        throw new NotImplementedException();
    }

    public override void SetPlayersAtInitialPosition(Vector3[] pos)
    {
        throw new NotImplementedException();
    }

    public override void StartGame()
    {
        Debug.Log("Game Started!");
    }

    public override void Win()
    {
        Debug.Log("Game Won!");
    }

    public override void Win(int playerID)
    {
        throw new NotImplementedException();
    }

    // Use this for initialization
    void Start () {
        StartGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
