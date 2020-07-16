using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProductLandData
{
    public DBProductsLandInfo productLandInfo;
    public DBMyProductsLand myProductLand;

    public static ProductLandData create()
    {
        ProductLandData ret = new ProductLandData();
        if (ret != null && ret.init())
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    bool init()
    {
        return true;
    }

    public static ProductLandData create(int _myProductLandId)
    {
        ProductLandData ret = new ProductLandData();
        if (ret != null && ret.init(_myProductLandId))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    bool init(int _myProductLandId)
    {
        myProductLand = DBMyProductsLand.Create(_myProductLandId);
        productLandInfo = DBProductsLandInfo.Create(myProductLand.product_landid);

        return true;
    }

    public void ReloadProductLandData()
    {
        AppDelegate appDelegate = AppDelegate.sharedManager();
        appDelegate.allProductLandData.Clear();
        //DBScreenInfo screenInfo = new DBScreenInfo();

        IEnumerable<my_products_land> ieumAllMyProductLand = DatabaseManager.sharedManager().databaseDocument.Table<my_products_land>()/*.Where(t => t.screenid == screenInfo.screenid)*/;

        foreach (my_products_land my_product_land in ieumAllMyProductLand)
        {
            ProductLandData.AddProductLandData(my_product_land.my_product_landid);
        }
    }

    private static void AddProductLandData(int my_product_landId)
    {
        AppDelegate appDelegate = AppDelegate.sharedManager();

        ProductLandData productLandData = ProductLandData.create(my_product_landId);
        appDelegate.allProductLandData.Add(productLandData);

        LoadProductLandObject();

    }

    public static void CreateProductLandInDatabase(int _product_landid, Vector2 createTileLocation)
    {
        AppDelegate appDelegate = AppDelegate.sharedManager();

        DBProductsLandInfo productLandInfo = DBProductsLandInfo.Create(_product_landid);
        //DBScreenInfo tnk = appDelegate->tanks->at(appDelegate->selectedTankIndex);

        int current_time = AppDelegate.GetTime();

        DBMyProductsLand myProductLand = DBMyProductsLand.Create();
        myProductLand.product_landid = productLandInfo.products_landid;
        //myProductLand.screenid = tnk.screenid;
        myProductLand.tilex = (int)createTileLocation.x;
        myProductLand.tiley = (int)createTileLocation.y;

        myProductLand.land_level = 1;
        myProductLand.timestamp = 0;
        myProductLand.my_productid = "0";

        myProductLand.last_time_coin_collection = current_time.ToString();
        if (productLandInfo.elementid == -1)
        {
            //myProductLand.product_limit = StorageFreeSlo.ToString();
        }

        else
        {
            myProductLand.product_limit = "0";
        }

        myProductLand.c3 = "0";
        myProductLand.c4 = "0";
        myProductLand.c5 = "0";
        myProductLand.c6 = "0";
        myProductLand.InsertIntoDatabase();

        ProductLandData.AddProductLandData(myProductLand.my_product_landid);
    }

    void AddProductInProductLand(int productObjectAtIndex, int landObjectAtIndex)
    {
        if (productObjectAtIndex >= 0 && landObjectAtIndex >= 0)
        {

            AppDelegate appDelegate = AppDelegate.sharedManager();

            DBMyProductsLand myProductsLand = appDelegate.allProductLandData[(landObjectAtIndex)].myProductLand;
            //DBProductsLandInfo *productLandInfo=appDelegate->allProductLandData->at(landObjectAtIndex)->productLandInfo;

            ProductData productData = appDelegate.allProductData[(productObjectAtIndex)];

            if (myProductsLand.my_productid == "0")
                myProductsLand.my_productid = (productData.myProduct.my_productid).ToString();
            else
                myProductsLand.my_productid = myProductsLand.my_productid + "," + (productData.myProduct.my_productid).ToString();
            myProductsLand.UpdateDatabase();
        }
    }

    ProductLandData GetProductLandData(ProductData productData)
    {
        AppDelegate appDelegate = AppDelegate.sharedManager();

        for (int i = 0; i < appDelegate.allProductLandData.Count; i++)
        {
            ProductLandData productLandData = appDelegate.allProductLandData[i];
            List<string> activeProductIdList = AppDelegate.ComponentsSeparatedByString(productLandData.myProductLand.my_productid, ",").ToList();

            for (int j = 0; j < activeProductIdList.Count; j++)
            {
                int activeProductId = Int32.Parse(activeProductIdList[j]);
                if (activeProductId == productData.myProduct.my_productid)
                {
                    return productLandData;
                }
            }
        }
        return null;
    }

    private static void LoadProductLandObject()
    {
        AppDelegate appDelegate = AppDelegate.sharedManager();
        //if (appDelegate.isGameNodeLoaded)
        {
            ProductLandData productLandData = appDelegate.allProductLandData[appDelegate.allProductLandData.Count - 1];
            productLandData.LoadProductLand(appDelegate.allProductLandData.Count - 1);
        }
    }

    private void LoadProductLand(int objectAtIndex)
    {
        //ITIWTiledMap* tiledMap = ITIWTiledMap::sharedManager();
        //productLandObject = ProductLand::create(objectAtIndex);
        //tiledMap->getParent()->addChild(productLandObject, -10 + ((myProductLand->tilex - (productLandInfo->tilex / 2)) + (myProductLand->tiley - (productLandInfo->tiley / 2))));
    }

    void RestoreProductLand()
    {
        AppDelegate appDelegate = AppDelegate.sharedManager();

        for (int i = 0; i < appDelegate.allProductLandData.Count; i++)
        {
            ProductLandData productLandData = appDelegate.allProductLandData[i];
            productLandData.LoadProductLand(i);
        }
    }


}
