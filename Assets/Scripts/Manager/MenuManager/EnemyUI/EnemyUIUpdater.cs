using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIUpdater : MonoBehaviour
{
    /// <summary>
    /// Updates the UI to display the hand played by the computer.
    /// </summary>
    /// <param name="handPlayed">The hand played by the computer.</param>
    public   void UpdateEnemyUI(BaseAttack handPlayed ,Transform _rotatingDots , TMP_Text _enemyHandTextHolder , Transform _enemyHandDisplayHolder)
    {
        // turns off thinking animation 
        _rotatingDots.gameObject.SetActive(false);

        _enemyHandTextHolder.text = "Computer played " + handPlayed.attackType.ToString();

        if (_enemyHandDisplayHolder.TryGetComponent(out Image enemyHandImage))
        {
            enemyHandImage.sprite = handPlayed.attackImage;
        }
    }
}
