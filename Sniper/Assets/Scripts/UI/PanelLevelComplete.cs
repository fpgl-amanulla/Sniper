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

    private void Start()
    {
        btnNext.onClick.AddListener(() => NextCallBack());
        btnAllMyAnimal.onClick.AddListener(() => AllMyAnimalCallBack());

        Level levelInfo = LevelManager.Instance.GetCurrentLevelInfo();
        txtLevelComplete.text = "Level " + (levelInfo.levelNo + 1).ToString() + " Completed";
    }

    private void AllMyAnimalCallBack()
    {
        GameObject panel = ReferenceManager.Instance.prefabsList.panelAllMyAnimalPrefab;
        Instantiate(panel, UIManager.Instance.transform);
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
