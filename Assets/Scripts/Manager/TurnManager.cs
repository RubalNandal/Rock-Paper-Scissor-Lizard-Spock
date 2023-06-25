using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public static TurnManager Instance;


    //[SerializeField]
    private List<GameState> _turnOrder;

    private int _currentTurn = 0;

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

        _turnOrder = new List<GameState>
        {
            GameState.MainMenu,
            GameState.PlayerTurn,
            GameState.EnemyTurn,
            GameState.Calculation,
            GameState.Victory,
            GameState.Defeat,
            GameState.Tie
        };
    }

    public void NextTurn()
    {
        Debug.Log("Turn Manager current state -> " + _turnOrder[_currentTurn]);

        if (_turnOrder[_currentTurn] == GameState.Victory || _turnOrder[_currentTurn] == GameState.Tie)
        {
            _currentTurn = 1;
            GameManager.Instance.ChangeGameState(_turnOrder[_currentTurn]);
        }
        else if (_turnOrder[_currentTurn] == GameState.Defeat)
        {
            _currentTurn = 0;
            GameManager.Instance.ChangeGameState(_turnOrder[_currentTurn]);
        }
        else if(_turnOrder[_currentTurn] == GameState.Calculation)
        {
            GameState _nextState = CombatManager.Instance.CalculateBattleResults();
            _currentTurn = _turnOrder.IndexOf(_nextState);
            GameManager.Instance.ChangeGameState(_nextState);
            
        }
        else
        {
            _currentTurn = _currentTurn + 1;
            GameManager.Instance.ChangeGameState(_turnOrder[_currentTurn]);
        }

        //Debug.Log("Turn Manager next state -> " + _turnOrder[_currentTurn]);

    }

}





