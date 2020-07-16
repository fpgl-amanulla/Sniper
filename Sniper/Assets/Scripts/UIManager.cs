﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Manager manager;
    public GameObject weaponUI;
    private void Awake()
    {
        AppDelegate.SharedManager();
        ProductData.ReloadProductData();
    }

    private void Start()
    {
        manager = Manager.Instance;
        manager.panelGame.gameObject.SetActive(false);
        LoadPanelObjective();
    }

    public void LoadPanelObjective()
    {
        weaponUI.SetActive(false);
        manager.prefabsList.LoadPanel(Panel.Objectives, this.transform);
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
