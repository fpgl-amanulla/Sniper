﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public bool isGameOver { get; set; }
    public bool isGameStarted { get; set; }
    public bool activeEnemyFire { get; set; }

    public int killCount { get; set; }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        isGameOver = false;
        isGameStarted = false;
        activeEnemyFire = false;
    }

    public int GetCurrentKillCount() => killCount;
    public void ResetKillCout() => killCount = 0;

    private void OnApplicationQuit()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            DatabaseManager.sharedManager().databaseBinary.Close();
            DatabaseManager.sharedManager().databaseDocument.Close();
        }
    }
}
