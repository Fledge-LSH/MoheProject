using System;

public class Grid
{
    //格子存储的物品
    public G_Object Gobj;
    //当前存储的数量 默认为零
    int Count=0;
    //格子最大容量
    public int Max_Count=20;

    //构造函数.
    public Grid(G_Object obj,int count=1)
    {
        Gobj = obj;
        Cur_Count = count;
    }
    //当前数量
    public int Cur_Count 
    {
        get { return Count; }
        set 
        {
            if (value >= Max_Count)
                Count = Max_Count;
            else
                Count = value;
        }
    }

}
