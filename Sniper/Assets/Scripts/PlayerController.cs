﻿using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour, ITakeDamage
{
    [SerializeField] private float health = 10;

    public Camera mainCamera;
    public Camera playerCamera;
    public WeaponUI weaponUI;

    public float scopedInFOV = 15f;
    public float normalFOV = 60;

    private Vector3 lastPos;
    private Vector3 deltaPos;
    public float speed;

    private float rotX = 0;
    private float rotY = 0;

    private RaycastHit hit;
    private bool isScopedIn = false;

    private bool cancelFire = false;

    private MasterManager manager;
    private void Start()
    {
        manager = MasterManager.Instance;
        weaponUI.ScopedInaction += ScopedIn;
        weaponUI.cancelFireAction += CancelFire;
    }

    private void CancelFire()
    {
        cancelFire = true;
        ScopedOut();
        isScopedIn = false;
    }
    private void OnDestroy()
    {
        weaponUI.ScopedInaction -= ScopedIn;
        weaponUI.cancelFireAction -= CancelFire;
    }

    private void ScopedIn()
    {
        isScopedIn = true;
        DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, scopedInFOV, .5f);
        playerCamera.gameObject.SetActive(false);
    }

    private void ScopedOut()
    {
        DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, normalFOV, .5f);
        StartCoroutine(WaitToScopedOut());
        if (cancelFire == false)
            Fire();
    }

    IEnumerator WaitToScopedOut()
    {
        yield return new WaitForSeconds(.35f);
        weaponUI.ScopedOut();
        playerCamera.gameObject.SetActive(true);
    }

    private void Fire()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 1000))
        {
            GameObject fireEffect = Instantiate(manager.fxManager.fireEffect, hit.point, Quaternion.identity);
            Destroy(fireEffect, 2.0f);

            if (hit.distance > 100)
            {
                Debug.Log("Not in Range");
            }

            Animal animal = hit.transform.GetComponentInParent<Animal>();
            if (animal != null)
            {
                animal.TakeDamage(1);
            }
            if (hit.collider.CompareTag("Head"))
            {
                weaponUI.ShowTextFirePopUp();
            }

            GameManager.Instance.activeEnemyFire = true;
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameOver && GameManager.Instance.isGameStarted)
        {
            if (Application.isEditor)
            {
                InputHandler();
            }
            else
            {
                if (Input.touchCount == 1)
                    InputHandler();
            }
        }
    }

    private void InputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
            weaponUI.DeactiveScopedBtn();
        }
        else if (Input.GetMouseButton(0))
        {
            deltaPos = Input.mousePosition - lastPos;

            rotX -= deltaPos.y * speed * Time.deltaTime;
            rotY -= deltaPos.x * speed * Time.deltaTime * -1;

            rotX = Mathf.Clamp(rotX, -25, 15);
            rotY = Mathf.Clamp(rotY, -50, 50);

            mainCamera.transform.localRotation = Quaternion.Euler(rotX, 0, 0);

            transform.rotation = Quaternion.Euler(0, rotY, 0);

            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (cancelFire == false)
            {
                if (isScopedIn)
                {
                    ScopedOut();
                    isScopedIn = false;
                }
            }
            cancelFire = false;
            weaponUI.NormalScopedBtn();
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        manager.panelGame.UpdateHealthBar(health);
        if (health <= 0)
        {
            GameManager.Instance.isGameOver = true;
            weaponUI.WeaponUISetActive(false);
            playerCamera.gameObject.SetActive(false);
            StartCoroutine(LoadLevelfailedPanel());
        }
    }

    IEnumerator LoadLevelfailedPanel()
    {
        yield return new WaitForSeconds(2.0f);
        manager.panelGame.gameObject.SetActive(false);
        GameObject panelLevelfailed = manager.prefabsList.panelLevelFailedPrefab;
        Instantiate(panelLevelfailed, manager.uiManager.transform);
        manager.levelManager.ResetLevel();

        health = 10;
    }
}
