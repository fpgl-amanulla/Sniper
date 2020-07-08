using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public GameObject playerHealthBar;
    public Image imgFill;

    private void Start()
    {
        imgFill.fillAmount = 1;
    }

    public void UpdateHealthBar(int currentHealth)
    {
        float amount = (float)currentHealth / 10;
        imgFill.fillAmount = amount;
        if (amount <= 0)
        {
            playerHealthBar.SetActive(false);
        }
    }
}
