using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLevelFailed : MonoBehaviour
{
    public Button btnRestart;

    void Start()
    {
        btnRestart.onClick.AddListener(() => RestartCallBack());
    }

    private void RestartCallBack()
    {
        GameManager.Instance.isGameOver = false;
        GameManager.Instance.isGameStarted = false;
        UIManager.Instance.LoadPanelObjective();
        Destroy(this.gameObject);
    }

}
