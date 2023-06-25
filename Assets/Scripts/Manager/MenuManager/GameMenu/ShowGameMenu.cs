using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowGameMenu : MonoBehaviour
{
    /// <summary>
    /// Shows the Game menu UI and hides the main menu panel.
    /// </summary>
    public void DispayGameMenu(Transform _mainMenu, Transform _gamePanel, CinemachineVirtualCamera _virtualCamera1, CinemachineVirtualCamera _virtualCamera2, Transform _resultsPanel, Transform _rotatingDots, TMP_Text _enemyHandTextHolder, Transform _enemyHandDisplayHolder, Sprite _thinkingSprite)
    {
        _mainMenu.gameObject.SetActive(false);
        _virtualCamera1.Priority = 9;
        _gamePanel.gameObject.SetActive(true);
        _virtualCamera2.Priority = 10;

        //set thinking UI
        _resultsPanel.gameObject.SetActive(false);
        _rotatingDots.transform.gameObject.SetActive(true);
        _enemyHandTextHolder.text = "Computer is thinking ... ";
        if (_enemyHandDisplayHolder.TryGetComponent(out Image enemyHandImage))
        {
            enemyHandImage.sprite = _thinkingSprite;
        }
    }
}
