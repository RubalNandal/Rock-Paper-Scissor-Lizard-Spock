using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the combat gameplay mechanics.
/// </summary>
[RequireComponent(typeof(UpdateEnemyHand), typeof(UpdatePlayerHand), typeof(ClearHands))]
public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public BaseAttack _enemyHand { get; set; } 
    public BaseAttack _playerHand { get; set; }
    public string attackDescription { get; private set; }

    [SerializeField]
    private List<BaseAttack> _attacks;

    IRandomHand _randmHandGenerator;
    ClearHands PlayerHandsClearer;
    
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
       
        EventManager.Instance.gameStateChange.AddListener(OnGameStateChange);
        _randmHandGenerator = GetComponent<IRandomHand>();
        PlayerHandsClearer = GetComponent<ClearHands>();
        PlayerHandsClearer.ClearPlayedHands();
    }



    /// <summary>
    /// Handles the game state change events.
    /// </summary>
    /// <param name="newGameState">The new game state.</param>
    private void OnGameStateChange(GameState newGameState)
    {
        if (newGameState == GameState.Calculation)
        {
            TurnManager.Instance.NextTurn();
        }
        if (newGameState == GameState.EnemyTurn)
        {
            _randmHandGenerator.GenerateRandomHand(_attacks);
        }
    }


    /// <summary>
    /// Calculates the battle results based on the player and enemy hands.
    /// </summary>
    public GameState CalculateBattleResults()
    {
        // Player ran out of time
        if (_playerHand.attackType == AttackType.Empty)
        {
            attackDescription = EnvConstants.RAN_OUT_OF_TIME_DESCRIPTION;
            PlayerHandsClearer.ClearPlayedHands();
            return (GameState.Defeat);
        }
        // If player's attack is strong against enemy, player wins
        else if (_playerHand.strongAgainst.Find(x => x.attackType == _enemyHand.attackType).attackType != AttackType.Empty)
        {
            attackDescription = _playerHand.attackType + " (Player) " + _playerHand.strongAgainst.Find(x => x.attackType == _enemyHand.attackType).description + " " + _enemyHand.attackType + " (Enemy)";
            PlayerHandsClearer.ClearPlayedHands();
            return (GameState.Victory);
        }
        // If player's attack is weak against enemy, player loses
        else if (_playerHand.weakAgainst.Find(x => x.attackType == _enemyHand.attackType).attackType != AttackType.Empty)
        {
            attackDescription =   _enemyHand.attackType + " (Enemy) " + _playerHand.weakAgainst.Find(x => x.attackType == _enemyHand.attackType).description +" " +_playerHand.attackType + " (Player)";
            PlayerHandsClearer.ClearPlayedHands();
            return (GameState.Defeat);
        }
        else
        {
            attackDescription = EnvConstants.TIE_DESCRIPTION;
            PlayerHandsClearer.ClearPlayedHands();
            return (GameState.Tie);
            
        }
    }


    private void OnDestroy()
    {
        EventManager.Instance.gameStateChange.RemoveListener(OnGameStateChange);
    }


}
