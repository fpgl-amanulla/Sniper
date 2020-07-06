using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UnityAction action;

    public Image imgScope;
    public Button btnScope;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        //btnScope.OnPointerEnter(()=>ScopedCallBack());

    }

    public void ScopedCallBack()
    {
        action.Invoke();
        imgScope.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
