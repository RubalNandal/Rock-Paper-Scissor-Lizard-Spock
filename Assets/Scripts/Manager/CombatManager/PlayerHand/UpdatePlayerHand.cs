using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePlayerHand : MonoBehaviour , IUpdateHand
{

    private void Awake()
    {
        EventManager.Instance.playerHandPlayed.AddListener(UpdateHand);
    }

    /// <summary>
    /// Updates the player's chosen hand and triggers game mechanics.
    /// </summary>
    /// <param name="handPlayed">The hand played by the player.</param>
    public void UpdateHand(BaseAttack handPlayed)
    {
        CombatManager.Instance._playerHand = handPlayed;
        TurnManager.Instance.NextTurn();
    }


    private void OnDestroy()
    {
        EventManager.Instance.playerHandPlayed.AddListener(UpdateHand);
    }

}
