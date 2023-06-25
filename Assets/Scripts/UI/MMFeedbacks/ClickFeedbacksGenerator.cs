using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ClickFeedbacksGenerator : MonoBehaviour
{
    Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(GenerateClickFeedback);
    }

    private void GenerateClickFeedback()
    {
        EventManager.Instance.clickFeedback.Invoke();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(GenerateClickFeedback);
    }
}
