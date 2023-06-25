using MoreMountains.Feedbacks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFeedbacksListener : MonoBehaviour
{
    MMF_Player _clickFeedBacks;

    private void Awake()
    {
        _clickFeedBacks = GetComponent<MMF_Player>();
        EventManager.Instance.clickFeedback.AddListener(OnClickFeedback);
    }

    private void OnClickFeedback()
    {
        _clickFeedBacks.PlayFeedbacks();
    }

    private void OnDestroy()
    {
        EventManager.Instance.clickFeedback.RemoveListener(OnClickFeedback);
    }
}
