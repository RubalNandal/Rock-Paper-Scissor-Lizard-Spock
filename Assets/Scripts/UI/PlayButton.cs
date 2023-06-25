using UnityEngine;

public class PlayButton : MonoBehaviour
{
    
    public void OnPlayClick(){
        TurnManager.Instance.NextTurn();
    }
}
