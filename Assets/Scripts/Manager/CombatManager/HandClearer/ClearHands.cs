using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearHands : MonoBehaviour
{
    /// <summary>
    /// Clears the played hands when the round ends.
    /// </summary>
    public void ClearPlayedHands()
    {
       CombatManager.Instance._playerHand = CombatManager.Instance._enemyHand = new BaseAttack();
    }
}
