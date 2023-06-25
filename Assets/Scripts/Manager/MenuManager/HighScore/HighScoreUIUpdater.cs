using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreUIUpdater : MonoBehaviour
{
    /// <summary>
    /// Updates the UI to display the high score.
    /// </summary>
    public void UpdateHighScoreUI(TMP_Text _highScoreCountHolder)
    {
        _highScoreCountHolder.text = PlayerPrefs.GetInt(EnvConstants.HIGH_SCORE_KEY).ToString();
    }
}
