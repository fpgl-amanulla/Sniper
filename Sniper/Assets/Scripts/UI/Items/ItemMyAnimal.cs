using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMyAnimal : MonoBehaviour
{
    public Image imgIcon;
    public TextMeshProUGUI txtName;

    internal void Init(DBMyProduct myProduct)
    {
        string path = Constants.animalIcon + "i" + myProduct.productid;
        imgIcon.sprite = Resources.Load<Sprite>(path);
        txtName.text = myProduct.my_name;
    }
}
