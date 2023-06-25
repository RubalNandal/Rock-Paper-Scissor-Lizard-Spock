using UnityEngine;

public class HighScoreUpdater : MonoBehaviour , IHighScore
{
    /// <summary>
    /// Checks and updates the high score if necessary.
    /// </summary>
    public void UpdateHighScore(int _score)
    {
        if (PlayerPrefs.HasKey(EnvConstants.HIGH_SCORE_KEY))
        {
            if (_score > PlayerPrefs.GetInt(EnvConstants.HIGH_SCORE_KEY))
            {
                PlayerPrefs.SetInt(EnvConstants.HIGH_SCORE_KEY, _score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(EnvConstants.HIGH_SCORE_KEY, _score);
        }

    }
}
