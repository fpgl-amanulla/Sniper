using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelObjective : MonoBehaviour
{
    public static PanelObjective Instance = null;

    public TextMeshProUGUI txtLevelNo;
    public Image imgVictim;
    public TextMeshProUGUI txtObjective;
    public Button btnPlay;

    private GameObject playerCamera;
    private void Start()
    {
        if (Instance == null) Instance = this;
        playerCamera = ReferenceManager.Instance.playerCamera;
        btnPlay.onClick.AddListener(() => PlayCallBack());
        LoadPanelObjective();
    }

    public void LoadPanelObjective()
    {
        this.gameObject.SetActive(true);
        playerCamera.SetActive(false);
        InitPanelObjective();
    }

    private void PlayCallBack()
    {
        playerCamera.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void InitPanelObjective()
    {

    }
}
