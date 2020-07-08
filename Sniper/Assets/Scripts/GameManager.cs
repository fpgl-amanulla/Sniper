using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public bool isGameOver { get; set; }
    public bool activeEnemyFire { get; set; }
    private void Awake()
    {
        if (Instance == null) Instance = this;
        isGameOver = false;
        activeEnemyFire = false;
    }
}
