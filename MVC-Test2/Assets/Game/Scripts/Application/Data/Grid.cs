using System;

public class Grid
{
    //格子存储的物品
    public string objName;

    public int objId;

    public ObjectType objType;
    //当前存储的数量 默认为零
    int Count;
    //格子最大容量
    public int Max_Count;
    //格子是否为空
    bool isEmpty;

    //构造函数.
    //public Grid(string objname,int count=1)
    //{
    //    this.objName = objname;
    //    Cur_Count = count;
    //}

    public void InitGrid() 
    {
        //初始化格子时，格子状态为空
        IsEmpty = true;
        objId = -1;
        objType = ObjectType.Mat;
        Max_Count = 20;
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

    public bool IsEmpty 
    {
        get 
        {
            return isEmpty;
        }

        set 
        {
            isEmpty = value;
            //当格子设置为空时
            if (value==true) 
            {
                objName = "";
                Cur_Count = 0;
            }
        }
    }

    public int Remain_Cpacity 
    {
        get { return Max_Count - Cur_Count; }
    }

}
