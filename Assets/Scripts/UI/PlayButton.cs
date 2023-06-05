using UnityEngine;

public class PlayButton : MonoBehaviour
{
    
    public void OnPlayClick(){
        GameManager.Instance.ChangeGameState(GameState.EnemyTurn);
    }
}
