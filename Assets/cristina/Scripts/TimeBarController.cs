using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the logic behind the healthBar
/// </summary>
public class TimeBarController : MonoBehaviour {

    /// <summary>
    /// Time left    /// </summary>
    [SerializeField]
    private float m_Time;

    /// <summary>
    /// The total time the player can have
    /// </summary>
    [SerializeField]
    private float m_TotalTime;

    public Text m_TimeText;

    // Run when the game starts
    private void Start()
    {
        // We put the Health to the maximum
        //AddHealth(m_MaxHealth);
    }

    public void Update()
    {
        m_TotalTime -= Time.deltaTime;
        //Debug.Log(m_TotalTime);

        if (m_TotalTime < 0)
        {
            // total is min
            m_TotalTime = 0;

            // We lose the game
            Debug.Log("Time Out! GAME OVER!");
            GameManager.Instance.GameLogic.Lose();
        }

        m_TimeText.text = "Time Left " + ((int)(m_TotalTime/60)).ToString()+":"+ (int)(m_TotalTime % 60);

        
    }
    
}
