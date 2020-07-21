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
    public int levelToLoad = 0;
    public GameObject environment;
    public List<Level> levelList = new List<Level>();
    public GameObject animalContainer;
    public List<GameObject> allAnimal = new List<GameObject>();

    private void Start()
    {

    }

    public void LoadLevel()
    {
        PrefabsList prefabsList = MasterManager.Instance.prefabsList;

        Level levelInfo = GetCurrentLevelInfo();
        DBProductInfo animalInfo = DBProductInfo.GetProductInfo(levelInfo.selectedAnimalId);

        for (int i = 0; i < levelInfo.animalToHunt + 1; i++)
        {
            GameObject newAnimal = Instantiate(prefabsList.GetAnimalPrefeb(levelInfo.selectedAnimalId));
            InitAnimal(animalInfo, newAnimal);
        }

        List<GameObject> randomAnimal = GetRandomAnimal(animalInfo.productid);
        for (int i = 0; i < Random.Range(2, 4); i++)
        {
            int randomIndex = Random.Range(0, randomAnimal.Count);
            GameObject newAnimal = Instantiate(randomAnimal[randomIndex]);
            int productId = Int32.Parse(randomAnimal[randomIndex].name);
            DBProductInfo randomAnimalInfo = DBProductInfo.GetProductInfo(productId);
            InitAnimal(randomAnimalInfo, newAnimal);


        }
    }

    private void InitAnimal(DBProductInfo animalInfo, GameObject newAnimal)
    {
        newAnimal.AddComponent<Animal>();
        string strMatPath = "Animal/" + animalInfo.productid + "/" + animalInfo.productid + "b";
        Material mat = Resources.Load<Material>(strMatPath);
        newAnimal.GetComponentInChildren<SkinnedMeshRenderer>().material = mat;
        Vector3 position = new Vector3(Random.Range(-50, 50), Random.Range(0, 10), Random.Range(-30, 50));
        newAnimal.transform.SetParent(animalContainer.transform);
        newAnimal.transform.localPosition = position;

        AnimalData animal = newAnimal.GetComponent<AnimalData>();
        animal.productId = animalInfo.productid;
        animal.productName = animalInfo.product_name;
        animal.health = float.Parse(animalInfo.health);
        allAnimal.Add(newAnimal);
    }

    public List<GameObject> GetRandomAnimal(int selectedId)
    {
        PrefabsList prefabsList = MasterManager.Instance.prefabsList;
        List<GameObject> allAnimal = prefabsList.allAnimalPrefabs;
        List<GameObject> animalList = new List<GameObject>();
        for (int i = 0; i < allAnimal.Count; i++)
        {
            if (allAnimal[i].name != selectedId.ToString())
            {
                animalList.Add(allAnimal[i]);
            }
        }
        return allAnimal;
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
