using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTurn : MonoBehaviour
{
    
    public void OnNextTurn()
    {
        TurnManager.Instance.NextTurn();
    }
}
