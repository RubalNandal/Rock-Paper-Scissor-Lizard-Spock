using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultsPanel : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _header;
    [SerializeField]
    private TMP_Text _description;
    [SerializeField]
    private Button _nextRoundButtonText;
    [SerializeField]
    private Button _mainMenuButtonText;

    public void InitilizeData(GameState header)
    {
        _header.text = header.ToString();
        _description.text = CombatManager.Instance.attackDescription;

        if(header == GameState.Victory || header == GameState.Tie)
        {
            _nextRoundButtonText.gameObject.SetActive(true);
            _mainMenuButtonText.gameObject.SetActive(false);
        }
        else if(header == GameState.Defeat)
        {
            _nextRoundButtonText.gameObject.SetActive(false);
            _mainMenuButtonText.gameObject.SetActive(true);
        }
        
    }
}
