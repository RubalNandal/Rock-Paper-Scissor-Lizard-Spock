using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayHand : MonoBehaviour
{
    [SerializeField]
    private BaseAttack _handAttack;

    [SerializeField]
    private Transform _handImage;

    [SerializeField]
    private TMP_Text _attackName;


    

    // called by UI buttons when player plas hand
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
