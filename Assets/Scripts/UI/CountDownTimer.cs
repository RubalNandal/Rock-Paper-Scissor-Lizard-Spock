using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    
    public float countDownTime = 2;

    private float _timeRemaining;
    private bool _timerIsRunning = false;
    private string _floatFormat = "0.0";

    [SerializeField]
    private Slider _timerSlider;
    
    [SerializeField]
    private TMP_Text _timerText;

    private void Start()
    {
        
        _timerIsRunning = true;
        _timeRemaining = countDownTime;
    }

    private void Awake() {
        EventManager.Instance.gameStateChange.AddListener(StartTimer);
    }

    private void OnDestroy() {
        EventManager.Instance.gameStateChange.RemoveListener(StartTimer);
    }

    void StartTimer(GameState newGameState){

        if(newGameState == GameState.PlayerTurn){

            _timerIsRunning  = true;
            _timeRemaining = countDownTime;
        }
        
    }

    void Update()
    {
        if (_timerIsRunning)
        {
            if (_timeRemaining > 0)
            {
                
                _timeRemaining -= Time.deltaTime;
                _timerText.text = _timeRemaining.ToString(_floatFormat);
                _timerSlider.value = _timeRemaining / countDownTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                _timerText.text ="Time Out";
                _timerSlider.value = 0;
                _timeRemaining = 0;
                _timerIsRunning = false;
                GameManager.Instance.ChangeGameState(GameState.Calculation);
            }
        }
    }
}
