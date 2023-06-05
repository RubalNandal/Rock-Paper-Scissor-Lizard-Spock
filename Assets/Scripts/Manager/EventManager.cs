using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    
    private void Awake() {
        
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(this);
        }
    }


    //called when game state is changed
    public UnityEvent<GameState> gameStateChange;
    
    // called when player plays there hand
    public UnityEvent<BaseAttack> playerHandPlayed;

    // called when enemy plays there hand
    public UnityEvent<BaseAttack> enemyHandPlayed;

    // called when round ends

    public UnityEvent roundEnded;
}
