using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField]
    private Transform _mainMenu , _gamePannel;

    [SerializeField]
    private Transform _enemyHandDisaplyHolder;
    [SerializeField]
    private TMP_Text _enemyHandTextHolder;
    [SerializeField]
    private TMP_Text _highScoreCountHolder;
    [SerializeField]
    private TMP_Text _currentScoreCountHolder;

    int _score;

    private void Awake() {
        
        if(Instance == null){
            Instance = this;
        }
        else{
            Destroy(this);
        }

        EventManager.Instance.gameStateChange.AddListener(OnGameStateChange);
        EventManager.Instance.enemyHandPlayed.AddListener(UpdateEnemyUI);
    }

    private void OnDestroy() {
        EventManager.Instance.gameStateChange.RemoveListener(OnGameStateChange);
        EventManager.Instance.enemyHandPlayed.RemoveListener(UpdateEnemyUI);
    }
    public void ShowMainMenu(){
        _mainMenu.gameObject.SetActive(true);
        _gamePannel.gameObject.SetActive(false);
    }

    private void OnGameStateChange(GameState newGameState){
        if(newGameState == GameState.MainMenu){
            ShowMainMenu();
            UpdateHighScoreUI();
            UpdatePlayerUI();
        }
        else if(newGameState == GameState.Victory)
        {
            _score++;
            UpdatePlayerUI();
        }
        else if(newGameState == GameState.Defeat)
        {
            _score = 0;
        }
        
        
    }

    // updates the UI for what hand computer played
    private void UpdateEnemyUI(BaseAttack handPlayed){

        _enemyHandTextHolder.text = "Computer played " + handPlayed.attackType.ToString();

        if(_enemyHandDisaplyHolder.TryGetComponent(out Image enemyHandImage)){
            enemyHandImage.sprite = handPlayed.attackImage;
        }

    }

    private void UpdatePlayerUI(){

        _currentScoreCountHolder.text = _score.ToString();

    }

    private void UpdateHighScoreUI()
    {
        _highScoreCountHolder.text = PlayerPrefs.GetInt(EnvConstants.HIGH_SCORE_KEY).ToString();
    }

    //private void UpdateHighScoreUI()
    //{
    //    _highScoreCountHolder.text = PlayerPrefs.GetInt(EnvConstants.HIGH_SCORE_KEY).ToString();
    //}

}
