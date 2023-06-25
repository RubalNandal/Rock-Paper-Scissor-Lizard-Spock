using System.Collections.Generic;
using UnityEngine;

public class RandomHandGenerator : MonoBehaviour, IRandomHand
{
    /// <summary>
    /// Plays a random hand for the enemy.
    /// </summary>
    public void GenerateRandomHand(List<BaseAttack> _attacks)
    {
        EventManager.Instance.enemyHandPlayed.Invoke(_attacks[UnityEngine.Random.Range(0, _attacks.Count)]);
    }
}
