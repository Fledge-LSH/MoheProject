using System;
using UnityEngine;

public class G_Object: ReusableObject
{
    //游戏物品类
    public string name;
    public int id;
    public int count;
    public ObjectType type;


    //物品数量
    public int Count
    {
        get { return count; }
        set { count = value; }
    }

    public string Name 
    {
        get { return name; }
    }
    public int ID 
    {
        get { return id; }
    }

    public ObjectType Type 
    {
        get {return type;}
    }

    #region 方法
    //构造方法
    public G_Object(string Name ,int Count=1)
    {
        name = Name;
        count = Count;
    }
    #endregion
}
