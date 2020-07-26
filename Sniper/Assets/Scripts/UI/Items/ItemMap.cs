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

    private MasterManager manager;
    private void Start()
    {
        manager = MasterManager.Instance;
        btnItem.onClick.AddListener(() => ItemCallBack());
    }

    private void ItemCallBack()
    {
        MasterManager.Instance.prefabsList.LoadPrefab(PrefabName.Objectives, MasterManager.Instance.uiManager.transform);
        Destroy(PanelStart.panelStart);
        Destroy(PanelMap.panelMap);
    }

    public void InitItem(DBProductsLandInfo productsLandInfo)
    {
        txtMapName.text = productsLandInfo.land_name;
    }
}
