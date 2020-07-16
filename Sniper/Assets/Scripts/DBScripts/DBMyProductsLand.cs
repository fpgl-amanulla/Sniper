using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.Linq;

public class my_products_land
{
    [PrimaryKey,AutoIncrement]
    public int my_product_landid { get; set; }
    public int product_landid { get; set; }
    public string my_productid { get; set; }
    public int screenid { get; set; }
    public int timestamp { get; set; }
    public int tilex { get; set; }
    public int tiley { get; set; }
    public int land_level { get; set; }
    public string last_time_coin_collection { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }

    public static my_products_land Create(DBMyProductsLand myProductsLand)
    {
        my_products_land ret = new my_products_land();
        if(ret!=null && ret.Init(myProductsLand))
        {
            return ret;
        }
        return null;
    }

    private bool Init(DBMyProductsLand myProductsLand)
    {
        this.my_product_landid = myProductsLand.my_product_landid;
        this.product_landid = myProductsLand.product_landid;
        this.my_productid = myProductsLand.my_productid;
        this.screenid = myProductsLand.screenid;
        this.timestamp = myProductsLand.timestamp;
        this.tilex = myProductsLand.tilex;
        this.tiley = myProductsLand.tiley;
        this.land_level = myProductsLand.land_level;
        this.last_time_coin_collection = myProductsLand.last_time_coin_collection;
        this.c2 = myProductsLand.product_limit;
        this.c3 = myProductsLand.c3;
        this.c4 = myProductsLand.c4;
        this.c5 = myProductsLand.c5;
        this.c6 = myProductsLand.c6;
        return true;
    }

    public void GetProductLand(DBMyProductsLand myProductsLand)
    {
        myProductsLand.my_product_landid = this.my_product_landid;
        myProductsLand.product_landid = this.product_landid;
        myProductsLand.my_productid = this.my_productid;
        myProductsLand.screenid = this.screenid;
        myProductsLand.timestamp = this.timestamp;
        myProductsLand.tilex = this.tilex;
        myProductsLand.tiley = this.tiley;
        myProductsLand.land_level = this.land_level;
        myProductsLand.last_time_coin_collection = this.last_time_coin_collection;
        myProductsLand.product_limit = this.c2;
        myProductsLand.c3 = this.c3;
        myProductsLand.c4 = this.c4;
        myProductsLand.c5 = this.c5;
        myProductsLand.c6 = this.c6;
    }
}
public class DBMyProductsLand
{
    public int my_product_landid { get; set; }
    public int product_landid { get; set; }
    public string my_productid { get; set; }
    public int screenid { get; set; }
    public int timestamp { get; set; }
    public int tilex { get; set; }
    public int tiley { get; set; }
    public int land_level { get; set; }
    public string last_time_coin_collection { get; set; }
    public string product_limit { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }


    public static  DBMyProductsLand Create()
    {
        DBMyProductsLand ret = new DBMyProductsLand();
        if (ret != null && ret.Init())
        {
            return ret;
        }
        else
            return null;
    }

    private bool Init()
    {
        return true;
    }

    public static DBMyProductsLand Create(int primarykey)
    {
        DBMyProductsLand ret = new DBMyProductsLand();
        if (ret != null && ret.Init(primarykey))
        {
            return ret;
        }
        else
            return null;
    }

    private bool Init(int primarykey)
    {
        DatabaseManager.sharedManager().databaseBinary.Table<my_products_land>().Where(t => t.my_product_landid == primarykey);
        IEnumerable<my_products_land> ieumAllMyLand = DatabaseManager.sharedManager().databaseDocument.Table<my_products_land>().Where(t => t.my_product_landid == primarykey);
        foreach (var t in ieumAllMyLand)
        {
            my_products_land _productLand = t;
            _productLand.GetProductLand(this);
        }
        return true;
    }

    public void InsertIntoDatabase()
    {
        my_products_land myProductsLand = my_products_land.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Insert(myProductsLand);
    }
    
