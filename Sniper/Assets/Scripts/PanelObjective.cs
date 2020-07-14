﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelObjective : MonoBehaviour
{
    public TextMeshProUGUI txtLevelNo;
    public Image imgVictim;
    public TextMeshProUGUI txtObjective;
    public Button btnPlay;

    private GameObject playerCamera;
    private void Start()
    {
        playerCamera = ReferenceManager.Instance.playerCamera;
        btnPlay.onClick.AddListener(() => PlayCallBack());
        InitPanelObjective();
    }

    public void LoadPanelObjective()
    {
        this.gameObject.SetActive(true);
        playerCamera.SetActive(false);
        InitPanelObjective();
    }

    private void PlayCallBack()
    {
        ReferenceManager.Instance.playerUI.UpdateLevel();
        ReferenceManager.Instance.playerUI.UpdateKillCount();
        UIManager.Instance.weaponUI.SetActive(true);
        ReferenceManager.Instance.playerUI.gameObject.SetActive(true);
        playerCamera.SetActive(true);
        LevelManager.Instance.LoadLevel();
        Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
    }

    public void InitPanelObjective()
    {
        playerCamera.SetActive(false);

        Level levelInfo = LevelManager.Instance.GetCurrentLevelInfo();

        txtLevelNo.text = "level " + (levelInfo.levelNo + 1).ToString();

        imgVictim.sprite = Resources.Load<Sprite>("Animal/Icon/i" + levelInfo.selectedAnimalId);

        DBProductInfo productInfo = DBProductInfo.GetProductInfo(levelInfo.selectedAnimalId);
        string objective = "Hunt " + levelInfo.animalToHunt + " " + productInfo.product_name + " Shark";
        txtObjective.text = objective;
    }
}