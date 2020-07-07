using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UnityAction action;

    public Image imgScope;
    public Image imgCrossHair;
    public Button btnScope;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {

    }

    public void ScopedCallBack()
    {
        action.Invoke();
        imgScope.gameObject.SetActive(true);
        btnScope.gameObject.SetActive(false);
        imgCrossHair.gameObject.SetActive(false);
    }

    internal void ScopedOut()
    {
        imgScope.gameObject.SetActive(false);
        btnScope.gameObject.SetActive(true);
        imgCrossHair.gameObject.SetActive(true);
        StartCoroutine(WaitToScopedOut());
    }

    IEnumerator WaitToScopedOut()
    {
        yield return new WaitForSeconds(.25f);

    }
}
