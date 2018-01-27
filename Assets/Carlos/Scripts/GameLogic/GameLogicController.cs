
using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Offers an abstract implementation of the main GameLogic elements
/// </summary>
public abstract class GameLogicController : MonoBehaviour, IGameLogic {
    /// <summary>
    /// (Property) True if the game is lost
    /// </summary>
    public abstract bool LoseFlag { get; }
    /// <summary>
    /// (Property) True if the game is won
    /// </summary>
    public abstract bool WinFlag { get; }

    /// <summary>
    /// Looses the game when called
    /// </summary>
    public abstract void Lose();
   
    /// <summary>
	/// Wins the game when called
    /// </summary>
    public abstract void Win();

	/// <summary>
	/// Wins the game when called, specifying which player
	/// </summary>
	/// <param name="playerID">the ID of the player that won</param>
	public abstract void Win(int playerID);

    /// <summary>
    /// Starts the game when called
    /// </summary>
    public abstract void StartGame();

    /// <summary>
    /// Sets the player in its inital position
    /// </summary>
    public abstract void SetPlayerAtInitialPosition(Vector3 pos);

	/// <summary>
	/// Sets the players at initial positions.
	/// </summary>
	/// <param name="pos">Position.</param>
	public abstract void SetPlayersAtInitialPosition(Vector3[] pos);


}
