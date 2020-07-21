using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelGame : MonoBehaviour
{
    public GameObject playerHealthBar;
    public Image imgFill;

    public TextMeshProUGUI txtLevelNo;
    public TextMeshProUGUI txtKillCount;

    Level levelInfo;
    public void InitPanelGame()
    {
        levelInfo = MasterManager.Instance.levelManager.GetCurrentLevelInfo();
        MasterManager.Instance.gameManager.ResetKillCout();
        UpdateLevel();
        UpdateKillCount();
        UpdateHealthBar(10);

        imgFill.fillAmount = 1;
    }

    public void UpdateLevel()
    {
        txtLevelNo.text = "Level " + (levelInfo.levelNo + 1).ToString();
    }

    public void UpdateKillCount()
    {
        string count = MasterManager.Instance.gameManager.killCount.ToString();
        txtKillCount.text = "Objectives Completed: " + count + "/" + levelInfo.animalToHunt;
    }
    public void UpdateHealthBar(float currentHealth)
    {
        float amount = (float)currentHealth / 5;
        imgFill.fillAmount = amount;
        if (amount <= 0)
        {
            playerHealthBar.SetActive(false);
        }
    }
}
