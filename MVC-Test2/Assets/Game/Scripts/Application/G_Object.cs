using System;
using UnityEngine;

public abstract class G_Object: UnityEngine.Object
{
    string name;
    int id;
    int count;

    protected G_Object(string name,int id)
    {
        this.id = id;
    }
    //物品数量
    public abstract int Count { get; set; }
    //游戏物品类 所有游戏物品都要继承这个类
    public abstract string Name { get ; }
    public abstract int ID { get; }
}
