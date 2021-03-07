using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class M_Bag : Model
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    //背包物品存储表
    public Dictionary<int, G_Object> m_Bag = new Dictionary<int, G_Object>();

    //背包格子
    List<Grid> m_Grid = new List<Grid>();

    //最大格子个数
    int Max_Capacity;

    //当前背包剩余格子数
    int Current_Capacity;
    #endregion

    #region 属性
    //Model标识
    public override string Name
    {
        get { return Consts.M_Bag; }
    }
    //背包当前容量
    public int CurrentCapacity 
    {
        set 
        {
            Current_Capacity = value;
            if (Current_Capacity<0)
            {
                Current_Capacity = 0;
            }
        }
        get { return Current_Capacity; }
    }

    public int MaxCapacity 
    {
        get { return Max_Capacity; }
        set 
        {
            Max_Capacity = MaxCapacity;
        }
    }
    //背包是否满了
    public bool Is_MaxCapacity 
    {
        get 
        {
            if (Current_Capacity ==0)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
    //背包是否为空
    public bool Is_Empty 
    {
        get 
        {
            if (CurrentCapacity == 0)
                return true;
            else
                return false;
        }
    }
    #endregion

    #region 方法
    //1.添加物品
    public int addObject(G_Object obj,int count=1)
    {
        //计算多出来的装不下的物品数量
        int o=0;
        //1、判断背包是否满了
        if (Is_MaxCapacity)
        {
            //全部返回
            o = count;
            return o;
        }
        //2、判断背包是否是空的 是否为第一次添加东西
        if (Is_Empty) 
        {
            Grid g=new Grid(obj);
            //计算出所需格子数t
            int sum = (g.Cur_Count + count)-1;
            int t = sum / g.Max_Count;
            int s = sum % g.Max_Count;
            if (s > 0)
            {
                t++;
            }
            //判断所需格子数是否超过背包容量
            if (t > CurrentCapacity)
            {
                o = (t - CurrentCapacity-1) * g.Max_Count + s;
                t = CurrentCapacity;
            }
            //重新计算当前背包剩余容量
            CurrentCapacity -= t;
            //判断所需格子数是否大于1 把刚刚创建的格子填满并加入到队列中
            if (t > 1)
            {
                t--;
                g.Cur_Count = g.Max_Count;
                m_Grid.Add(g);
            }
            //创建格子存储物品
            for (int i = 1; i <= t; i++)
            {
                if (i == t)
                {
                    Grid gri1 = new Grid(obj, s);
                    m_Grid.Add(gri1);
                    return o;
                }
                Grid gri2 = new Grid(obj, g.Max_Count);
                m_Grid.Add(gri2);
            }
        }
        //2、判断背包中是否已经有了该物体并且未存满格子
        foreach (Grid g in m_Grid) 
        {
            if (g.Gobj.ID == obj.ID && g.Cur_Count < g.Max_Count)
            {
                //计算出未满格子中物体的数量并且加上需要增加的个数
                int sum = g.Cur_Count + count;
                int t = sum / g.Max_Count;
                int s = sum % g.Max_Count;
                //算出需要的格子个数
                if (s > 0)
                {
                    t++;
                }
                //如果需要一个以上的格子
                if (t>1) 
                {
                    //将原先未填满格子排除 并且填满该格子
                    t--;
                    g.Cur_Count = g.Max_Count;
                }
                //如果所需格子数大于背包剩余容量
                if (t > Current_Capacity)
                {
                    o = (t - CurrentCapacity - 1) * g.Max_Count + s;
                    t = CurrentCapacity;
                }
                //重新计算当前背包剩余容量
                Current_Capacity -= t;
                //需要创建t个格子存储物品
                for (int i = 1; i <= t; i++)
                {
                    if (i == t)
                    {
                        //到了最后一个格子
                        Grid gri1 = new Grid(obj, s);
                        m_Grid.Add(gri1);
                        return o;
                    }
                    Grid gri2 = new Grid(obj, g.Max_Count);
                    m_Grid.Add(gri2);
                }
                break;
            }
            else if(g == m_Grid.Last())
            {
                int sum = count;
                int t = sum / g.Max_Count;
                int s = sum % g.Max_Count;
                //算出需要的格子个数
                if (s > 0)
                {
                    t++;
                }
                if (t > Current_Capacity)
                {
                     o = (t - CurrentCapacity-1) * g.Max_Count + s;
                    t = CurrentCapacity;
                }
                //重新计算当前背包剩余容量
                CurrentCapacity -= t;
                //创建格子存储物品
                for (int i = 1; i <= t; i++)
                {
                    if (i == t)
                    {
                        Grid gri1 = new Grid(obj, s);
                        m_Grid.Add(gri1);
                        return o;
                    }
                    Grid gri2 = new Grid(obj, g.Max_Count);
                    m_Grid.Add(gri2);
                }
            }
        }
        return o;
    }
    //2.获取物品信息
    public ObjectInfo GetObjectInfo(Grid grid) 
    {
        ObjectInfo objInfo = new ObjectInfo();
        return  objInfo;
    }
    //3.取出物品
    public G_Object GetObject(string Name,int Count=1) 
    {
        G_Object obj=null;
        return obj;
    }
    //4.移除物品
    #endregion

    #region unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}
