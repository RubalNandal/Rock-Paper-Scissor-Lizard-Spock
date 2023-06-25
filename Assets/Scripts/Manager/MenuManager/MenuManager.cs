using Cinemachine;
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
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera1;
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera2;
    [SerializeField]
    private Transform _resultsPanel;
    [SerializeField]
    private Transform _rotatingDots;
    [SerializeField]
    private Sprite _thinkingSprite;

    private int _score;


    ShowMainMenu _showMainMenu;
    ShowGameMenu _showGameMenu;
    EnemyUIUpdater _enemyUIUpdater;
    HighScoreUIUpdater _highScoreUpdater;
    PlayerUIUpdater _playerUIUpdater;

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

        _showMainMenu = GetComponent<ShowMainMenu>();
        _showGameMenu = GetComponent<ShowGameMenu>();
        _enemyUIUpdater = GetComponent<EnemyUIUpdater>();
        _highScoreUpdater = GetComponent<HighScoreUIUpdater>();
        _playerUIUpdater = GetComponent<PlayerUIUpdater>();
    }


    private void OnGameStateChange(GameState newGameState)
    {
        if (newGameState == GameState.MainMenu)
        {
            _showMainMenu.DisplayMainMenu(_mainMenu,_gamePanel,_virtualCamera1,_virtualCamera2);
            _highScoreUpdater.UpdateHighScoreUI(_highScoreCountHolder);
            _playerUIUpdater.UpdatePlayerUI(newGameState , _score , _currentScoreCountHolder , _resultsPanel);
        }
        else if( newGameState == GameState.PlayerTurn)
        {
            _showGameMenu.DispayGameMenu(_mainMenu, _gamePanel, _virtualCamera1, _virtualCamera2, _resultsPanel , _rotatingDots, _enemyHandTextHolder , _enemyHandDisplayHolder , _thinkingSprite); ;
        }
        else if (newGameState == GameState.Victory)
        {
            _score++;
            _playerUIUpdater.UpdatePlayerUI(newGameState, _score, _currentScoreCountHolder, _resultsPanel);
        }
        else if (newGameState == GameState.Defeat)
        {
            _playerUIUpdater.UpdatePlayerUI(newGameState, _score, _currentScoreCountHolder, _resultsPanel);
            _score = 0;
        }
        else if(newGameState == GameState.Tie)
        {
            _playerUIUpdater.UpdatePlayerUI(newGameState, _score, _currentScoreCountHolder, _resultsPanel);
        }
    }

    /// <summary>
    /// Updates the UI to display the hand played by the computer.
    /// </summary>
    /// <param name="handPlayed">The hand played by the computer.</param>
    private void UpdateEnemyUI(BaseAttack handPlayed)
    {
        _enemyUIUpdater.UpdateEnemyUI(handPlayed,_rotatingDots, _enemyHandTextHolder, _enemyHandDisplayHolder);
    }

   /* /// <summary>
    /// Updates the UI to display the player's current score.
    /// </summary>
    private void UpdatePlayerUI(GameState newGameState)
    {
        _currentScoreCountHolder.text = _score.ToString();
        _resultsPanel.gameObject.SetActive(true);
        _resultsPanel.gameObject.GetComponent<ResultsPanel>().InitilizeData(newGameState);
    }*/

    private void OnDestroy()
    {
        EventManager.Instance.gameStateChange.RemoveListener(OnGameStateChange);
        EventManager.Instance.enemyHandPlayed.RemoveListener(UpdateEnemyUI);
    }


}
