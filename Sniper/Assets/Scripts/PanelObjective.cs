using System;
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

    private MasterManager manager;
    private void Start()
    {
        manager = MasterManager.Instance;
        playerCamera = MasterManager.Instance.playerCamera;
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
        manager.panelGame.gameObject.SetActive(true);
        manager.panelGame.InitPanelGame();
        manager.uiManager.weaponUI.SetActive(true);
        playerCamera.SetActive(true);
        manager.gameManager.isGameStarted = true;
        manager.levelManager.LoadLevel();
        Destroy(this.gameObject);
    }

    public void InitPanelObjective()
    {
        playerCamera.SetActive(false);

        Level levelInfo = manager.levelManager.GetCurrentLevelInfo();

        txtLevelNo.text = "Mission " + (levelInfo.levelNo + 1).ToString();

        imgVictim.sprite = Resources.Load<Sprite>("Animal/Icon/i" + levelInfo.selectedAnimalId);

        DBProductInfo productInfo = DBProductInfo.GetProductInfo(levelInfo.selectedAnimalId);
        string objective = "Hunt " + levelInfo.animalToHunt + " " + productInfo.product_name + " Shark";
        txtObjective.text = objective;
    }
}
