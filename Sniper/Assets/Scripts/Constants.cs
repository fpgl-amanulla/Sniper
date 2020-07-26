﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITakeDamage
{
    void TakeDamage(float damageAmount);
}

public enum PrefabName
{
    Start,
    Map,
    LevelComplete,
    LevelFailed,
    AllMyAnimal,
    Objectives,
    AnimalCanvas
}
public static class Constants
{
    public static string animal = "Animal/";
    public static string animalIcon = "Animal/Icon/";
}
