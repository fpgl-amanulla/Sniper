
public class AppDelegate
{
    public static AppDelegate Instance = null;

    public static AppDelegate SharedManager()
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
}
