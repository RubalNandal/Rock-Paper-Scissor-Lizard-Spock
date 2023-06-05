using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    private BaseAttack _enemyHand;
    private BaseAttack _playerHand;

    [SerializeField]
    private List<BaseAttack> _attacks;
    
    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(this);
        }

        EventManager.Instance.enemyHandPlayed.AddListener(UpdateEnemyhand);
        EventManager.Instance.playerHandPlayed.AddListener(UpdatePlayerhand);
        EventManager.Instance.gameStateChange.AddListener(OnGameStateChange);
    }

    private void Start() {
        ClearPlayedHands();
    }
    

    private void OnDestroy() {
        EventManager.Instance.enemyHandPlayed.RemoveListener(UpdateEnemyhand);
        EventManager.Instance.playerHandPlayed.RemoveListener(UpdatePlayerhand);
        EventManager.Instance.gameStateChange.RemoveListener(OnGameStateChange);
    }


    private void UpdatePlayerhand(BaseAttack handPlayed)
    {
        Debug.Log("Player played : " + handPlayed.attackType.ToString());
        _playerHand = handPlayed;

        //pause game mechanics
        Time.timeScale = 0;

        GameManager.Instance.ChangeGameState(GameState.Calculation);
    }

    private void UpdateEnemyhand(BaseAttack handPlayed)
    {
        Debug.Log("Enemy played : " + handPlayed.attackType.ToString());
        _enemyHand = handPlayed;
        GameManager.Instance.ChangeGameState(GameState.PlayerTurn);
    }

    private void OnGameStateChange(GameState newGameState)
    {
        if(newGameState == GameState.Calculation){
            CalculateBattleResults();
        }
        else if(newGameState == GameState.EnemyTurn){       
            PlayRandomHand();
        }
        else if(newGameState == GameState.Victory){
            GameManager.Instance.ChangeGameState(GameState.EnemyTurn);
        }
        else if(newGameState == GameState.Defeat){
            GameManager.Instance.ChangeGameState(GameState.MainMenu);
        }
        
    }

    void PlayRandomHand(){

        EventManager.Instance.enemyHandPlayed.Invoke(_attacks[UnityEngine.Random.Range(0, _attacks.Count)]);

    }

    private void CalculateBattleResults(){

        Debug.Log("Player registered : " + _playerHand.attackType + "Vs Enemy Registed : " + _enemyHand.attackType);
        // player ran out of time
        if(_playerHand.attackType == AttackType.Empty){

            // clear hands data after calculations
            ClearPlayedHands();
            GameManager.Instance.ChangeGameState(GameState.Defeat);
        }
        //If player attack is string against enemy , Player Wins
        else if(_playerHand.strongAgainst.Contains(_enemyHand.attackType)){

            // clear hands data after calculations
            ClearPlayedHands();
            GameManager.Instance.ChangeGameState(GameState.Victory);
        }
        else if(_playerHand.weakAgainst.Contains(_enemyHand.attackType)){

            // clear hands data after calculations
            ClearPlayedHands();
            GameManager.Instance.ChangeGameState(GameState.Defeat);
        }
        else{
            // clear hands data after calculations
            ClearPlayedHands();
            GameManager.Instance.ChangeGameState(GameState.EnemyTurn);
            Debug.Log("Player is nither strong or weak against enemy");
        }

    }

    

    // empty hands when round ends
    private void ClearPlayedHands(){
        _playerHand = _enemyHand = new BaseAttack();
        //Unpause game machanics
        Time.timeScale = 1;
    }
}
