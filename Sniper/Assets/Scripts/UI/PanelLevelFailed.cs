﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLevelFailed : MonoBehaviour
{
    public Button btnRestart;

    private MasterManager manager;
    void Start()
    {
        manager = MasterManager.Instance;
        btnRestart.onClick.AddListener(() => RestartCallBack());
    }

    private void RestartCallBack()
    {
        manager.gameManager.isGameOver = false;
        manager.gameManager.isGameStarted = false;
        manager.uiManager.LoadPanelObjective();
        Destroy(this.gameObject);
    }

}