    public void InsertIntoDatabaseWithPk()
    {
        var q = string.Format("INSERT INTO \"{0}\" VALUES( \"{1}\", \"{2}\", \"{3}\", \"{4}\", \"{5}\", \"{6}\", \"{7}\", \"{8}\", \"{9}\", \"{10}\", \"{11}\", \"{12}\", \"{13}\", \"{14}\")", "my_products_land", this.my_product_landid, this.product_landid, this.my_productid, this.screenid, this.timestamp, this.tilex, this.tiley, this.land_level, this.last_time_coin_collection, this.product_limit, this.c3, this.c4, this.c5, this.c6);

        DatabaseManager.sharedManager().databaseDocument.Execute(q);
    }

    public void UpdateDatabase()
    {
        my_products_land myProductsLand = my_products_land.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Update(myProductsLand);
    }
    public static void DeleteDatabase(int primarykey)
    {
        DatabaseManager.sharedManager().databaseDocument.Delete<my_products_land>(primarykey);
    }

    public static void DeleteAllDatabase()
    {
        DatabaseManager.sharedManager().databaseDocument.DeleteAll<my_products_land>();
    }

    public static List<DBMyProductsLand> AllActiveProductLands()
    {
        List<DBMyProductsLand> allProductLands = new List<DBMyProductsLand>();

        List<my_products_land> str_productland = DatabaseManager.sharedManager().databaseDocument.Query<my_products_land>("SELECT my_product_landid FROM my_products_land").ToList();
        foreach (var item in str_productland)
        {
            //Debug.Log(item.my_product_landid);
            int primaryKeyProductLand = item.my_product_landid;
            DBMyProductsLand aProductLand=DBMyProductsLand.Create(primaryKeyProductLand);
            allProductLands.Add(aProductLand);
        }
        return allProductLands;
    }

    public List<DBProductsLandInfo> allActiveProductLandsWithElementId(int _elementId)
    {
        List<DBProductsLandInfo> allProductLands = new List<DBProductsLandInfo>();

        List<my_products_land> str_productland = DatabaseManager.sharedManager().databaseDocument.Query<my_products_land>("SELECT my_product_landid FROM my_products_land").ToList();
        foreach (var item in str_productland)
        {
            int primaryKeyProductLand = item.my_product_landid;

            DBMyProductsLand aProductLand = DBMyProductsLand.Create(primaryKeyProductLand);
            DBProductsLandInfo productLandnfo = DBProductsLandInfo.Create(aProductLand.product_landid);
            if (productLandnfo.elementid == _elementId)
                allProductLands.Add(productLandnfo);
        }
        return allProductLands;
    }

    static string strDivider = "_";

    public string GetMyProductLandInfoInString()
    {
        string strReturn = my_product_landid + strDivider
                           + product_landid + strDivider
                           + my_productid + strDivider
                           + screenid + strDivider
                           + timestamp + strDivider
                           + tilex + strDivider
                           + tiley + strDivider
                           + land_level + strDivider
                           + last_time_coin_collection + strDivider
                           + product_limit + strDivider
                           + c3 + strDivider
                           + c4 + strDivider
                           + c5 + strDivider
                           + c6;

        return strReturn;
    }

    public static void SetMyPrdoctLandInfoInDatabase(string strReturn)
    {
        string[] arrAllData = AppDelegate.ComponentsSeparatedByString(strReturn, strDivider);

        int index = 0;

        DBMyProductsLand myProductsLand = DBMyProductsLand.Create();

        myProductsLand.my_product_landid = int.Parse(arrAllData[index++]);
        myProductsLand.product_landid = int.Parse(arrAllData[index++]);
        myProductsLand.my_productid = arrAllData[index++];
        myProductsLand.screenid = int.Parse(arrAllData[index++]);
        myProductsLand.timestamp = int.Parse(arrAllData[index++]);
        myProductsLand.tilex = int.Parse(arrAllData[index++]);
        myProductsLand.tiley = int.Parse(arrAllData[index++]);
        myProductsLand.land_level = int.Parse(arrAllData[index++]);
        myProductsLand.last_time_coin_collection = /*int.Parse(arrAllData[index++]);*/arrAllData[index++];
        myProductsLand.product_limit = arrAllData[index++];
        myProductsLand.c3 = arrAllData[index++];
        myProductsLand.c4 = arrAllData[index++];
        myProductsLand.c5 = arrAllData[index++];
        myProductsLand.c6 = arrAllData[index++];

        myProductsLand.InsertIntoDatabaseWithPk();
        //myProductsLand.showDBMyProductLand();
    }
}
