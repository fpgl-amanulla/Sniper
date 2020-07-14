using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PrefabsList", menuName = "PrefabsList")]
public class PrefabsList : ScriptableObject
{
    [Header("Animal Prefabs List")]
    public List<GameObject> allAnimalPrefabs = new List<GameObject>();

    [Header("Animal Canvas Prefabs")]
    public AnimalCanvas animalCanvasPrefab;

    [Header("Panel Objective Prefab")]
    public GameObject panelObjectivePrefab;
    [Header("Panel Level Complete")]
    public GameObject panelLevelCompletePrefab;
    [Header("Panel Level Failed")]
    public GameObject panelLevelFailedPrefab;

    public GameObject GetAnimalPrefeb(int productId)
    {
        for (int i = 0; i < allAnimalPrefabs.Count; i++)
        {
            if (allAnimalPrefabs[i].name == productId.ToString())
            {
                return allAnimalPrefabs[i];
            }
        }
        return allAnimalPrefabs[0];
    }
}
