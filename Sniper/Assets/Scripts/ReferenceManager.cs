using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance = null;

    public NavMeshSurface navMeshSurface;
    public List<GameObject> hitPoints = new List<GameObject>();
    public List<GameObject> enemyHidePoints = new List<GameObject>();
    public GameObject player;
    public PlayerUI playerUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        navMeshSurface.BuildNavMesh();

        VanishEnemyPoint();
    }

    private void VanishEnemyPoint()
    {
        for (int i = 0; i < enemyHidePoints.Count; i++)
        {
            enemyHidePoints[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
