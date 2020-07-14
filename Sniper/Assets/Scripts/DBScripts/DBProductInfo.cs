using System;
using SQLite4Unity3d;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class product_info
{
    [PrimaryKey, AutoIncrement]
    public int productid { get; set; }
    public int catergoryid { get; set; }
    public string product_name { get; set; }
    public int level { get; set; }
    public int elementid { get; set; }
    public int rarity { get; set; }
    public int star { get; set; }
    public int tier { get; set; }
    public int pieces { get; set; }
    public string buy_price { get; set; }
    public string sell_price { get; set; }
    public string levelup_price { get; set; }
    public int sound { get; set; }
    public string description { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public DBProductInfo GetDBProductInfo()
    {
        DBProductInfo productInfo = new DBProductInfo();
        GetDBProductInfo(productInfo);

        return productInfo;
    }

    public void GetDBProductInfo(DBProductInfo productInfo)
    {
        productInfo.productid = this.productid;
        productInfo.catergoryid = this.catergoryid;
        productInfo.product_name = this.product_name;
        productInfo.level = this.level;
        productInfo.elementid = this.elementid;
        productInfo.rarity = this.rarity;
        productInfo.star = this.star;
        productInfo.tier = this.tier;
        productInfo.pieces = this.pieces;
        productInfo.buy_price = this.buy_price;
        productInfo.sell_price = this.sell_price;
        productInfo.levelup_price = this.levelup_price;
        productInfo.sound = this.sound;
        productInfo.description = this.description;
        productInfo.adult_time = this.c1;
        productInfo.fusionCreation = this.c2;
        productInfo.collect_coins_amount = this.c3;
        productInfo.collect_coins_time = this.c4;
        productInfo.health = this.c5;
        productInfo.c6 = this.c6;
        productInfo.c7 = this.c7;
        productInfo.c8 = this.c8;
        productInfo.c9 = this.c9;
    }
}

public class DBProductInfo
{
    [PrimaryKey, AutoIncrement]
    public int productid { get; set; }
    public int catergoryid { get; set; }
    public string product_name { get; set; }
    public int level { get; set; }
    public int elementid { get; set; }
    public int rarity { get; set; }
    public int star { get; set; }
    public int tier { get; set; }
    public int pieces { get; set; }
    public string buy_price { get; set; }
    public string sell_price { get; set; }
    public string levelup_price { get; set; }
    public int sound { get; set; }
    public string description { get; set; }
    public string adult_time { get; set; }
    public string fusionCreation { get; set; }
    public string collect_coins_amount { get; set; }
    public string collect_coins_time { get; set; }
    public string health { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    private static List<DBProductInfo> allProductInfo = new List<DBProductInfo>();

    public static DBProductInfo Create()
    {
        DBProductInfo ret = new DBProductInfo();
        if (ret != null && ret.Init())
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    public bool Init()
    {
        SetDefaultValue();
        return true;
    }

    public static DBProductInfo Create(int primaryKey)
    {
        DBProductInfo ret = new DBProductInfo();
        if (ret != null && ret.Init(primaryKey))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    public bool Init(int primaryKey)
    {



        return true;
    }

    private void SetDefaultValue()
    {
        productid = 0;
        catergoryid = 0;
        product_name = "0";
        level = 0;
        elementid = 0;
        rarity = 0;
        star = 0;
        tier = 0;
        pieces = 0;
        buy_price = "0";
        sell_price = "0";
        levelup_price = "0";
        sound = 0;
        description = "0";
        adult_time = "0";
        fusionCreation = "0";
        collect_coins_amount = "0";
        collect_coins_time = "0";
        health = "0";
        c6 = "0";
        c7 = "0";
        c8 = "0";
        c9 = "0";
    }

    public void showProductInfo()
    {
        Debug.Log("-------------------------");
        Debug.Log("productid " + productid);
        Debug.Log("catergoryid " + catergoryid);
        Debug.Log("product_name " + product_name);
        Debug.Log("level " + level);
        Debug.Log("elementid " + elementid);
        Debug.Log("rarity " + rarity);
        Debug.Log("star " + star);
        Debug.Log("tier " + tier);
        Debug.Log("pieces " + pieces);
        Debug.Log("buy_price " + buy_price);
        Debug.Log("sell_price " + sell_price);
        Debug.Log("levelup_price " + levelup_price);
        Debug.Log("sound " + sound);
        Debug.Log("description " + description);
        Debug.Log("adult_time " + adult_time);
        Debug.Log("fusionCreation " + fusionCreation);
        Debug.Log("collect_coins_amount " + collect_coins_amount);
        Debug.Log("collect_coins_time " + collect_coins_time);
        Debug.Log("c5 " + health);
        Debug.Log("c6 " + c6);
        Debug.Log("c7 " + c7);
        Debug.Log("c8 " + c8);
        Debug.Log("c9 " + c9);
    }

    public static List<DBProductInfo> GetAllProductInfo()
    {
        if (allProductInfo.Count == 0)
        {
            IEnumerable<product_info> ieumAllProductInfo = (DatabaseManager.sharedManager().databaseBinary.Table<product_info>()).Where(t => t.level < 18).OrderBy(t => t.level);

            foreach (var product_info in ieumAllProductInfo)
            {
                DBProductInfo productInfo = product_info.GetDBProductInfo();
                allProductInfo.Add(productInfo);
            }
        }

        return allProductInfo;
    }

    public static DBProductInfo GetProductInfo(int _productId)
    {
        if (allProductInfo.Count == 0)
            allProductInfo = GetAllProductInfo();

        for (int i = 0; i < allProductInfo.Count; i++)
        {
            DBProductInfo productInfo = allProductInfo[i];
            if (productInfo.productid == _productId)
            {
                return productInfo;
            }
        }

        return DBProductInfo.GetProductInfo(2808);
    }

    public static List<DBProductInfo> GetElementaLALLProducts(int _elementId, int _level = 0)
    {
        List<DBProductInfo> products = new List<DBProductInfo>();
        if (allProductInfo.Count == 0)
        {
            allProductInfo = GetAllProductInfo();
        }

        foreach (var t in allProductInfo)
        {
            if (t.elementid == _elementId && t.level <= _level)
            {
                products.Add(t);
            }
        }

        return products;
    }

    public static List<DBProductInfo> GetStarAllProducts(int _star, int _level)
    {
        List<DBProductInfo> products = new List<DBProductInfo>();

        if (allProductInfo.Count == 0)
        {
            allProductInfo = GetAllProductInfo();
        }
        foreach (var t in allProductInfo)
        {
            if (t.star == _star && t.level <= _level)
            {
                products.Add(t);
            }
        }

        return products;
    }

    public static List<DBProductInfo> GetElementalWithStarALLProducts(int _elementId, int _star, int _level)
    {
        List<DBProductInfo> products = new List<DBProductInfo>();
        if (allProductInfo.Count == 0)
        {
            allProductInfo = GetAllProductInfo();
        }
        foreach (var t in allProductInfo)
        {
            if (t.star == _star && t.elementid == _elementId && t.level <= _level)
            {
                products.Add(t);
            }
        }

        return products;
    }


    public static List<DBProductInfo> GetRarityAllProducts(int _rarity)
    {
        List<DBProductInfo> products = new List<DBProductInfo>();

        if (allProductInfo.Count == 0)
        {
            allProductInfo = GetAllProductInfo();
        }
        foreach (var t in allProductInfo)
        {
            if (t.rarity == _rarity)
            {
                products.Add(t);
            }
        }

        return products;
    }
}
