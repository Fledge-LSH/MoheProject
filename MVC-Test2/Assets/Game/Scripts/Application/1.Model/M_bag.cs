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
    public List<Grid> m_Grid = new List<Grid>();

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
            Max_Capacity = value;
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
            if (CurrentCapacity == MaxCapacity)
                return true;
            else
                return false;
        }
    }
    #endregion

    #region 方法
    public void InitBag() 
    {
        //初始化背包
        MaxCapacity = 25;
        CurrentCapacity = MaxCapacity;
        m_Grid.Clear();

        for (int i=0;i<MaxCapacity;i++) 
        {
            Grid g = new Grid();

            //初始化格子
            g.InitGrid();

            m_Grid.Add(g);
        }
    }

    //查找背包中存储obj物品的格子中剩余未使用的个数总数
    int RemainCapacity(G_Object obj) 
    {
        int r=0;
        foreach (Grid g in m_Grid) 
        {
            if (g.objName == obj.name)
                r += (g.Max_Count-g.Cur_Count);
        }
        return r;
    }

    //添加物品
    public int addobj(G_Object obj, int count = 1)
    {
        Grid g = new Grid();
        g.InitGrid();

        int o;//返回值

        //1.首先判断背包容量是否够大
        //先遍历未存储物品的格子

        int rg=0;//用来存储剩余格子个数

        foreach (Grid gri in m_Grid) 
        {
            if (gri.IsEmpty==true) 
            {
                rg++;
            }
        }

        //再查找已存该物品中剩余的格子
        int ro = RemainCapacity(obj);
        //再计算出总的剩余的格子数
        int sum = rg * g.Max_Count + ro;

        if (count > sum)
        {
            //超出背包容量
            o = count - sum;
            count = sum;
        }
        else 
        {
            //未超出背包容量 返回值为零
            o = 0;
        }

        //2.开始添加物品
        if (Is_Empty)
        {
            //背包为空
            int t = count / g.Max_Count;
            int s = count % g.Max_Count;
            if (s != 0)
                t++;
            for (int i = 0; i < m_Grid.Count; i++)
            {
                if (t == 1 && s != 0)
                {
                    m_Grid[i].objName = obj.name;
                    m_Grid[i].Cur_Count = s;
                    //格子不为空
                    m_Grid[i].IsEmpty = false;
                    CurrentCapacity--;
                    t = 0;
                    break;
                }
                else if (t == 1 && s == 0)
                {
                    m_Grid[i].objName = obj.name;
                    m_Grid[i].Cur_Count = m_Grid[i].Max_Count;
                    //格子不为空
                    m_Grid[i].IsEmpty = false;
                    CurrentCapacity--;
                    t = 0;
                    break;
                }
                m_Grid[i].objName = obj.name;
                m_Grid[i].Cur_Count = m_Grid[i].Max_Count;
                //格子不为空
                m_Grid[i].IsEmpty = false;
                CurrentCapacity--;
                t--;

            }

        }
        else 
        {
           //背包不为空

            //将未填满的格子填满
            foreach (Grid gri in m_Grid) 
            {
                //如果格子剩余容量为零则跳过
                if (gri.Remain_Cpacity == 0)
                    continue;

                //找到该物体
                if (gri.objName == obj.name)
                {
                    if (count > gri.Remain_Cpacity)
                    {
                        count -= gri.Remain_Cpacity;
                        gri.Cur_Count = MaxCapacity;
                        continue;
                    }
                    else
                    {
                        gri.Cur_Count += count;
                        count = 0;
                        break;
                    }
                }
            }

            //如果没有剩余 则返回o
            if (count == 0)
                return o;

            //将剩余的存放在空格子中
            foreach (Grid gri in m_Grid) 
            {
                //如果格子剩余容量为零则跳过
                if (gri.Remain_Cpacity == 0)
                    continue;

                //找到空格子 将剩余物品装入
                if (gri.IsEmpty==true)
                {
                    if (count > gri.Max_Count)
                    {
                        count -= gri.Max_Count;
                        gri.objName = obj.name;
                        gri.Cur_Count = MaxCapacity;
                        //格子不为空
                        gri.IsEmpty = false;
                        CurrentCapacity--;
                    }
                    else 
                    {
                        gri.objName = obj.name;
                        gri.Cur_Count += count;
                        //格子不为空
                        gri.IsEmpty = false;
                        count = 0;
                        CurrentCapacity--;
                        break;
                    }
                }
            }
        }

        return o;
    }


    //1.添加物品（弃）
    //public int addObject(G_Object obj,int count=1)
    //{
    //    //计算多出来的装不下的物品数量
    //    int o=0;

    //    //1、判断背包是否满了
    //    if (Is_MaxCapacity)
    //    {
    //        //全部返回
    //        o = count;
    //        return o;
    //    }
    //    //2、判断背包是否是空的 是否为第一次添加东西
    //    if (Is_Empty) 
    //    {
    //        Grid g=new Grid(obj.name);
    //        //计算出所需格子数t
    //        int sum = (g.Cur_Count + count)-1;
    //        int t = sum / g.Max_Count;
    //        int s = sum % g.Max_Count;
    //        if (s > 0)
    //        {
    //            t++;
    //        }
    //        //判断所需格子数是否超过背包容量
    //        if (t > CurrentCapacity)
    //        {
    //            o = (t - CurrentCapacity-1) * g.Max_Count + s;
    //            t = CurrentCapacity;
    //        }
    //        //重新计算当前背包剩余容量
    //        CurrentCapacity -= t;
    //        //判断所需格子数是否大于1 把刚刚创建的格子填满并加入到队列中
    //        if (t > 1)
    //        {
    //            //t--;

    //            for (int i=0;i<t;i++) 
    //            {
    //                m_Grid[i].objName = obj.name;
    //                m_Grid[i].Cur_Count = count;
    //            }

    //            //g.Cur_Count = g.Max_Count;
    //            //m_Grid.Add(g);
    //        }
    //        //创建格子存储物品
    //        for (int i = 1; i <= t; i++)
    //        {
    //            if (i == t)
    //            {
    //                Grid gri1 = new Grid(obj.name, s);
    //                m_Grid.Add(gri1);

    //                AddObjectArgs e1 = new AddObjectArgs();
    //                e1.objID = obj.ID;
    //                e1.count = s;
    //                SendEvent(Consts.E_AddObject, e1);
    //                return o;
    //            }
    //            Grid gri2 = new Grid(obj.name, g.Max_Count);
    //            m_Grid.Add(gri2);

    //            //发送添加物品事件
    //            AddObjectArgs e2 = new AddObjectArgs();
    //            e2.objID = obj.ID;
    //            e2.count = g.Max_Count;
    //            SendEvent(Consts.E_AddObject,e2);
    //        }
    //    }
    //    //3、判断背包中是否已经有了该物体并且未存满格子
    //    foreach (Grid g in m_Grid) 
    //    {
    //        if (g.objName == obj.name && g.Cur_Count < g.Max_Count)
    //        {
    //            //计算出未满格子中物体的数量并且加上需要增加的个数
    //            int sum = g.Cur_Count + count;
    //            int t = sum / g.Max_Count;
    //            int s = sum % g.Max_Count;
    //            //算出需要的格子个数
    //            if (s > 0)
    //            {
    //                t++;
    //            }
    //            //如果需要一个以上的格子
    //            if (t>1) 
    //            {
    //                //将原先未填满格子排除 并且填满该格子
    //                t--;
    //                g.Cur_Count = g.Max_Count;
    //            }
    //            //如果所需格子数大于背包剩余容量
    //            if (t > Current_Capacity)
    //            {
    //                o = (t - CurrentCapacity - 1) * g.Max_Count + s;
    //                t = CurrentCapacity;
    //            }
    //            //重新计算当前背包剩余容量
    //            Current_Capacity -= t;
    //            //需要创建t个格子存储物品
    //            for (int i = 1; i <= t; i++)
    //            {
    //                if (i == t)
    //                {
    //                    //到了最后一个格子
    //                    Grid gri1 = new Grid(obj.name, s);
    //                    m_Grid.Add(gri1);

    //                    //发送添加物品事件
    //                    AddObjectArgs e3 = new AddObjectArgs();
    //                    e3.objID = obj.ID;
    //                    e3.count = s;
    //                    SendEvent(Consts.E_AddObject, e3);
    //                    return o;
    //                }
    //                Grid gri2 = new Grid(obj.name, g.Max_Count);
    //                m_Grid.Add(gri2);

    //                //发送添加物品事件
    //                AddObjectArgs e4 = new AddObjectArgs();
    //                e4.objID = obj.ID;
    //                e4.count = g.Max_Count;
    //                SendEvent(Consts.E_AddObject, e4);
    //            }
    //            break;
    //        }
    //        else if(g == m_Grid.Last())
    //        {
    //            int sum = count;
    //            int t = sum / g.Max_Count;
    //            int s = sum % g.Max_Count;
    //            //算出需要的格子个数
    //            if (s > 0)
    //            {
    //                t++;
    //            }
    //            if (t > Current_Capacity)
    //            {
    //                 o = (t - CurrentCapacity-1) * g.Max_Count + s;
    //                t = CurrentCapacity;
    //            }
    //            //重新计算当前背包剩余容量
    //            CurrentCapacity -= t;
    //            //创建格子存储物品
    //            for (int i = 1; i <= t; i++)
    //            {
    //                if (i == t)
    //                {
    //                    Grid gri1 = new Grid(obj.name, s);
    //                    m_Grid.Add(gri1);

    //                    //发送添加物品事件
    //                    AddObjectArgs e5 = new AddObjectArgs();
    //                    e5.objID = obj.ID;
    //                    e5.count = s;
    //                    SendEvent(Consts.E_AddObject, e5);
    //                    return o;
    //                }
    //                Grid gri2 = new Grid(obj.name, g.Max_Count);
    //                m_Grid.Add(gri2);

    //                //发送添加物品事件
    //                AddObjectArgs e6 = new AddObjectArgs();
    //                e6.objID = obj.ID;
    //                e6.count = g.Max_Count;
    //                SendEvent(Consts.E_AddObject, e6);
    //            }
    //        }
    //    }
    //    return o;
    //}

    //2.获取物品信息

    //获取物品信息
    public ObjectInfo GetObjectInfo(G_Object obj) 
    {
        ObjectInfo objInfo;
        //遍历物品信息字典
        foreach (KeyValuePair<int,ObjectInfo> o in StaticData.objectInfo) 
        {
            if (o.Value.objName == obj.name) 
            {
                objInfo = o.Value;
                return objInfo;
            }
        }
        return null;
    }

    //取出格子里面物品
    public G_Object GetObject(Grid g,int Count=1) 
    {
        //判断格子是否为空
        if (g.IsEmpty) 
        {
            return null;
        }
        G_Object go=new G_Object(g.objName,1);
        //判断数量
        if (Count>=g.Cur_Count) 
        {
            //如果取出物品个数大于等于格子内个数 置空格子
            Count = g.Cur_Count;
            go.Count = Count;
            g.IsEmpty = true;
            return go;
        }

        go.Count = Count;
        g.Cur_Count -= Count;

        return go;
    }

    //移除选中格子中的物品
    public G_Object remove(Grid g) 
    {
        //首先判断该格子是否为空
        if (!g.IsEmpty)
        {
            G_Object go = new G_Object(g.objName, g.Cur_Count);
            //格子置空
            g.IsEmpty = true;
            return go;
        }
        else
            return null;
    }

    //背包按ID排序
    public void IdSort() 
    {
        Grid gri;
        //冒泡排序法
        for (int j=0;j<m_Grid.Count-1;j++) 
        {
            for (int i=0;i<m_Grid.Count-1-j;i++) 
            {
                if (m_Grid[i].objId>m_Grid[i+1].objId) 
                {
                    gri = m_Grid[i];
                    m_Grid[i] = m_Grid[i + 1];
                    m_Grid[i + 1] = gri;
                }
            }
        }
    }
    //背包按类型排序
    public void TypeSort() 
    {
        int m1=0, e1=0, d1=0;//用来记录背包中三种类型物品的个数
        //遍历集合，计算每种类型物品的个数
        foreach (Grid gri in m_Grid) 
        {
            switch (gri.objType) 
            {
                case ObjectType.Mat:
                    m1++;
                    break;
                case ObjectType.Eqiup:
                    e1++;
                    break;
                case ObjectType.Drug:
                    d1++;
                    break;
            }
        }

        //按照Mat、Eqiup、Drug顺序排序
        List<Grid> mgri = new List<Grid>();
        int m2 = 0, e2 = 0, d2 = 0;//记录三种类型物品已添加个数
        foreach (Grid gri2 in m_Grid) 
        {
            switch (gri2.objType)
            {
                case ObjectType.Mat:
                    mgri[m2] = gri2;
                    m2++;
                    break;
                case ObjectType.Eqiup:
                    mgri[m1 + e2] = gri2;
                    e2++;
                    break;
                case ObjectType.Drug:
                    mgri[m1 + e1 + d2] = gri2;
                    break;
            }
        }
        m_Grid = mgri;
    }
    //背包按类型和ID排序
    public void Sort() 
    {
        //先按类型排序 再按Id排序
        TypeSort();
        IdSort();
    }
    #endregion

    #region unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}
