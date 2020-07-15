﻿using DG.Tweening;
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
        ProductData.ReloadProductData();

        ReferenceManager.Instance.panelGame.gameObject.SetActive(false);
        LoadPanelObjective();
    }

    public void LoadPanelObjective()
    {
        weaponUI.SetActive(false);
        ReferenceManager.Instance.prefabsList.LoadPanel(Panel.Objectives, this.transform);
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
