using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages events and event listeners for various game events.
/// </summary>
public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

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

    /// <summary>
    /// Event triggered when the game state is changed.
    /// </summary>
    public UnityEvent<GameState> gameStateChange;

    /// <summary>
    /// Event triggered when the player plays their hand.
    /// </summary>
    public UnityEvent<BaseAttack> playerHandPlayed;

    /// <summary>
    /// Event triggered when the enemy plays their hand.
    /// </summary>
    public UnityEvent<BaseAttack> enemyHandPlayed;

    /// <summary>
    /// Event triggered when the round ends.
    /// </summary>
    public UnityEvent roundEnded;

    /// <summary>
    /// Event triggers click feedback
    /// </summary>
    public UnityEvent clickFeedback;
}
