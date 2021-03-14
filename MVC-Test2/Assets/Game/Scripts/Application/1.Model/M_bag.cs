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
    public void InitMBag() 
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
    int RemainCapacity(GameObject obj) 
    {
        G_Object gobj = obj.GetComponent<G_Object>();
        int r=0;
        foreach (Grid g in m_Grid) 
        {
            ObjectInfo objinfo = GetGridObjectInfo(g);
            if (g.objId == gobj.ID)
                r += (g.Max_Count-g.Cur_Count);
        }
        return r;
    }

    //添加物品
    public int addobj(GameObject obj, int count = 1)
    {
        Grid g = new Grid();
        g.InitGrid();
        ObjectInfo objinfo=null;
        G_Object gobj = obj.GetComponent<G_Object>();
        //通过物品ID找到物品信息
        foreach (ObjectInfo info in StaticData.objectInfo.Values)
        {
            if (info.ID == gobj.ID)
            {
                objinfo = info;
            }
        }

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
                    m_Grid[i].objId = objinfo.ID;
                    m_Grid[i].Cur_Count = s;
                    //格子不为空
                    m_Grid[i].IsEmpty = false;
                    CurrentCapacity--;
                    t = 0;
                    break;
                }
                else if (t == 1 && s == 0)
                {
                    m_Grid[i].objId = objinfo.ID;
                    m_Grid[i].Cur_Count = m_Grid[i].Max_Count;
                    //格子不为空
                    m_Grid[i].IsEmpty = false;
                    CurrentCapacity--;
                    t = 0;
                    break;
                }

                m_Grid[i].objId = objinfo.ID;
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
                if (gri.objId == gobj.ID)
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
                        gri.objId = objinfo.ID;
                        gri.Cur_Count = MaxCapacity;
                        //格子不为空
                        gri.IsEmpty = false;
                        CurrentCapacity--;
                    }
                    else 
                    {
                        gri.objId = objinfo.ID;
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

    public ObjectInfo GetGridObjectInfo(Grid gri) 
    {
        ObjectInfo objInfo;
        //遍历物品信息字典
        foreach (ObjectInfo info in StaticData.objectInfo.Values) 
        {
            if (info.ID==gri.objId) 
            {
                objInfo = info;
                return objInfo;
            }
        }
        return null;
    }

    //取出格子里面物品
    public GameObject GetObject(Grid g,int Count=1) 
    {
        ObjectInfo objinfo=null;
        //判断格子是否为空
        if (g.IsEmpty) 
        {
            return null;
        }

        objinfo = GetGridObjectInfo(g);
        //从对象池中取出
        GameObject go = Game.Instance.ObjectPool.OnSpawn(objinfo.objName);

        //判断数量
        if (Count>=g.Cur_Count) 
        {
            //如果取出物品个数大于等于格子内个数 置空格子
            Count = g.Cur_Count;
            go.GetComponent<G_Object>().Count = Count;
            g.IsEmpty = true;
            return go;
        }

        go.GetComponent<G_Object>().Count = Count;
        g.Cur_Count -= Count;

        return go;
    }

    //移除选中格子中的物品
    public GameObject remove(Grid g) 
    {
        ObjectInfo objinfo = null;
        //首先判断该格子是否为空
        if (g.IsEmpty)
            return null;

        objinfo = GetGridObjectInfo(g);
        GameObject go = Game.Instance.ObjectPool.OnSpawn(objinfo.objName);
        //格子置空
        g.IsEmpty = true;
        return go;
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
        ObjectInfo objinfo = null;
        int m1=0, e1=0, d1=0;//用来记录背包中三种类型物品的个数
        //遍历集合，计算每种类型物品的个数
        foreach (Grid gri in m_Grid) 
        {
            objinfo = GetGridObjectInfo(gri);

            //如果为空则跳过
            if (objinfo==null) 
                continue;

            switch (objinfo.type) 
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
            objinfo = GetGridObjectInfo(gri2);

            //如果为空则跳过
            if (objinfo == null)
                continue;

            switch (objinfo.type)
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
