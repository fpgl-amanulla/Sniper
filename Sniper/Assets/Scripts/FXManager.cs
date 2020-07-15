using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public static FXManager Instance = null;

    public GameObject fireEffect;
    public GameObject deathEffect;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
