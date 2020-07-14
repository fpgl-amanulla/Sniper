using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Level
{
    public int levelNo;
    public int animalToHunt;
    public int rewardAmount;
    public int numberOfBullet;
    public int selectedAnimalId;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int levelToLoad = 0;
    public GameObject environment;
    public List<Level> levelList = new List<Level>();
    public GameObject animalContainer;
    public List<GameObject> allAnimal = new List<GameObject>();
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {

    }

    public void LoadLevel()
    {
        PrefabsList prefabsList = ReferenceManager.Instance.prefabsList;

        Level levelInfo = GetCurrentLevelInfo();
        DBProductInfo animalInfo = DBProductInfo.GetProductInfo(levelInfo.selectedAnimalId);

        for (int i = 0; i < levelInfo.animalToHunt + 2; i++)
        {
            GameObject newAnimal = Instantiate(prefabsList.GetAnimalPrefeb(levelInfo.selectedAnimalId));
            newAnimal.AddComponent<Animal>();
            Animal animal = newAnimal.GetComponent<Animal>();
            animal.productId = animalInfo.productid;
            animal.productName = animalInfo.product_name;
            animal.health = float.Parse(animalInfo.health);

            Vector3 position = new Vector3(Random.Range(-50, 50), 0, Random.Range(-30, 50));
            newAnimal.transform.SetParent(animalContainer.transform);
            newAnimal.transform.localPosition = position;

            allAnimal.Add(newAnimal);
        }
    }


    public void ResetLevel()
    {
        for (int i = 0; i < allAnimal.Count; i++)
        {
            Destroy(allAnimal[i]);
        }
    }
    public Level GetCurrentLevelInfo()
    {
        DBUserInfo userInfo = DBUserInfo.Create(1);
        int currntLevel = userInfo.level;
        return levelList[currntLevel];
    }
}
