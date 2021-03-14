using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StartUpCommand : Controller
{
    public override void Execute(object data)
    {
        //注册模型
        RegisterModel(new M_Bag());
        //注册视图
        RegisterView(GameObject.Find("Bag").GetComponent<V_Bag>());

        M_Bag mbag = GetModel<M_Bag>();
        V_Bag vbag = GetView<V_Bag>();

        mbag.InitMBag();//初始化背包模型
        vbag.InitVBag();//初始化背包视图

        //mbag.m_Grid[0].objName = book.name;
        //mbag.m_Grid[0].Cur_Count = 15;

        Debug.Log("格子数量："+mbag.m_Grid.Count);

        //查看背包格子状态
        foreach (Grid gri in mbag.m_Grid) 
        {
            Debug.Log(gri.IsEmpty);
        }

        //创建一些物品
        GameObject g1= Game.Instance.ObjectPool.OnSpawn("泰拉之刃");
        GameObject g2 = Game.Instance.ObjectPool.OnSpawn("真永夜之刃");
        GameObject g3 = Game.Instance.ObjectPool.OnSpawn("红药水");
        GameObject g4 = Game.Instance.ObjectPool.OnSpawn("雪球大炮");
        GameObject g5 = Game.Instance.ObjectPool.OnSpawn("变性药水");
        GameObject g6 = Game.Instance.ObjectPool.OnSpawn("变态刀");

        //添加物品
        mbag.addobj(g1);
        mbag.addobj(g2);
        mbag.addobj(g3,50);
        mbag.addobj(g4);
        mbag.addobj(g5,100);
        mbag.addobj(g6);

        //取出物品
        mbag.GetObject(mbag.m_Grid[0], 10);
        mbag.GetObject(mbag.m_Grid[2], 4);

        //移除格子中的物品
        mbag.remove(mbag.m_Grid[3]);
        Debug.Log("mbag.m_Grid[3] is empty?:" + mbag.m_Grid[3].IsEmpty);

        //查看物品信息
        ObjectInfo j = mbag.GetGridObjectInfo(mbag.m_Grid[4]);
        if (j != null)
        {
            Debug.Log("物品名字：" + j.objName + "\r\n" + "物品ID：" + j.ID + "\r\n" + "物品信息：" + j.info);
        }

        //foreach (KeyValuePair<int,ObjectInfo> o in StaticData.objectInfo) 
        //{
        //    Debug.Log("物品名称："+o.Value.objName+"   物品ID："+o.Value.ID+"   物品类型："+o.Value.type+"   基本信息："+o.Value.info+"\r\n");
        //}

       // mbag.Sort();
        //获取视图
        vbag = GetView<V_Bag>();

        //更新视图
        vbag.refreshBag();
    }
}
