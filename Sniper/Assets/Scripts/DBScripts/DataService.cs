using SQLite4Unity3d;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class DataService
{

    public SQLiteConnection _connection;
    //public SQLiteConnection _connection_local;
    public string filepath;
    public string dbPath;

    public DataService(string DatabaseName, bool replaceDBFile = false)
    {

#if UNITY_EDITOR
        dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
        filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
        //Debug.Log("PATH_E: " + filepath);

        if (!File.Exists(filepath) || replaceDBFile)
        {
            //Directory.CreateDirectory(Application.persistentDataPath);
            File.Delete(filepath);
            File.Copy(dbPath, filepath);
        }

#elif UNITY_ANDROID

        filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
        if (!File.Exists(filepath) || replaceDBFile)
        {
            Debug.Log("Database not in Persistent path: " + filepath);
            // if it doesn't, open StreamingAssets directory and load the db ->

            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.Delete(filepath);
            File.WriteAllBytes(filepath, loadDb.bytes);
        }

#elif UNITY_IOS

        filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);
        if (!File.Exists(filepath) || replaceDBFile)
        {
            Debug.Log("Database not in Persistent path: " + filepath);

            var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
            // then save to Application.persistentDataPath
            File.Delete(filepath);
            File.Copy(loadDb, filepath);
        }

#else
	    var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	    // then save to Application.persistentDataPath
	    File.Copy(loadDb, filepath);
#endif
        dbPath = filepath;
        //Debug.Log(dbPath);
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        //Debug.Log("Final PATH: " + dbPath);
    }
}