using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject player;
    public GameObject bullet;
    public Transform firePoint;
    public float fireDelay = 5f;
    private float delayTime = 0f;

    private List<GameObject> playerHitPoints = new List<GameObject>();

    private bool isDied = false;
    private void Die()
    {
        isDied = true;
        Destroy(gameObject, 3.0f);
    }

    private void Start()
    {
        playerHitPoints = ReferenceManager.Instance.hitPoints;
        player = ReferenceManager.Instance.player;
    }

    private void Update()
    {
        delayTime += Time.deltaTime;
        if (delayTime >= fireDelay && isDied == false && !GameManager.Instance.isGameOver)
        {
            delayTime = 0;
            transform.LookAt(player.transform);
            fireDelay = UnityEngine.Random.Range(2, 4);
            int point = UnityEngine.Random.Range(0, playerHitPoints.Count);

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
}
