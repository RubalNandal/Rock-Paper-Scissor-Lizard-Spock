using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the combat gameplay mechanics.
/// </summary>
public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    private BaseAttack _enemyHand;
    private BaseAttack _playerHand;

    [SerializeField]
    private List<BaseAttack> _attacks;

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

        EventManager.Instance.enemyHandPlayed.AddListener(UpdateEnemyHand);
        EventManager.Instance.playerHandPlayed.AddListener(UpdatePlayerHand);
        EventManager.Instance.gameStateChange.AddListener(OnGameStateChange);
    }

    private void Start()
    {
        ClearPlayedHands();
    }

    private void OnDestroy()
    {
        EventManager.Instance.enemyHandPlayed.RemoveListener(UpdateEnemyHand);
        EventManager.Instance.playerHandPlayed.RemoveListener(UpdatePlayerHand);
        EventManager.Instance.gameStateChange.RemoveListener(OnGameStateChange);
    }

    /// <summary>
    /// Updates the player's chosen hand and triggers game mechanics.
    /// </summary>
    /// <param name="handPlayed">The hand played by the player.</param>
    private void UpdatePlayerHand(BaseAttack handPlayed)
    {
        Debug.Log("Player played: " + handPlayed.attackType.ToString());
        _playerHand = handPlayed;

        // Pause game mechanics
        Time.timeScale = 0;

        GameManager.Instance.ChangeGameState(GameState.Calculation);
    }

    /// <summary>
    /// Updates the enemy's chosen hand and triggers player's turn.
    /// </summary>
    /// <param name="handPlayed">The hand played by the enemy.</param>
    private void UpdateEnemyHand(BaseAttack handPlayed)
    {
        Debug.Log("Enemy played: " + handPlayed.attackType.ToString());
        _enemyHand = handPlayed;
        GameManager.Instance.ChangeGameState(GameState.PlayerTurn);
    }

    /// <summary>
    /// Handles the game state change events.
    /// </summary>
    /// <param name="newGameState">The new game state.</param>
    private void OnGameStateChange(GameState newGameState)
    {
        if (newGameState == GameState.Calculation)
        {
            CalculateBattleResults();
        }
        else if (newGameState == GameState.EnemyTurn)
        {
            PlayRandomHand();
        }
        else if (newGameState == GameState.Victory)
        {
            GameManager.Instance.ChangeGameState(GameState.EnemyTurn);
        }
        else if (newGameState == GameState.Defeat)
        {
            GameManager.Instance.ChangeGameState(GameState.MainMenu);
        }
    }

    /// <summary>
    /// Plays a random hand for the enemy.
    /// </summary>
    private void PlayRandomHand()
    {
        EventManager.Instance.enemyHandPlayed.Invoke(_attacks[UnityEngine.Random.Range(0, _attacks.Count)]);
    }

    /// <summary>
    /// Calculates the battle results based on the player and enemy hands.
    /// </summary>
    private void CalculateBattleResults()
    {
        Debug.Log("Player registered: " + _playerHand.attackType + " vs. Enemy Registered: " + _enemyHand.attackType);

        // Player ran out of time
        if (_playerHand.attackType == AttackType.Empty)
        {
            // Clear hands data after calculations
            ClearPlayedHands();
            GameManager.Instance.ChangeGameState(GameState.Defeat);
        }
        // If player's attack is strong against enemy, player wins
        else if (_playerHand.strongAgainst.Contains(_enemyHand.attackType))
        {
            // Clear hands data after calculations
            ClearPlayedHands();
            GameManager.Instance.ChangeGameState(GameState.Victory);
        }
        // If player's attack is weak against enemy, player loses
        else if (_playerHand.weakAgainst.Contains(_enemyHand.attackType))
        {
            // Clear hands data after calculations
            ClearPlayedHands();
            GameManager.Instance.ChangeGameState(GameState.Defeat);
        }
        else
        {
            // Clear hands data after calculations
            ClearPlayedHands();
            GameManager.Instance.ChangeGameState(GameState.EnemyTurn);
            Debug.Log("Player is neither strong nor weak against enemy");
        }
    }

    /// <summary>
    /// Clears the played hands when the round ends.
    /// </summary>
    private void ClearPlayedHands()
    {
        _playerHand = _enemyHand = new BaseAttack();

        // Unpause game mechanics
        Time.timeScale = 1;
    }
}
