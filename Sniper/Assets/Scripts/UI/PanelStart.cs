using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelStart : MonoBehaviour
{
    public Button btnPlay;
    public Button btnMyCollection;
    public Button btnWeapon;

    private MasterManager manager;
    public static GameObject panelStart;
    private void Start()
    {
        manager = MasterManager.Instance;
        panelStart = this.gameObject;
        btnPlay.onClick.AddListener(() => PlayCallBack());
        btnMyCollection.onClick.AddListener(() => MyCollectionCallBack());
        btnWeapon.onClick.AddListener(() => WeaponCallBack());
    }

    private void PlayCallBack()
    {
        manager.prefabsList.LoadPrefab(PrefabName.Objectives, manager.uiManager.transform);
        this.gameObject.SetActive(false);
    }

    private void MyCollectionCallBack()
    {
        manager.prefabsList.LoadPrefab(PrefabName.AllMyAnimal, manager.uiManager.transform);

    }
    private void WeaponCallBack()
    {

    }

}
