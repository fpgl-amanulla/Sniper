using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int health = 10;

    public Camera mainCamera;
    public Camera playerCamera;

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
    private void Start()
    {
        UIManager.Instance.ScopedInaction += ScopedIn;
        UIManager.Instance.cancelFireAction += CancelFire;
    }

    private void CancelFire()
    {
        cancelFire = true;
        ScopedOut();
        isScopedIn = false;
    }
    private void OnDestroy()
    {
        UIManager.Instance.ScopedInaction -= ScopedIn;
        UIManager.Instance.cancelFireAction -= CancelFire;
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
        UIManager.Instance.ScopedOut();
        playerCamera.gameObject.SetActive(true);
    }

    private void Fire()
    {
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 1000))
        {
            GameObject fireEffect = Instantiate(FXManager.Instance.fireEffect, hit.point, Quaternion.identity);
            Destroy(fireEffect, 2.0f);

            Enemy enemy = hit.transform.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                Destroy(enemy.gameObject.GetComponent<NavMeshAgent>());
                enemy.TakeDamage(1);

                enemy.gameObject.AddComponent(typeof(Rigidbody));
                enemy.GetComponent<Rigidbody>().AddForce(10 * -hit.normal, ForceMode.Impulse);
            }
            //if (hit.rigidbody != null)
            //{
            //    hit.rigidbody.AddForce(10 * -hit.normal, ForceMode.Impulse);
            //}
            if (hit.collider.CompareTag("Head"))
            {
                UIManager.Instance.ShowTextFirePopUp();
            }

            GameManager.Instance.activeEnemyFire = true;
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.isGameOver)
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
            UIManager.Instance.DeactiveScopedBtn();
        }
        else if (Input.GetMouseButton(0))
        {
            deltaPos = Input.mousePosition - lastPos;

            rotX -= deltaPos.y * speed * Time.deltaTime;
            rotY -= deltaPos.x * speed * Time.deltaTime * -1;

            rotX = Mathf.Clamp(rotX, -5, 10);
            rotY = Mathf.Clamp(rotY, -45, 45);

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
            UIManager.Instance.NormalScopedBtn();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        ReferenceManager.Instance.playerUI.UpdateHealthBar(health);
        if (health <= 0)
        {
            GameManager.Instance.isGameOver = true;
            UIManager.Instance.WeaponUISetActive(false);
            playerCamera.gameObject.SetActive(false);
        }
    }
}
