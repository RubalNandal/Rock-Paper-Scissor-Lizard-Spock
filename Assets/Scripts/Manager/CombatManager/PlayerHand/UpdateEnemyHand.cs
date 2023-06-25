using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateEnemyHand : MonoBehaviour, IUpdateHand
{
    private void Awake()
    {
        EventManager.Instance.enemyHandPlayed.AddListener(UpdateHand);
    }

    /// <summary>
    /// Updates the enemy's chosen hand and triggers player's turn.
    /// </summary>
    /// <param name="handPlayed">The hand played by the enemy.</param>
    public void UpdateHand(BaseAttack handPlayed)
    {
        CombatManager.Instance._enemyHand = handPlayed;
        TurnManager.Instance.NextTurn();
    }

    private void OnDestroy()
    {
        EventManager.Instance.enemyHandPlayed.AddListener(UpdateHand);
    }

    
}
