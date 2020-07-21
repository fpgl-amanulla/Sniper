using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance = null;

    public NavMeshSurface navMeshSurface;
    public List<Transform> waypoints = new List<Transform>();
    public GameObject player;
    public GameObject mainCamera;
    public GameObject playerCamera;

    [Header("Prefabs List")]
    public PrefabsList prefabsList;
    [Header("FX MasterManager")]
    public FXManager fxManager;
    [Header("Player UI")]
    public PanelGame panelGame;
    [Header("Managers")]
    public GameManager gameManager;
    public UIManager uiManager;
    public LevelManager levelManager;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        navMeshSurface.BuildNavMesh();

        HideWaypoint();
    }

    private void HideWaypoint()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            waypoints[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
