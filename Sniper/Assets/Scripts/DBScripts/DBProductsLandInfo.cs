using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;

public class products_land
{
    [PrimaryKey,AutoIncrement]
    public int products_landid { get; set; }
    public int elementid { get; set; }
    public string land_name { get; set; }
    public int build_time { get; set; }
    public int level_id { get; set; }
    public int buy_coins { get; set; }
    public int buy_bucks { get; set; }
    public int sell_coins { get; set; }
    public int sell_bucks { get; set; }
    public int tilex { get; set; }
    public int tiley { get; set; }
    public int product_limit { get; set; }
    public int experience { get; set; }
    public int c1 { get; set; }
    public int c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public DBProductsLandInfo GetProductsLandInfo()
    {
        DBProductsLandInfo productLandInfo = new DBProductsLandInfo();
        GetDBProductInfo(productLandInfo);

        return productLandInfo;
    }

    public void GetDBProductInfo(DBProductsLandInfo productLandInfo)
    {
        productLandInfo.products_landid = this.products_landid;
        productLandInfo.elementid = this.elementid;
        productLandInfo.land_name = this.land_name;
        productLandInfo.build_time = this.build_time;
        productLandInfo.level_id = this.level_id;
        productLandInfo.buy_coins = this.buy_coins;
        productLandInfo.buy_bucks = this.buy_bucks;
        productLandInfo.sell_coins = this.sell_coins;
        productLandInfo.product_limit = this.product_limit;
        productLandInfo.sell_bucks = this.sell_bucks;
        productLandInfo.tilex = this.tilex;
        productLandInfo.tiley = this.tiley;
        productLandInfo.experience = this.experience;
        productLandInfo.capacity = this.c1;
        productLandInfo.product_land_update = this.c2;
        productLandInfo.c3 = this.c3;
        productLandInfo.c4 = this.c4;
        productLandInfo.c5 = this.c5;
        productLandInfo.c6 = this.c6;
        productLandInfo.c7 = this.c7;
        productLandInfo.c8 = this.c8;
        productLandInfo.c9 = this.c9;
    }
}

public class DBProductsLandInfo
{
    public int products_landid { get; set; }
    public int elementid { get; set; }
    public string land_name { get; set; }
    public int build_time { get; set; }
    public int level_id { get; set; }
    public int buy_coins { get; set; }
    public int buy_bucks { get; set; }
    public int sell_coins { get; set; }
    public int sell_bucks { get; set; }
    public int tilex { get; set; }
    public int tiley { get; set; }
    public int product_limit { get; set; }
    public int experience { get; set; }
    public int capacity { get; set; }
    public int product_land_update { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public static List<DBProductsLandInfo> allProductsLandInfo= new List<DBProductsLandInfo>();

    public static DBProductsLandInfo Create()
    {
        DBProductsLandInfo ret = new DBProductsLandInfo();
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
    public static DBProductsLandInfo Create(int primarykey)
    {
        DBProductsLandInfo ret = new DBProductsLandInfo();
        if (ret != null && ret.Init(primarykey))
        {
            return ret;
        }
        else
            return null;
    }

    private bool Init(int primarykey)
    {
        DatabaseManager.sharedManager().databaseBinary.Table<products_land>().Where(t => t.products_landid == primarykey);
        IEnumerable<products_land> ieumAllMyLand = DatabaseManager.sharedManager().databaseDocument.Table<products_land>().Where(t => t.products_landid == primarykey);
        foreach (var t in ieumAllMyLand)
        {
            products_land _land_info = t;
            _land_info.GetDBProductInfo(this);
        }
        return true;
    }

    public static List<DBProductsLandInfo> AllProductsLand()
    {
        if(allProductsLandInfo.Count==0)
        {
            IEnumerable<products_land> ieumAllProductsLandInfo =
                DatabaseManager.sharedManager().databaseDocument.Table<products_land>().OrderBy(t => t.level_id);
            foreach (var item in ieumAllProductsLandInfo)
            {
                DBProductsLandInfo getEntry = item.GetProductsLandInfo();
                allProductsLandInfo.Add(getEntry);
            }
        }
        return allProductsLandInfo;
    }

    public List<DBProductsLandInfo> allProductsLandWithElement(int _elementId)
    {
        List<DBProductsLandInfo> elementalProductLand = new List<DBProductsLandInfo>();
        if (allProductsLandInfo.Count == 0) allProductsLandInfo = AllProductsLand();
        for (int i = 0; i < allProductsLandInfo.Count; i++)
        {
            DBProductsLandInfo productsLandInfo = allProductsLandInfo[i];
            if(productsLandInfo.elementid == _elementId)
            {
                elementalProductLand.Add(productsLandInfo);
            }
        }
        return elementalProductLand;
    }
}
