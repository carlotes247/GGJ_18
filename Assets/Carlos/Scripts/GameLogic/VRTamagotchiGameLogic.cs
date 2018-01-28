using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRTamagotchiGameLogic : GameLogicController {

    [SerializeField]
    private bool m_LoseFlag;
    [SerializeField]
    private bool m_WinFlag;
    public Text GameStatusText;
    public GameObject PanelGameStatus;

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
        PanelGameStatus.SetActive(true);
        GameStatusText.text = "GAME OVER";
        m_WinFlag = false;

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
        //Debug.Log("Game Started!");
        PanelGameStatus.SetActive(false);
    }

    public override void Win()
    {
        GameStatusText.text = "Well Done! You Won!!";
        Debug.Log("Game Won!");
        m_WinFlag = true;
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
