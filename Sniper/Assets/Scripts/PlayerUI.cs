using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject playerHealthBar;
    public Image imgFill;

    public TextMeshProUGUI txtLevelNo;
    public TextMeshProUGUI txtKillCount;
    private void OnEnable()
    {
        imgFill.fillAmount = 1;
    }

    public void UpdateLevel()
    {
        txtLevelNo.text = "Level " + (LevelManager.Instance.GetCurrentLevelInfo().levelNo + 1).ToString();
    }

    public void UpdateKillCount()
    {
        txtKillCount.text = "Objectives Completed: " + GameManager.Instance.killCount.ToString();
    }
    public void UpdateHealthBar(float currentHealth)
    {
        float amount = (float)currentHealth / 10;
        imgFill.fillAmount = amount;
        if (amount <= 0)
        {
            playerHealthBar.SetActive(false);
        }
    }
}
