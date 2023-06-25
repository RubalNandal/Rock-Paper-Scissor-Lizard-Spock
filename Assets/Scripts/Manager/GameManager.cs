using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the game state and score, and handles state transitions.
/// </summary>
[RequireComponent(typeof(IHighScore))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState _gameState;
    private int _score;
    private IHighScore _highScoreUpdater;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        _highScoreUpdater = GetComponent<IHighScore>();
    }

    private void Start()
    {
        ChangeGameState(GameState.MainMenu);
    }

    /// <summary>
    /// Changes the game state and triggers associated events.
    /// </summary>
    /// <param name="newGameState">The new game state.</param>
    public void ChangeGameState(GameState newGameState)
    {
        _gameState = newGameState;

        // Perform actions before raising events
        switch (newGameState)
        {
            case GameState.MainMenu:
                {
                    _highScoreUpdater.UpdateHighScore(_score);
                    break;
                }
            case GameState.PlayerTurn:
                {
                    Debug.Log("Game Manager -> Player Turn");
                    // Unpause the game mechanics when player's turn starts
                    Time.timeScale = 1;
                    break;
                }
            case GameState.EnemyTurn:
                {
                    Debug.Log("Game Manager -> Enemy Turn");
                    break;
                }
            case GameState.Calculation:
                {
                    Debug.Log("Game Manager -> Computing battle results");
                    break;
                }
            case GameState.Victory:
                {
                    Debug.Log("Game Manager -> Round Won");
                    _score++;
                    break;
                }
            case GameState.Defeat:
                {
                    _highScoreUpdater.UpdateHighScore(_score);
                    // Reset current score after high score update
                    _score = 0;
                    Debug.Log("Game Manager -> Round Lost");
                    break;
                }
        }

        EventManager.Instance.gameStateChange.Invoke(newGameState);
    }

}

/// <summary>
/// Represents different game states.
/// </summary>
public enum GameState
{
    MainMenu,
    EnemyTurn,
    PlayerTurn,
    Calculation,
    Victory,
    Defeat,
    Tie
}
