using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    void TakeDamage(float damageAmount);
}

public enum Panel
{
    LevelComplete = 101,
    LevelFailed = 102,
    AllMyAnimal = 103,
    Objectives = 104
}
public static class Constants
{
    public static string animal = "Animal/";
    public static string animalIcon = "Animal/Icon/";
}
