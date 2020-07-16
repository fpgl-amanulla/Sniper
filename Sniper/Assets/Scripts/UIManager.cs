using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private Manager manager;
    public GameObject weaponUI;
    private void Awake()
    {
        AppDelegate.sharedManager();
        ProductData.ReloadProductData();
    }

    private void Start()
    {
        manager = Manager.Instance;
        LoadPanelStart();
    }

    private void LoadPanelStart()
    {
        weaponUI.SetActive(false);
        manager.panelGame.gameObject.SetActive(false);
        manager.prefabsList.LoadPanel(Panel.Start, this.transform);
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
