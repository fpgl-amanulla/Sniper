using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : WeaponUI
{
    public static UIManager Instance;


    private void Awake()
    {
        if (Instance == null) Instance = this;

        AppDelegate appDelegate = AppDelegate.SharedManager();

        weaponUI.SetActive(false);
        ReferenceManager.Instance.playerUI.gameObject.SetActive(false);
        LoadPanelObjective();
    }

    public void LoadPanelObjective()
    {
        weaponUI.SetActive(false);
        GameObject panel = ReferenceManager.Instance.prefabsList.panelObjectivePrefab;
        Instantiate(panel, this.transform);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
