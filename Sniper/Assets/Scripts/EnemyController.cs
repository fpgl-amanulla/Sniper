using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemtType
{
    staticEnemy,
    dynamicEnemy,
    killerEnemy,
    briefCaseEnemy,
    bossEnemy
}

public class EnemyController : Enemy
{
    public override void TakeDamage(int damageAmount)
    {
        //base.TakeDamage(damageAmount);
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    public EnemtType enemtType;
    private GameObject player;
    public GameObject victim = null;
    private NavMeshAgent agent;
    public GameObject bullet;
    public Transform firePoint;
    private float fireDelay = 1f;
    private float delayTime = 0f;

    private List<GameObject> playerHitPoints = new List<GameObject>();
    private List<GameObject> enemyHidePoints = new List<GameObject>();

    private bool isDied = false;
    private bool isHide = false;
    private bool shootVictim = true;
    public float waitTime;
    private Transform briefCaseEnemyEscapePoint;

    private void Die()
    {
        isDied = true;
        Destroy(gameObject, 3.0f);
    }

    private void Start()
    {
        if (GetComponent<NavMeshAgent>() == null)
            gameObject.AddComponent<NavMeshAgent>();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.baseOffset = 1.0f;
        agent.height = 2.0f;

        playerHitPoints = ReferenceManager.Instance.hitPoints;
        player = ReferenceManager.Instance.player;
        enemyHidePoints = ReferenceManager.Instance.enemyHidePoints;

        if (ReferenceManager.Instance.escapePoint != null)
        {
            briefCaseEnemyEscapePoint = ReferenceManager.Instance.escapePoint;
        }
    }

    private void Update()
    {

        if (isDied == false && !GameManager.Instance.isGameOver && GameManager.Instance.activeEnemyFire)
        {

            switch (enemtType)
            {
                case EnemtType.dynamicEnemy:
                    if (!isHide)
                    {
                        isHide = true;
                        Vector3 nearestPosition = FindNearestHidePoint();
                        agent.SetDestination(nearestPosition);
                    }

                    if (agent.remainingDistance < .5f)
                    {
                        StartShooting();
                    }
                    break;
                case EnemtType.staticEnemy:
                    StartShooting();
                    break;
                case EnemtType.killerEnemy:
                    if (shootVictim)
                    {
                        shootVictim = false;
                        transform.LookAt(victim.transform);
                        StartCoroutine(Kill(waitTime));
                    }
                    break;
                case EnemtType.briefCaseEnemy:
                    {
                        agent.speed = 5.0f;
                        agent.SetDestination(briefCaseEnemyEscapePoint.position);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    private IEnumerator Kill(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        newBullet.transform.DOMove(victim.transform.position, .5f);
        yield return new WaitForSeconds(.5f);
        victim.gameObject.AddComponent(typeof(Rigidbody));
        victim.GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Impulse);
        Destroy(newBullet, .45f);
    }

    private void StartShooting()
    {
        delayTime += Time.deltaTime;
        if (delayTime >= fireDelay)
        {
            delayTime = 0;
            fireDelay = UnityEngine.Random.Range(2, 4);
            int point = UnityEngine.Random.Range(0, playerHitPoints.Count);
            transform.LookAt(playerHitPoints[point].transform);

            if (playerHitPoints[point].gameObject.name == "Point")
            {
                PlayerController _player = player.GetComponent<PlayerController>();
                _player.TakeDamage(1);
            }
            GameObject newBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
            newBullet.transform.DOMove(playerHitPoints[point].transform.position, .5f);
            Destroy(newBullet, 1.0f);
        }
    }

    private Vector3 FindNearestHidePoint()
    {
        float minDistance = Mathf.Infinity;
        Vector3 nearestPoint = Vector3.zero;
        for (int i = 0; i < enemyHidePoints.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, enemyHidePoints[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPoint = enemyHidePoints[i].transform.position;
            }
        }

        return nearestPoint;
    }
}
