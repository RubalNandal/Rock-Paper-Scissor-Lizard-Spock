using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents a playable hand in the game.
/// </summary>
public class PlayHand : MonoBehaviour
{
    [SerializeField]
    private BaseAttack _handAttack;

    [SerializeField]
    private Transform _handImage;

    [SerializeField]
    private TMP_Text _attackName;


    

    /// <summary>
    /// Invoked when the player plays the hand.
    /// </summary>
    public void OnHandPlayed(){
        
        EventManager.Instance.playerHandPlayed.Invoke(_handAttack);
    }

    private void Start() {
        _attackName.text = _handAttack.attackType.ToString();

        if(_handImage.TryGetComponent(out Image enemyHandImage)){
            enemyHandImage.sprite = _handAttack.attackImage;
        }
        
    }
}
