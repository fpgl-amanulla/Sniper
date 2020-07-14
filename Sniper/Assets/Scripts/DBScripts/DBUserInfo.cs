using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;
using System.Linq;

public class users
{
    [AutoIncrement, PrimaryKey]
    public int uid { get; set; }
    public string UDID { get; set; }
    public int coins { get; set; }
    public int bucks { get; set; }
    public int experience { get; set; }
    public int active_screenid { get; set; }
    public int level { get; set; }
    public int last_visited { get; set; }



    public static users Create(DBUserInfo user)
    {
        users ret = new users();
        if (ret != null && ret.Init(user))
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    private bool Init(DBUserInfo user)
    {
        this.uid = user.uid;
        this.UDID = user.UDID;
        this.coins = user.coins;
        this.bucks = user.bucks;
        this.experience = user.experience;
        this.active_screenid = user.active_screenid;
        this.level = user.level;
        this.last_visited = user.last_visited;

        return true;
    }

    //public DBUserInfo GetDBUserInfo()
    //{
    //    DBUserInfo userInfo = new DBUserInfo();
    //    GetDBUserInfo(userInfo);
    //    return userInfo;
    //}

    public void GetDBUserInfo(DBUserInfo userInfo)
    {
        userInfo.uid = this.uid;
        userInfo.UDID = this.UDID;
        userInfo.coins = this.coins;
        userInfo.bucks = this.bucks;
        userInfo.experience = this.experience;
        userInfo.active_screenid = this.active_screenid;
        userInfo.level = this.level;
        userInfo.last_visited = this.last_visited;
    }
}

public class levels
{
    [PrimaryKey, AutoIncrement]
    public int levelid { get; set; }
    public int experience { get; set; }
    public int bonous_Coins { get; set; }
    public int bonous_bucks { get; set; }
    public int c1 { get; set; }
    public int c2 { get; set; }
    public int c3 { get; set; }
    public int c4 { get; set; }
    public int c5 { get; set; }
}

public class DBUserInfo
{
    public int uid { get; set; }
    public string UDID { get; set; }
    public int coins { get; set; }
    public int bucks { get; set; }
    public int experience { get; set; }
    public int active_screenid { get; set; }
    public int level { get; set; }
    public int last_visited { get; set; }
    public int exp_current { get; set; }
    public int exp_remain { get; set; }

    public static List<DBUserInfo> allUserInfo = new List<DBUserInfo>();

    public static DBUserInfo Create()
    {
        DBUserInfo ret = new DBUserInfo();
        if (ret != null && ret.Init())
        {
            return ret;
        }
        else
        {
            return null;
        }
    }

