
using System;
using System.Collections.Generic;

public class AppDelegate
{
    public static AppDelegate Instance = null;
    public List<ProductData> allProductData = new List<ProductData>();
    public List<ProductLandData> allProductLandData = new List<ProductLandData>();

    public static AppDelegate sharedManager()
    {
        if (Instance == null)
            Instance = AppDelegate.Create();
        return Instance;
    }

    private static AppDelegate Create()
    {
        AppDelegate ret = new AppDelegate();
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
        DBUserInfo userInfo = DBUserInfo.Create(1);
        if (userInfo == null || userInfo.UDID != "user")
        {
            SetDefaults(userInfo);
            userInfo.InsertIntoDatabase();
        }
        return true;
    }
    private void SetDefaults(DBUserInfo userInfo)
    {
        userInfo.UDID = "user";
        userInfo.coins = 0;
        userInfo.bucks = 0;
        userInfo.experience = 0;
        userInfo.active_screenid = 0;
        userInfo.level = 0;
        userInfo.last_visited = 0;
    }

    public static string[] ComponentsSeparatedByString(string _string, string separator)
    {
        string[] arrSeparator = { separator };
        string[] arrString = _string.Split(arrSeparator, System.StringSplitOptions.RemoveEmptyEntries);

        return arrString;
    }

    public static int GetTime()
    {
        int timestamp = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        return timestamp;
    }
}
