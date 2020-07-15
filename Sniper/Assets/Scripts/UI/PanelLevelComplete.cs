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
    public Button btnAllMyAnimal;

    private Manager manager;
    private void Start()
    {
        manager = Manager.Instance;

        btnNext.onClick.AddListener(() => NextCallBack());
        btnAllMyAnimal.onClick.AddListener(() => AllMyAnimalCallBack());

        Level levelInfo = manager.levelManager.GetCurrentLevelInfo();
        txtLevelComplete.text = "Level " + (levelInfo.levelNo + 1).ToString() + " Completed";
    }

    private void AllMyAnimalCallBack()
    {
        Manager.Instance.prefabsList.LoadPanel(Panel.AllMyAnimal, manager.uiManager.transform);
    }

    private void NextCallBack()
    {
        LevelUp();
        manager.gameManager.isGameOver = false;
        manager.gameManager.isGameStarted = false;
        manager.uiManager.LoadPanelObjective();
        Destroy(this.gameObject);
    }

    private static void LevelUp()
    {
        DBUserInfo userInfo = DBUserInfo.Create(1);
        userInfo.level++;
        userInfo.UpdateDatabase();
    }
}
