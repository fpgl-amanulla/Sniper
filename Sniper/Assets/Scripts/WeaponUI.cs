using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour
{
    public UnityAction ScopedInaction;
    public UnityAction cancelFireAction;

    public GameObject weaponUI;

    [Header("Weapon UI Components")]
    public Image imgScope;
    public Image imgCrossHair;
    public Button btnScope;
    public Button btnCancelFire;
    public TextMeshProUGUI txtFirePopUp;

    [Header("Scope Sprite")]
    public Sprite defaultScope;
    public Sprite inactiveScope;


    private void Start()
    {
        btnCancelFire.onClick.AddListener(() => CancelFireCallBack());
    }

    public void CancelFireCallBack()
    {
        cancelFireAction.Invoke();
    }

    public void WeaponUISetActive(bool task)
    {
        weaponUI.SetActive(task);
    }
    public void ScopedCallBack()
    {
        ScopedInaction.Invoke();
        imgScope.gameObject.SetActive(true);
        btnScope.gameObject.SetActive(false);
        imgCrossHair.gameObject.SetActive(false);

        btnCancelFire.GetComponent<RectTransform>().DOAnchorPosX(0, .5f);
    }

    internal void ScopedOut()
    {
        btnCancelFire.GetComponent<RectTransform>().DOAnchorPosX(-140, .25f);
        imgScope.gameObject.SetActive(false);
        btnScope.gameObject.SetActive(true);
        imgCrossHair.gameObject.SetActive(true);
    }


    public void DeactiveScopedBtn() => btnScope.image.sprite = inactiveScope;
    public void NormalScopedBtn() => btnScope.image.sprite = defaultScope;

    public void ShowTextFirePopUp()
    {
        txtFirePopUp.gameObject.SetActive(true);
        StartCoroutine(DisableTxtFirePopUp());
    }

    IEnumerator DisableTxtFirePopUp()
    {
        yield return new WaitForSeconds(1.25f);
        txtFirePopUp.gameObject.SetActive(false);
    }
}
