using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIUpdater : MonoBehaviour
{
    /// <summary>
    /// Updates the UI to display the player's current score.
    /// </summary>
    public void UpdatePlayerUI(GameState newGameState ,int _score , TMP_Text _currentScoreCountHolder , Transform _resultsPanel)
    {
        _currentScoreCountHolder.text = _score.ToString();
        _resultsPanel.gameObject.SetActive(true);
        _resultsPanel.gameObject.GetComponent<ResultsPanel>().InitilizeData(newGameState);
    }
}
