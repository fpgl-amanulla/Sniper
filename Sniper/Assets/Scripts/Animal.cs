using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AnimalType
{
    Big,
    Small
}


public class Animal : AnimalData, ITakeDamage
{
    public AnimalType animalType;
    private Animator animator;
    public float movementSpeed;
    public float rotationSpeed;

    public float stopDistance;
    public Vector3 destination;
    public bool reachedDestination;

    public List<Transform> waypoints = new List<Transform>();
    public AnimalCanvas animalCanvas;
    private bool isDied = false;
    private bool isAttacking = false;

    private MasterManager manager;
    private void Start()
    {
        manager = MasterManager.Instance;
        InitAnimalData();
        waypoints = manager.waypoints;
        animator = GetComponent<Animator>();
        SetDestination();
    }

    private void InitAnimalData()
    {
        if (animalType == AnimalType.Big)
        {
            GameObject canvas = manager.prefabsList.LoadPrefab(PrefabName.AnimalCanvas, this.transform);
            animalCanvas = canvas.GetComponent<AnimalCanvas>();
            movementSpeed = Random.Range(4, 7);
            rotationSpeed = Random.Range(100, 120);
        }
        else
        {
            movementSpeed = Random.Range(2, 5);
            rotationSpeed = Random.Range(100, 120);
        }
        //animalCanvas.InitAnimalCanvas(health, productName);
        stopDistance = 2.5f;
    }

    private void Update()
    {
        if (isAttacking && !isDied)
        {
            AttackPlayer();
        }
        else
        {
            Move();
        }

    }

    private void Move()
    {
        Vector3 desDirection = destination - transform.position;

        float desDistance = desDirection.magnitude;

        if (desDistance >= stopDistance)
        {
            reachedDestination = false;
            Quaternion targetRotation = Quaternion.LookRotation(desDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            reachedDestination = true;
            SetDestination();

        }
    }

    public void SetDestination()
    {
        destination = waypoints[Random.Range(0, waypoints.Count)].position;
    }

    public void TakeDamage(float damageAmount)
    {
        if (animalType == AnimalType.Big)
        {
            health -= damageAmount;
            animalCanvas.UpadateHealthBar(health);
            if (health <= 0)
            {
                isDied = true;
                animalCanvas.gameObject.SetActive(false);
                movementSpeed = 0;
                rotationSpeed = 0;
                if (this.productId == manager.levelManager.GetCurrentLevelInfo().selectedAnimalId)
                {
                    manager.gameManager.killCount++;
                    manager.panelGame.UpdateKillCount();
                    if (manager.gameManager.killCount >= manager.levelManager.GetCurrentLevelInfo().animalToHunt)
                    {
                        Debug.Log("Level Complete");
                        manager.gameManager.isGameOver = true;
                        StartCoroutine(LoadPanelLevelComplete());
                    }
                }
                Die();

            }
            else
            {
                isAttacking = true;
                transform.DOShakePosition(1.0f);
                transform.DOShakeScale(.1f);
                movementSpeed += Random.Range(20, 25);
                rotationSpeed += Random.Range(250, 300);
            }
        }
        else
        {
            Die();
        }
    }

    private IEnumerator LoadPanelLevelComplete()
    {
        yield return new WaitForSeconds(1.5f);
        manager.panelGame.gameObject.SetActive(false);
        manager.prefabsList.LoadPrefab(PrefabName.LevelComplete, manager.uiManager.transform);
        manager.levelManager.ResetLevel();
    }

    private void AttackPlayer()
    {
        Transform playerTransform = manager.player.transform;
        Vector3 desDirection = playerTransform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(desDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        float distance = Vector3.Distance(this.transform.position, playerTransform.position);
        if (distance < 3.5f)
        {
            movementSpeed = 0;
            rotationSpeed = 0;
            if (!manager.gameManager.isGameOver)
            {
                animator.SetTrigger("Attack");
                playerTransform.GetComponent<PlayerController>().TakeDamage(.05f);
            }
        }
    }

    private void Die()
    {
        ProductData.CreateProductInDatabase(productId);

        transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 180), 1f).OnComplete(OncompleteCallBack);
        GameObject deathFx = Instantiate(manager.fxManager.deathEffect, transform.position, Quaternion.identity);
        Destroy(deathFx, 1.5f);
        Destroy(gameObject, 4.0f);
    }

    private void OncompleteCallBack()
    {
        gameObject.AddComponent<Rigidbody>();
    }
}
