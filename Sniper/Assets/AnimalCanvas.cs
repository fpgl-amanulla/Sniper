using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalCanvas : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public Image imgFill;

    private float healtAmount;

    public void UpadateHealthBar(float currentHealth)
    {
        imgFill.fillAmount = (currentHealth / healtAmount);
    }

    internal void InitAnimalCanvas(Animal animal)
    {
        healtAmount = animal.health;
        txtName.text = animal.productName;
        imgFill.fillAmount = 1f;
    }
}
