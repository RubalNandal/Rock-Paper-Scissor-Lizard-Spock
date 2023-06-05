using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the menu UI and updates UI elements based on game events.
/// </summary>
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField]
    private Transform _mainMenu, _gamePanel;

    [SerializeField]
    private Transform _enemyHandDisplayHolder;
    [SerializeField]
    private TMP_Text _enemyHandTextHolder;
    [SerializeField]
    private TMP_Text _highScoreCountHolder;
    [SerializeField]
    private TMP_Text _currentScoreCountHolder;

    private int _score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        EventManager.Instance.gameStateChange.AddListener(OnGameStateChange);
        EventManager.Instance.enemyHandPlayed.AddListener(UpdateEnemyUI);
    }

    private void OnDestroy()
    {
        EventManager.Instance.gameStateChange.RemoveListener(OnGameStateChange);
        EventManager.Instance.enemyHandPlayed.RemoveListener(UpdateEnemyUI);
    }

    /// <summary>
    /// Shows the main menu UI and hides the game panel.
    /// </summary>
    public void ShowMainMenu()
    {
        _mainMenu.gameObject.SetActive(true);
        _gamePanel.gameObject.SetActive(false);
    }

    private void OnGameStateChange(GameState newGameState)
    {
        if (newGameState == GameState.MainMenu)
        {
            ShowMainMenu();
            UpdateHighScoreUI();
            UpdatePlayerUI();
        }
        else if (newGameState == GameState.Victory)
        {
            _score++;
            UpdatePlayerUI();
        }
        else if (newGameState == GameState.Defeat)
        {
            _score = 0;
        }
    }

    /// <summary>
    /// Updates the UI to display the hand played by the computer.
    /// </summary>
    /// <param name="handPlayed">The hand played by the computer.</param>
    private void UpdateEnemyUI(BaseAttack handPlayed)
    {
        _enemyHandTextHolder.text = "Computer played " + handPlayed.attackType.ToString();

        if (_enemyHandDisplayHolder.TryGetComponent(out Image enemyHandImage))
        {
            enemyHandImage.sprite = handPlayed.attackImage;
        }
    }

    /// <summary>
    /// Updates the UI to display the player's current score.
    /// </summary>
    private void UpdatePlayerUI()
    {
        _currentScoreCountHolder.text = _score.ToString();
    }

    /// <summary>
    /// Updates the UI to display the high score.
    /// </summary>
    private void UpdateHighScoreUI()
    {
        _highScoreCountHolder.text = PlayerPrefs.GetInt(EnvConstants.HIGH_SCORE_KEY).ToString();
    }
}
