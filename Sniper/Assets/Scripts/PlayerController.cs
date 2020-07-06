using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera playerCamera;

    public float scopedInFOV = 15f;
    public float normalFOV = 60;

    private Vector3 lastPos;
    private Vector3 deltaPos;
    public float speed;

    public float rotX = 0;
    public float rotY = 0;

    private void Start()
    {

    }

    private void OnEnable()
    {
        UIManager.Instance.action += ScopedIn;
    }
    private void OnDisable()
    {
        UIManager.Instance.action -= ScopedIn;
    }

    private void ScopedIn()
    {
        Debug.Log("ScopedIn");
        playerCamera.gameObject.SetActive(false);
        mainCamera.fieldOfView = scopedInFOV;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            deltaPos = Input.mousePosition - lastPos;

            rotX -= deltaPos.y * speed * Time.deltaTime;
            rotY -= deltaPos.x * speed * Time.deltaTime * -1;

            rotX = Mathf.Clamp(rotX, -15, 15);
            rotY = Mathf.Clamp(rotY, -45, 45);

            mainCamera.transform.localRotation = Quaternion.Euler(rotX, 0, 0);

            transform.rotation = Quaternion.Euler(0, rotY, 0);

            lastPos = Input.mousePosition;
        }
    }
}
