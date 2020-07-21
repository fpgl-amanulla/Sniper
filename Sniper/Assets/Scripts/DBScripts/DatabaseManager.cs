using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using UnityEngine.UI;
using System;

public class DatabaseManager
{
    public Text DebugText;

    public static DatabaseManager Instance = null;

    public SQLiteConnection databaseDocument;       //Connection for scores   
    public SQLiteConnection databaseBinary;         //Connection for scores-A / scores-B from StreamingAssets
    public DataService dataService;                 //DS for scores
    public DataService dataServiceLocal;            //DS for scores-A / scores-B from StreamingAssets
    
    public static DatabaseManager sharedManager(bool replaceDBFile = false)
    {
        if(Instance == null)
        {
            Instance=DatabaseManager.Create(replaceDBFile);
        }
        return Instance;
    }

    public static DatabaseManager Create(bool replaceDBFile = false)
    {
        DatabaseManager ret = new DatabaseManager();
        if (ret != null && ret.Init(replaceDBFile))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    public bool Init(bool replaceDBFile = false)
    {
        //Debug.Log("Database MasterManager");
        if (Instance == null)
        {
            Instance = this;
        }

        dataServiceLocal = new DataService("scores-A.sqlite", replaceDBFile);
        databaseBinary = dataServiceLocal._connection;

        dataService = new DataService("scores.sqlite");
        databaseDocument = dataService._connection;

        return true;
    }
    
    private void ToConsole(string msg)
    {
        DebugText.text += System.Environment.NewLine + msg;
        Debug.Log(msg);
    }
}
