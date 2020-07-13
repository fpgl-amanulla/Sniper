using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Animal : AnimalData, ITakeDamage
{
    private Animator animator;
    public float movementSpeed;
    public float rotationSpeed;

    public float stopDistance;
    public Vector3 destination;
    public bool reachedDestination;

    public List<Transform> waypoints = new List<Transform>();

    private void Start()
    {
        InitAnimalData();
        waypoints = ReferenceManager.Instance.waypoints;
        animator = GetComponent<Animator>();
        SetDestination();
    }

    private void InitAnimalData()
    {
        movementSpeed = Random.Range(4, 7);
        rotationSpeed = Random.Range(100, 120);
    }

    private void Update()
    {
        Vector3 desDirection = destination - transform.position;
        desDirection.y = 0;

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
        destination = waypoints[UnityEngine.Random.Range(0, waypoints.Count)].position;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject, 3.0f);
    }
}
