using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelMyAnimal : MonoBehaviour
{
    public GameObject itemPrefab;
    public Button btnCross;
    public Transform content;

    private List<ProductData> allProductData = new List<ProductData>();
    private void Start()
    {
        AppDelegate appDelegate = AppDelegate.sharedManager();
        allProductData = appDelegate.allProductData;
        btnCross.onClick.AddListener(() => CrossCallback());
        InitItem();
    }

    private void CrossCallback()
    {
        Destroy(this.gameObject);
    }

    private void InitItem()
    {
        for (int i = 0; i < allProductData.Count; i++)
        {
            GameObject item = Instantiate(itemPrefab, content.transform);
            item.GetComponent<ItemMyAnimal>().Init(allProductData[i].myProduct);
        }
    }
}
