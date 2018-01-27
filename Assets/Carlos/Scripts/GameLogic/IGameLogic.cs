//---------------------------------------------------------------------------
// Carlos Gonzalez Diaz - TFG - Simulador Virtual Carabina M4 - 2016
// Universidad Rey Juan Carlos - ETSII
//---------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

/// <summary>
/// Interfaces de main game logic elements
/// </summary>
public interface IGameLogic {

    /// <summary>
    /// (Property) True if the game is won
    /// </summary>
    bool WinFlag { get; }
    /// <summary>
    /// (Property) False if the game is lost
    /// </summary>
    bool LoseFlag { get; }

    /// <summary>
    /// Wins then game when called
    /// </summary>
    void Win();

	/// <summary>
	/// Wins then game when called, passing in which player
	/// </summary>
	/// <param name="playerID">Player ID.</param>
	void Win(int playerID);

    /// <summary>
    /// Looses the game when called
    /// </summary>
    void Lose();

    /// <summary>
    /// Starts the Game
    /// </summary>
    void StartGame();
}
