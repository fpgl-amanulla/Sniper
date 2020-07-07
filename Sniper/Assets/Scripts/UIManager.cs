using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UnityAction ScopedInaction;
    public UnityAction cancelFireAction;

    public Image imgScope;
    public Image imgCrossHair;
    public Button btnScope;
    public Button btnCancelFire;


    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        btnCancelFire.onClick.AddListener(() => CancelFireCallBack());
    }

    public void CancelFireCallBack()
    {
        cancelFireAction.Invoke();
    }

    public void ScopedCallBack()
    {
        ScopedInaction.Invoke();
        imgScope.gameObject.SetActive(true);
        btnScope.gameObject.SetActive(false);
        imgCrossHair.gameObject.SetActive(false);

        btnCancelFire.GetComponent<RectTransform>().DOAnchorPosX(20, .5f);
    }

    internal void ScopedOut()
    {
        btnCancelFire.GetComponent<RectTransform>().DOAnchorPosX(-140, .25f);
        imgScope.gameObject.SetActive(false);
        btnScope.gameObject.SetActive(true);
        imgCrossHair.gameObject.SetActive(true);
        StartCoroutine(WaitToScopedOut());
    }

    IEnumerator WaitToScopedOut()
    {
        yield return new WaitForSeconds(.25f);

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
