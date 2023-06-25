using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMainMenu : MonoBehaviour
{
    /// <summary>
    /// Shows the main menu UI and hides the game panel.
    /// </summary>
    public void DisplayMainMenu(Transform _mainMenu, Transform _gamePanel, CinemachineVirtualCamera _virtualCamera1, CinemachineVirtualCamera _virtualCamera2)
    {
        _mainMenu.gameObject.SetActive(true);
        _virtualCamera1.Priority = 10;
        _gamePanel.gameObject.SetActive(false);
        _virtualCamera2.Priority = 9;
    }
}
