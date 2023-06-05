using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    GameState _gameState;
    int _score;

    private void Awake() {
        
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(this);
        }
    }

    private void Start() {
        ChangeGameState(GameState.MainMenu);
    }

    public void ChangeGameState(GameState newGameState){
        _gameState = newGameState;

        // can write logics that needs to be executed before raising events
        switch(newGameState){
            case GameState.MainMenu:{
                CheckHighScore();
                break;
            }
            case GameState.PlayerTurn :
            {
                Debug.Log("Player Turn"); 
                // unpause the game mechanics when player turn starts
                Time.timeScale = 1;
                break;
            }
            case GameState.EnemyTurn :
            {
                Debug.Log("Enemy Turn"); 
                break;
            }
            case GameState.Calculation :
            {
                Debug.Log("Computing battle results"); 
                break;
            }
            case GameState.Victory :
            {
                Debug.Log("Round Won");
                _score++;
                break;
            }
            case GameState.Defeat :
            {
                CheckHighScore();
                Debug.Log("Round Lost");
                break;
            }
        }

        EventManager.Instance.gameStateChange.Invoke(newGameState);

    }


    public void CheckHighScore()
    {

        if (PlayerPrefs.HasKey(EnvConstants.HIGH_SCORE_KEY))
        {
            if(_score > PlayerPrefs.GetInt(EnvConstants.HIGH_SCORE_KEY))
            {
                PlayerPrefs.SetInt(EnvConstants.HIGH_SCORE_KEY, _score);
            }
            
        }
        else
        {
            PlayerPrefs.SetInt(EnvConstants.HIGH_SCORE_KEY, _score);
        }

        // reset current score after high score update
        _score = 0;
    }
    

    

}

public enum GameState{
    MainMenu,
    EnemyTurn,
    PlayerTurn,
    Calculation,
    Victory,
    Defeat,
}
