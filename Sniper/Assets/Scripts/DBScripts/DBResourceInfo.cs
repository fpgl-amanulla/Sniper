using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;

class resources
{
    [PrimaryKey, AutoIncrement]
    public int resid { get; set; }
    public string res_name { get; set; }
    public string display_name { get; set; }
    public int amount { get; set; }
    public int capacity { get; set; }
    public int bucks_value { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }

    public DBResourceInfo GetDBResourceInfo()
    {
        DBResourceInfo resourceInfo = new DBResourceInfo();
        GetDBResourceInfo(resourceInfo);
        return resourceInfo;
    }

    private void GetDBResourceInfo(DBResourceInfo resourceInfo)
    {
        resourceInfo.resid = this.resid;
        resourceInfo.res_name = this.res_name;
        resourceInfo.display_name = this.display_name;
        resourceInfo.amount = this.amount;
        resourceInfo.capacity = this.capacity;
        resourceInfo.bucks_value = this.bucks_value;
        resourceInfo.challenge_time_info = this.c1;
        resourceInfo.challenge_time_stamp = this.c2;
        resourceInfo.c3 = this.c3;
        resourceInfo.c4 = this.c4;
        resourceInfo.c5 = this.c5;
    }

    internal static resources Create(DBResourceInfo dBResourceInfo)
    {
        resources ret = new resources();
        if (ret != null && ret.Init(dBResourceInfo))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    private bool Init(DBResourceInfo dBResourceInfo)
    {
        this.resid = dBResourceInfo.resid;
        this.res_name = dBResourceInfo.res_name;
        this.display_name = dBResourceInfo.display_name;
        this.amount = dBResourceInfo.amount;
        this.capacity = dBResourceInfo.capacity;
        this.bucks_value = dBResourceInfo.bucks_value;
        this.c1 = dBResourceInfo.challenge_time_info;
        this.c2 = dBResourceInfo.challenge_time_stamp;
        this.c3 = dBResourceInfo.c3;
        this.c4 = dBResourceInfo.c4;
        this.c5 = dBResourceInfo.c5;
        return true;
    }
}

public class DBResourceInfo
{
    public int resid { get; set; }
    public string res_name { get; set; }
    public string display_name { get; set; }
    public int amount { get; set; }
    public int capacity { get; set; }
    public int bucks_value { get; set; }
    public string challenge_time_info { get; set; }
    public string challenge_time_stamp { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }

    public static List<DBResourceInfo> allResourceInfo = new List<DBResourceInfo>();


    public static DBResourceInfo Create()
    {
        DBResourceInfo ret = new DBResourceInfo();
        if (ret != null && ret.Init())
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    private bool Init()
    {
        return true;
    }

    public static DBResourceInfo Create(int _primaryKey)
    {
        DBResourceInfo ret = new DBResourceInfo();
        if (ret != null && ret.Init(_primaryKey))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    private bool Init(int primaryKey)
    {
        DatabaseManager.sharedManager().databaseDocument.Table<resources>().Where(t => t.resid == primaryKey);
        return true;
    }

    public int InsertIntoDatabase()
    {
        resources res = resources.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Insert(res);
        return res.resid;
    }

    public void InsertIntoDataBaseByPK()
    {
        resources res = resources.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Insert(res);
    }

    public void UpdateDatabase()
    {
        resources res = resources.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Update(res);
    }

    public void DeleteDatabase(int _primarykey)
    {
        DatabaseManager.sharedManager().databaseDocument.Delete<resources>(_primarykey);
    }


    public string CsvOfMyValues()
    {
        string returnStr = resid.ToString() + ","
            + res_name + "," + display_name + ","
            + amount.ToString() + "," + capacity.ToString() + ","
            + bucks_value.ToString() + "," + challenge_time_info + ","
            + challenge_time_stamp + "," + c3 + "," + c4 + "," + c5;
        return returnStr;
    }

    
}