    public static DBUserInfo Create(int primarykey)
    {
        DBUserInfo ret = new DBUserInfo();
        if (ret != null && ret.Init(primarykey))
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

    private bool Init(int primarykey)
    {
        IEnumerable<users> userList = DatabaseManager.sharedManager().databaseDocument.Table<users>().Where(t => t.uid == primarykey);

        foreach (var t in userList)
        {
            users _userInfo = t;
            _userInfo.GetDBUserInfo(this);
        }

        SetExpCurrentAndRemain();

        return true;
    }

    public void ShowDBUserInfo()
    {
        Debug.Log("-------------------------");
        Debug.Log("uid " + uid);
        Debug.Log("UDID " + UDID);
        Debug.Log("coins " + coins);
        Debug.Log("bucks " + bucks);
        Debug.Log("experience " + experience);
        Debug.Log("active_screenid " + active_screenid);
        Debug.Log("level " + level);
        Debug.Log("last_visited " + last_visited);
        Debug.Log("exp_current " + exp_current);
        Debug.Log("exp_remain " + exp_remain);
    }

    public int InsertIntoDatabase()
    {
        users user = users.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Insert(user);
        return user.uid;
    }

    public void UpdateDatabase(bool isScorebarUpdate = true)
    {
        users user = users.Create(this);
        DatabaseManager.sharedManager().databaseDocument.Update(user);

        SetExpCurrentAndRemain();
    }

    //public List<DBUserInfo> GetAllUserInfo()
    //{
    //    if(allUserInfo.Count==0)
    //    {
    //        IEnumerable<users> ieumAllUserInfo = DatabaseManager.sharedManager().databaseDocument.
    //        Table<users>().ToList();
    //        foreach (var item in ieumAllUserInfo)
    //        {
    //            DBUserInfo userInfo = item.GetDBUserInfo();
    //            allUserInfo.Add(userInfo);
    //        }

    //    }
    //    return allUserInfo;

    //}

    public void UpdateLevel(bool isScoreBarUpdate = true)
    {
        /* int oldLevel = 0;
          int resultLevel = 0;
          int bonusCoins = 0;
          int bonusBucks = 0;

          //.............LOAD LEVEL AGAINST EXPRIENCE........................
          List<levels> str_xp = DatabaseManager.sharedManager().databaseDocument.
              Query<levels>("SELECT * FROM levels ORDER BY levelid DESC LIMIT 1").ToList();

          foreach (var item in str_xp)
          {
              if(item.experience <= experience)
              {
                  resultLevel = item.levelid;
                  bonusCoins = item.bonous_Coins;
                  bonusBucks = item.bonous_bucks;
              }
          }



          //.............SELECT USER CURRENT LEVEL............................
          //List<users> str_old = DatabaseManager.sharedManager().databaseDocument.
          //    Table<users>().ToList();

          //foreach (var item in str_old)
          //{
          //    if(item.uid == primarykey)
          //    {
          //        oldLevel = item.uid;
          //    }
          //}

          this.level = resultLevel;
          this.UpdateDatabase();

          if(resultLevel> oldLevel)
          {
              //if (!appDelegate->isVillageLevelBattleLevelSame)
                  UpdateLevel(resultLevel, isScoreBarUpdate);
          }*/

    }

    public void UpdateLevel(int resultLevel, bool isScoreBarUpdate = true)
    {
        int bonusCoins = 0;
        int bonusBucks = 0;
        List<levels> str_xp = DatabaseManager.sharedManager().databaseDocument.
            Query<levels>("SELECT * FROM levels ORDER BY levelid DESC LIMIT 1").ToList();

        foreach (var item in str_xp)
        {
            if (item.experience <= experience)
            {
                bonusCoins = item.bonous_Coins;
                bonusBucks = item.bonous_bucks;
            }
        }
        this.coins = coins + bonusCoins;
        this.bucks = bucks + bonusBucks;
        this.level = resultLevel;
        this.UpdateDatabase(isScoreBarUpdate);
        //if (appDelegate->isGameNodeLoaded)
        //{
        //    appDelegate->gHud->loadStorePanel(panelLevelUp);

        //    if (resultLevel >= 1)
        //    {
        //        ITIWScoreBar::sharedManager()->showStaticFightBtn();
        //    }

        //    Goal::sharedManager()->findAllGoal();

        //    Goal::sharedManager()->showQuestAnimation();

        //    if (resultLevel >= TagManager::challengeEnableLevel())
        //        ChallengeData::restoreChallenge();


        //    /*[[MyEventsManager sharedManager] addEvent:evTouchLevelUpdate Point:ccp(0,0) :0];

        //     appDelegate.bonusCoins=bonusCoins;
        //     [GameHud loadStorePanel:40];
        //     appDelegate.gnode.campaignTapCount=0;

        //     if([appDelegate.goalTutorial count]==0)
        //     [appDelegate.ghud update_goal_icon];

        //     for (int i=0; i<[appDelegate.allWishingObject count]; i++)
        //     {
        //     Decor *wishingObject=[appDelegate.allWishingObject objectAtIndex:i];
        //     [wishingObject checkForWishingDecorLevel];
        //     }

        //     if(resultLevel==4) [appDelegate.gnode restoreTankChallenge];
        //     if(resultLevel>4)
        //     {
        //     [appDelegate.gnode loadTankChallenge];
        //     [appDelegate.gnode updateChallengeObjects];
        //     }

        //     //            [[JigsawPuzzleNode sharedManager] showMenu];

        //     //          CCLOG(@"Level---> %@  %d",[NSString stringWithFormat:@"gaeUserLevel %d",resultLevel],oldLevel);

        //     [ITIWAppDelegate googleEventFight:[NSString stringWithFormat:@"gaeUserLevel %d",resultLevel] action:[NSString stringWithFormat:@"oldLevel %d",oldLevel] label:@""];

        //     if(/ *[appDelegate.goalTutorial count]==0 && * /resultLevel>3)
        //     {
        //     [appDelegate.ghud showLevelUpAnimation:resultLevel oldLevel:oldLevel];
        //     }*/

        //    //ITIWScoreBar::sharedManager()->hideStaticCenterMenu();

        //    //        appDelegate->gHud->showMinigameBtn();

        //    if (resultLevel >= 5 && resultLevel <= 8)
        //        DailyBonusProductNode::sharedManager()->showBonusMenu();
        //    if (appDelegate->userInfo->level >= 4 && appDelegate->userInfo->level <= 10)
        //    {
        //        appDelegate->gHud->startJump3Animation();
        //    }
        //    else
        //        appDelegate->gHud->stopJump3Animation();

        //    if (resultLevel == 5)
        //        InAppLand::restoreInAppIsland();
        //}
    }


    public void SetExpCurrentAndRemain()
    {
        int current_level = 1;

        List<levels> str_xp_current = DatabaseManager.sharedManager().databaseDocument.
            Table<levels>().ToList();
        foreach (var item in str_xp_current)
        {
            if (item.levelid == this.level)
            {
                this.exp_current = item.levelid;
                current_level = this.exp_current;
                this.exp_current = this.experience - this.exp_current;
            }
        }

        if (this.level <= 1)
        {
            this.exp_current = this.experience;
        }

        List<levels> str_xp_remain = DatabaseManager.sharedManager().databaseDocument.
           Table<levels>().ToList();
        foreach (var item in str_xp_current)
        {
            if (item.levelid == this.level + 1)
            {
                this.exp_remain = item.experience;
                this.exp_remain = this.exp_remain - current_level;
            }
        }
    }

    public int ExperienceNeededForNextLevel(int _currentLevel)
    {
        int expNeeded = 0;
        int currentLevelExp = 0;
        int nextLevelExp = 0;
        int count = 0;

        List<levels> str_xp_current = DatabaseManager.sharedManager().databaseDocument.
            Table<levels>().ToList();
        foreach (var item in str_xp_current)
        {
            if (this.experience == _currentLevel || this.experience == _currentLevel + 1)
            {
                if (count == 0) currentLevelExp = item.levelid;
                if (count == 1) nextLevelExp = item.levelid;
                count++;
            }
        }

        expNeeded = nextLevelExp - currentLevelExp;
        if (expNeeded < 1) expNeeded = 1;

        return expNeeded;
    }

}



