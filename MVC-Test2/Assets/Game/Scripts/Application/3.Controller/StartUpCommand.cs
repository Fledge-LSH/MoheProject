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

        G_Object book = GameObject.Find("book").GetComponent<G_Object>();
        G_Object map = GameObject.Find("map").GetComponent<G_Object>();
        G_Object key = GameObject.Find("key").GetComponent<G_Object>();
        G_Object drug = GameObject.Find("drug").GetComponent<G_Object>();



        mbag.InitBag();
        //mbag.m_Grid[0].objName = book.name;
        //mbag.m_Grid[0].Cur_Count = 15;

        Debug.Log("格子数量："+mbag.m_Grid.Count);

        //查看背包格子状态
        foreach (Grid gri in mbag.m_Grid) 
        {
            Debug.Log(gri.IsEmpty);
        }

        //添加物品
        mbag.addobj(book, 58);
        mbag.addobj(key, 22);
        mbag.addobj(map, 13);
        mbag.addobj(map, 15);
        mbag.addobj(drug);
        mbag.addobj(drug, 46);

        Debug.Log(mbag.m_Grid[0].IsEmpty);
        Debug.Log(mbag.m_Grid[6].IsEmpty);

        //取出物品
        mbag.GetObject(mbag.m_Grid[0], 10);
        mbag.GetObject(mbag.m_Grid[6], 4);

        //移除格子中的物品
        mbag.remove(mbag.m_Grid[7]);
        Debug.Log("mbag.m_Grid[7]:"+ mbag.m_Grid[7].IsEmpty);

        ////查看物品信息
        //ObjectInfo o = mbag.GetObjectInfo(drug);
        //if (o!=null) 
        //{
        //    Debug.Log("物品名字：" + o.objName + "\r\n" + "物品ID：" + o.ID + "\r\n" + "物品信息：" + o.info);
        //}

        foreach (KeyValuePair<int,ObjectInfo> o in StaticData.objectInfo) 
        {
            Debug.Log("物品名称："+o.Value.objName+"   物品ID："+o.Value.ID+"   物品类型："+o.Value.type+"   基本信息："+o.Value.info+"\r\n");
        }

        //获取视图
        V_Bag vbag = GetView<V_Bag>();
        //更新视图
        vbag.refreshBag();
    }
}
