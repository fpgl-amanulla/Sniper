using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PanelMap : MonoBehaviour
{
    public GameObject itemMapPrefab;
    public Transform content;
    public Button btnCross;

    private List<DBProductsLandInfo> allLandInfo = new List<DBProductsLandInfo>();

    public static GameObject panelMap;
    private void Start()
    {
        panelMap = this.gameObject;
        btnCross.onClick.AddListener(() => CrossCallBack());

        PopulateItem();
    }

    private void PopulateItem()
    {
        if (allLandInfo.Count == 0)
            allLandInfo = DBProductsLandInfo.AllProductsLand();
        for (int i = 0; i < allLandInfo.Count; i++)
        {
            GameObject item = Instantiate(itemMapPrefab, content.transform);
            item.GetComponent<ItemMap>().InitItem(allLandInfo[i]);
        }
    }

    private void CrossCallBack()
    {
        PanelStart.panelStart.SetActive(true);
        Destroy(this.gameObject);

    }
}
