using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages the game state and score, and handles state transitions.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState _gameState;
    private int _score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
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
                    CheckHighScore();
                    break;
                }
            case GameState.PlayerTurn:
                {
                    Debug.Log("Player Turn");
                    // Unpause the game mechanics when player's turn starts
                    Time.timeScale = 1;
                    break;
                }
            case GameState.EnemyTurn:
                {
                    Debug.Log("Enemy Turn");
                    break;
                }
            case GameState.Calculation:
                {
                    Debug.Log("Computing battle results");
                    break;
                }
            case GameState.Victory:
                {
                    Debug.Log("Round Won");
                    _score++;
                    break;
                }
            case GameState.Defeat:
                {
                    CheckHighScore();
                    Debug.Log("Round Lost");
                    break;
                }
        }

        EventManager.Instance.gameStateChange.Invoke(newGameState);
    }

    /// <summary>
    /// Checks and updates the high score if necessary.
    /// </summary>
    public void CheckHighScore()
    {
        if (PlayerPrefs.HasKey(EnvConstants.HIGH_SCORE_KEY))
        {
            if (_score > PlayerPrefs.GetInt(EnvConstants.HIGH_SCORE_KEY))
            {
                PlayerPrefs.SetInt(EnvConstants.HIGH_SCORE_KEY, _score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(EnvConstants.HIGH_SCORE_KEY, _score);
        }

        // Reset current score after high score update
        _score = 0;
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
    Defeat
}
