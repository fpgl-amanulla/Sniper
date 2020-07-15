using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelLevelComplete : MonoBehaviour
{
    public TextMeshProUGUI txtLevelComplete;
    public Button btnNext;

    private void Start()
    {
        btnNext.onClick.AddListener(() => NextCallBack());

        Level levelInfo = LevelManager.Instance.GetCurrentLevelInfo();
        txtLevelComplete.text = "Level " + (levelInfo.levelNo + 1).ToString() + " Completed";
    }

    private void NextCallBack()
    {
        LevelUp();
        GameManager.Instance.isGameOver = false;
        GameManager.Instance.isGameStarted = false;
        UIManager.Instance.LoadPanelObjective();
        Destroy(this.gameObject);

    }

    private static void LevelUp()
    {
        DBUserInfo userInfo = DBUserInfo.Create(1);
        userInfo.level++;
        userInfo.UpdateDatabase();
    }
}
