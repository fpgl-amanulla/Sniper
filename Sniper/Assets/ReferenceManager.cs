using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance = null;

    public List<GameObject> hitPoints = new List<GameObject>();
    public GameObject player;
    public PlayerUI playerUI;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
