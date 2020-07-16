using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    void TakeDamage(float damageAmount);
}

public enum Panel
{
    Start,
    Map,
    LevelComplete,
    LevelFailed,
    AllMyAnimal,
    Objectives,
}
public static class Constants
{
    public static string animal = "Animal/";
    public static string animalIcon = "Animal/Icon/";
}
