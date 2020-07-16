using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMap : MonoBehaviour
{
    public TextMeshProUGUI txtMapName;
    public Button btnItem;

    private Manager manager;
    private void Start()
    {
        manager = Manager.Instance;
        btnItem.onClick.AddListener(() => ItemCallBack());
    }

    private void ItemCallBack()
    {
        Manager.Instance.prefabsList.LoadPanel(Panel.Objectives, Manager.Instance.uiManager.transform);
        Destroy(PanelStart.panelStart);
        Destroy(PanelMap.panelMap);
    }

    public void InitItem(DBProductsLandInfo productsLandInfo)
    {
        txtMapName.text = productsLandInfo.land_name;
    }
}
