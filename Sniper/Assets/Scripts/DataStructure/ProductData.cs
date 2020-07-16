using System;
using System.Collections.Generic;
using UnityEngine;

public class ProductData
{
    public DBMyProduct myProduct;
    public DBProductInfo productInfo;

    public static ProductData Create()
    {
        ProductData ret = new ProductData();
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
        return true;
    }

    public static ProductData Create(int productId)
    {
        ProductData ret = new ProductData();
        if (ret != null && ret.Init(productId))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }
    public bool Init(int productId)
    {
        myProduct = DBMyProduct.Create(productId);
        productInfo = DBProductInfo.GetProductInfo(productId);
        return true;
    }
    public static void AddProductData(int _productId)
    {
        AppDelegate appDelegate = AppDelegate.sharedManager();

        ProductData productData = ProductData.Create(_productId);
        appDelegate.allProductData.Add(productData);
    }

    public static void CreateProductInDatabase(int productId)
    {
        DBProductInfo productInfo = DBProductInfo.GetProductInfo(productId);

        DBMyProduct myProduct = DBMyProduct.Create(productId);

        //Debug.Log(myProduct.productid);

        if (myProduct == null)
        {
            myProduct = DBMyProduct.Create();

            myProduct.productid = productId;
            myProduct.screenid = 0;
            myProduct.my_name = productInfo.product_name;
            myProduct.time_stamp = 0;
            myProduct.my_level = 0;
            myProduct.my_tier = 0;
            myProduct.my_pieces = 0;
            myProduct.my_pieces = 0;
            myProduct.cost = "0";
            myProduct.c2 = "0";
            myProduct.c3 = "0";
            myProduct.c4 = "0";
            myProduct.c5 = "0";
            myProduct.c6 = "0";
            myProduct.c7 = "0";
            myProduct.c8 = "0";
            myProduct.c9 = "0";

            myProduct.InsertIntoDatabase();
            ProductData.AddProductData(myProduct.productid);
        }
    }

    public static void ReloadProductData()
    {
        AppDelegate appDelegate = AppDelegate.sharedManager();
        appDelegate.allProductData.Clear();

        IEnumerable<my_product> ieumAllMyProduct = DatabaseManager.sharedManager().databaseDocument.Table<my_product>();

        foreach (my_product my_product in ieumAllMyProduct)
        {
            ProductData.AddProductData(my_product.productid);
        }
    }

}
