using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator animator;
    public float movementSpeed;
    public float rotationSpeed;

    public float stopDistance;
    public Vector3 destination;
    public bool reachedDestination;

    public List<Transform> waypoints = new List<Transform>();

    private void Start()
    {
        waypoints = ReferenceManager.Instance.waypoints;
        animator = GetComponent<Animator>();
        SetDestination();
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
        destination = waypoints[Random.Range(0, waypoints.Count)].position;
    }

}
