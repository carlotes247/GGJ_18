using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the logic behind the healthBar
/// </summary>
public class HealthBarController : MonoBehaviour {

    /// <summary>
    /// Health of the player
    /// </summary>
    [SerializeField]
    private float m_Health;

    /// <summary>
    /// Max Health the player can have
    /// </summary>
    [SerializeField]
    private float m_MaxHealth;

    public Text m_HealthText;

    // Run when the game starts
    private void Start()
    {
        // We put the Health to the maximum
        AddHealth(m_MaxHealth);
    }

    /// <summary>
    /// Adds or removes health depending on the value
    /// </summary>
    /// <param name="value">How much health to add/remove</param>
    public void AddHealth (float value)
    {
        m_Health += value;

        // If the total is above the max health...
        if (m_Health > m_MaxHealth)
        {
            // total is max
            m_Health = m_MaxHealth;
        }
        // If the total is below min...
        else if (m_Health < 0)
        {
            // total is min
            m_Health = 0;

            // We lose the game
            Debug.Log("Health is 0! GAME OVER!");
            GameManager.Instance.GameLogic.Lose();
        }

        m_HealthText.text = m_Health.ToString();
    }
}
