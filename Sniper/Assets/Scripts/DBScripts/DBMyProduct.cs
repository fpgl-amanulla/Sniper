using System;
using SQLite4Unity3d;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class my_product
{
    [PrimaryKey, AutoIncrement]
    public int my_productid { get; set; }
    public int productid { get; set; }
    public int screenid { get; set; }
    public string my_name { get; set; }
    public int time_stamp { get; set; }
    public int my_level { get; set; }
    public int my_tier { get; set; }
    public int my_pieces { get; set; }
    public string cost { get; set; }
    public string c1 { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public static my_product Create(DBMyProduct myProduct)
    {
        my_product ret = new my_product();
        if (ret != null && ret.Init(myProduct))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    private bool Init(DBMyProduct myProduct)
    {
        this.my_productid = myProduct.my_productid;
        this.productid = myProduct.productid;
        this.screenid = myProduct.screenid;
        this.my_name = myProduct.my_name;
        this.time_stamp = myProduct.time_stamp;
        this.my_level = myProduct.my_level;
        this.my_tier = myProduct.my_tier;
        this.my_pieces = myProduct.my_pieces;
        this.cost = myProduct.cost;
        this.c1 = myProduct.slot_info;
        this.c2 = myProduct.c2;
        this.c3 = myProduct.c3;
        this.c4 = myProduct.c4;
        this.c5 = myProduct.c5;
        this.c6 = myProduct.c6;
        this.c7 = myProduct.c7;
        this.c8 = myProduct.c8;
        this.c9 = myProduct.c9;

        return true;
    }

    public void GetMyProduct(DBMyProduct myProduct)
    {
        myProduct.my_productid = this.my_productid;
        myProduct.productid = this.productid;
        myProduct.screenid = this.screenid;
        myProduct.my_name = this.my_name;
        myProduct.time_stamp = this.time_stamp;
        myProduct.my_level = this.my_level;
        myProduct.my_tier = this.my_tier;
        myProduct.my_pieces = this.my_pieces;
        myProduct.cost = this.cost;
        myProduct.slot_info = this.c1;
        myProduct.c2 = this.c2;
        myProduct.c3 = this.c3;
        myProduct.c4 = this.c4;
        myProduct.c5 = this.c5;
        myProduct.c6 = this.c6;
        myProduct.c7 = this.c7;
        myProduct.c8 = this.c8;
        myProduct.c9 = this.c9;
    }
}

public class DBMyProduct
{
    public int my_productid { get; set; }
    public int productid { get; set; }
    public int screenid { get; set; }
    public string my_name { get; set; }
    public int time_stamp { get; set; }
    public int my_level { get; set; }
    public int my_tier { get; set; }
    public int my_pieces { get; set; }
    public string cost { get; set; }
    public string slot_info { get; set; }
    public string c2 { get; set; }
    public string c3 { get; set; }
    public string c4 { get; set; }
    public string c5 { get; set; }
    public string c6 { get; set; }
    public string c7 { get; set; }
    public string c8 { get; set; }
    public string c9 { get; set; }

    public static DBMyProduct Create()
    {
        DBMyProduct ret = new DBMyProduct();
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
        SetDefaultValue();
        return true;
    }

    public static DBMyProduct Create(int primaryKey)
    {
        DBMyProduct ret = new DBMyProduct();
        if (ret != null && ret.Init(primaryKey))
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
        IEnumerable<my_product> ieumAllMyProduct = DatabaseManager.sharedManager().databaseDocument.Table<my_product>().Where(t => t.productid == primaryKey);

        foreach (var t in ieumAllMyProduct)
        {
            my_product _my_Product = t;
            _my_Product.GetMyProduct(this);
        }

        return true;
    }

    private void SetDefaultValue()
    {
        my_productid = 0;
        productid = 0;
        screenid = 0;
        my_name = "0";
        time_stamp = 0;
        my_level = 0;
        my_tier = 0;
        my_pieces = 0;
        cost = "0";
        slot_info = "0";
        c2 = "0";
        c3 = "0";
        c4 = "0";
        c5 = "0";
        c6 = "0";
        c7 = "0";
        c8 = "0";
        c9 = "0";
    }

    public void showDBMyProduct()
    {
        Debug.Log("-------------------------");
        Debug.Log("my_productid " + my_productid);
        Debug.Log("productid " + productid);
        Debug.Log("screenid " + screenid);
        Debug.Log("my_name " + my_name);
        Debug.Log("time_stamp " + time_stamp);
        Debug.Log("my_level " + my_level);
        Debug.Log("my_tier " + my_tier);
        Debug.Log("my_pieces " + my_pieces);
        Debug.Log("cost " + cost);
        Debug.Log("slot_info " + slot_info);
        Debug.Log("c2 " + c2);
        Debug.Log("c3 " + c3);
        Debug.Log("c4 " + c4);
        Debug.Log("c5 " + c5);
        Debug.Log("c6 " + c6);
        Debug.Log("c7 " + c7);
        Debug.Log("c8 " + c8);
        Debug.Log("c9 " + c9);
    }

    public void InsertIntoDatabase()
    {
        my_product _my_Product = my_product.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Insert(_my_Product);

        this.my_productid = _my_Product.my_productid;
    }

    public void UpdateDatabase()
    {
        my_product _my_Product = my_product.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Update(_my_Product);
    }

    public static void DeleteDatabase(int primaryKey)
    {
        DatabaseManager.sharedManager().databaseDocument.Delete<my_product>(primaryKey);
    }

    public static void DeleteAllDatabase()
    {
        DatabaseManager.sharedManager().databaseDocument.DeleteAll<my_product>();
    }

    #region Update Price

    static string[] updatePrice =
     {
        "2,0;1,0",
        "2,20;1,5",
        "2,25;1,7",
        "2,30;1,10",
        "2,35;1,12",
        "2,40;1,15",
        "2,45;1,16",
        "2,50;1,18",
        "2,55;1,18",
        "2,60;1,20",
        "2,72;1,20",
        "2,122;1,25",
        "2,137;1,25",
        "2,151;1,30",
        "2,166;1,30",
        "2,180;1,35",
        "2,259;1,35",
        "2,278;1,35",
        "2,298;1,40",
        "2,317;1,40",
        "2,336;1,45",
        "2,576;1,45",
        "2,648;1,50",
        "2,720;1,50",
        "2,792;1,50",
        "2,864;1,55",
        "2,1248;1,55",
        "2,1344;1,55",
        "2,1440;1,60",
        "2,1536;1,60",
        "2,1632;1,60",
        "2,2160;1,60",
        "2,2280;1,65",
        "2,2400;1,65",
        "2,2520;1,65",
        "2,2640;1,70",
        "2,3312;1,70",
        "2,3456;1,70",
        "2,3600;1,75",
        "2,3744;1,75",
        "2,3888;1,80",
        "2,4704;1,100",
        "2,4872;1,100",
        "2,5040;1,100",
        "2,5208;1,100",
        "2,5376;1,100",
        "2,6336;1,100",
        "2,6528;1,100",
        "2,6720;1,100",
        "2,6912;1,100",
        "2,7296;1,100",
        "2,8640;1,100",
        "2,9072;1,100",
        "2,10560;1,100",
        "2,11040;1,100",
        "2,11520;1,100",
        "2,12000;1,100",
        "2,12480;1,100",
        "2,14256;1,100",
        "2,14784;1,100",
        "2,15312;1,100",
        "2,16104;1,100",
        "2,16896;1,100",
        "2,19296;1,100",
        "2,20160;1,100",
        "2,21024;1,100",
        "2,23712;1,100",
        "2,24648;1,100",
        "2,27552;1,100",
        "2,28560;1,100",
        "2,29568;1,100",
        "2,30912;1,100",
        "2,32256;1,100",
        "2,36000;1,100",
        "2,37440;1,100",
        "2,38880;1,100",
        "2,40320;1,100",
        "2,41760;1,100",
        "2,46080;1,100",
        "2,47616;1,100",
        "2,49152;1,100",
        "2,54264;1,100",
        "2,56304;1,100",
        "2,61776;1,100",
        "2,63936;1,100",
        "2,66096;1,100",
        "2,72048;1,100",
        "2,74328;1,100",
        "2,80640;1,100",
        "2,83040;1,100",
        "2,85440;1,100",
        "2,92736;1,100",
        "2,95760;1,100",
        "2,104000;1,100",
        "2,107000;1,100",
        "2,110000;1,100",
        "2,119000;1,100",
        "2,122000;1,100",
        "2,131000;1,100",
        "2,134000;1,100"
    };

    static string[] updatePriceGem =
    {
        "2,0;3,0",
        "2,20;3,5",
        "2,25;3,7",
        "2,30;3,10",
        "2,35;3,12",
        "2,40;3,15",
        "2,45;3,16",
        "2,50;3,18",
        "2,55;3,18",
        "2,60;3,20",
        "2,72;3,20",
        "2,122;3,25",
        "2,137;3,25",
        "2,151;3,30",
        "2,166;3,30",
        "2,180;3,35",
        "2,259;3,35",
        "2,278;3,35",
        "2,298;3,40",
        "2,317;3,40",
        "2,336;3,45",
        "2,576;3,45",
        "2,648;3,50",
        "2,720;3,50",
        "2,792;3,50",
        "2,864;3,55",
        "2,1248;3,55",
        "2,1344;3,55",
        "2,1440;3,60",
        "2,1536;3,60",
        "2,1632;3,60",
        "2,2160;3,60",
        "2,2280;3,65",
        "2,2400;3,65",
        "2,2520;3,65",
        "2,2640;3,70",
        "2,3312;3,70",
        "2,3456;3,70",
        "2,3600;3,75",
        "2,3744;3,75",
        "2,3888;3,80",
        "2,4704;3,100",
        "2,4872;3,100",
        "2,5040;3,100",
        "2,5208;3,100",
        "2,5376;3,100",
        "2,6336;3,100",
        "2,6528;3,100",
        "2,6720;3,100",
        "2,6912;3,100",
        "2,7296;3,100",
        "2,8640;3,100",
        "2,9072;3,100",
        "2,10560;3,100",
        "2,11040;3,100",
        "2,11520;3,100",
        "2,12000;3,100",
        "2,12480;3,100",
        "2,14256;3,100",
        "2,14784;3,100",
        "2,15312;3,100",
        "2,16104;3,100",
        "2,16896;3,100",
        "2,19296;3,100",
        "2,20160;3,100",
        "2,21024;3,100",
        "2,23712;3,100",
        "2,24648;3,100",
        "2,27552;3,100",
        "2,28560;3,100",
        "2,29568;3,100",
        "2,30912;3,100",
        "2,32256;3,100",
        "2,36000;3,100",
        "2,37440;3,100",
        "2,38880;3,100",
        "2,40320;3,100",
        "2,41760;3,100",
        "2,46080;3,100",
        "2,47616;3,100",
        "2,49152;3,100",
        "2,54264;3,100",
        "2,56304;3,100",
        "2,61776;3,100",
        "2,63936;3,100",
        "2,66096;3,100",
        "2,72048;3,100",
        "2,74328;3,100",
        "2,80640;3,100",
        "2,83040;3,100",
        "2,85440;3,100",
        "2,92736;3,100",
        "2,95760;3,100",
        "2,104000;3,100",
        "2,107000;3,100",
        "2,110000;3,100",
        "2,119000;3,100",
        "2,122000;3,100",
        "2,131000;3,100",
        "2,134000;3,100"
    };

    #endregion
}
