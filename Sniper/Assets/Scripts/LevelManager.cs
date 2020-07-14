using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

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
        LoadLevel();
    }

    public void LoadLevel()
    {
        int currentLevel;
        DBUserInfo userInfo = DBUserInfo.Create(1);
        if (userInfo.UDID != "user")
        {
            SetDefaults(userInfo);
            userInfo.InsertIntoDatabase();
        }
        currentLevel = userInfo.level;

        Debug.Log(currentLevel);
        PrefabsList prefabsList = ReferenceManager.Instance.prefabsList;

        Level levelInfo = GetLevelInfo(currentLevel);
        DBProductInfo animalInfo = DBProductInfo.GetProductInfo(levelInfo.selectedAnimalId);
        Debug.Log(animalInfo.health);
        for (int i = 0; i < levelInfo.animalToHunt + 2; i++)
        {
            GameObject newAnimal = Instantiate(prefabsList.GetAnimalPrefeb(levelInfo.selectedAnimalId));
            newAnimal.AddComponent<Animal>();
            Animal animal = newAnimal.GetComponent<Animal>();
            animal.productId = animalInfo.productid;
            animal.productName = animalInfo.product_name;
            animal.health = float.Parse(animalInfo.health);

            newAnimal.transform.SetParent(animalContainer.transform);
        }
    }

    private void SetDefaults(DBUserInfo userInfo)
    {
        userInfo.UDID = "user";
        userInfo.coins = 0;
        userInfo.bucks = 0;
        userInfo.experience = 0;
        userInfo.active_screenid = 0;
        userInfo.level = 0;
        userInfo.last_visited = 0;
    }

    public void ResetLevel()
    {
        for (int i = 0; i < allAnimal.Count; i++)
        {
            Destroy(allAnimal[i]);
        }
    }
    public Level GetLevelInfo(int levelNo) => levelList[levelNo];
}
