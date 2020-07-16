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
    [Header("Panel AllMyAnimalPrefab")]
    public GameObject panelAllMyAnimalPrefab;

    private void OnEnable()
    {

    }

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

    public GameObject LoadPanel(Panel panel, Transform transform = null)
    {

        GameObject gPanel = null;
        switch (panel)
        {
            case Panel.LevelComplete:
                if (transform)
                    gPanel = Instantiate(panelLevelCompletePrefab, transform);
                else
                    gPanel = Instantiate(panelLevelCompletePrefab);
                break;
            case Panel.LevelFailed:
                if (transform)
                    gPanel = Instantiate(panelLevelFailedPrefab, transform);
                else
                    gPanel = Instantiate(panelLevelFailedPrefab);
                break;
            case Panel.AllMyAnimal:
                if (transform)
                    gPanel = Instantiate(panelAllMyAnimalPrefab, transform);
                else
                    gPanel = Instantiate(panelAllMyAnimalPrefab);
                break;
            case Panel.Objectives:
                if (transform)
                    gPanel = Instantiate(panelObjectivePrefab, transform);
                else
                    gPanel = Instantiate(panelObjectivePrefab);
                break;
            default:
                break;
        }
        return gPanel;
    }
}
