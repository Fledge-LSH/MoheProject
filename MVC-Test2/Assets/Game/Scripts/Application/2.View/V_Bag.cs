using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class V_Bag : View
{

    int capacity;
    List<Grid> grid;
    public GameObject panel;
    public GameObject InfoPanel;
    
    #region 字段
    public override string Name
    {
        get { return Consts.V_Bag; }
    }

    public int Capacity 
    {
        get { return capacity; }
        set 
        {
            capacity = value;
        }
    }
    #endregion

    #region 方法

    public void InitVBag() 
    {
        //
    }

    //显示信息面板
    public void ShowInfoPanel(int index) 
    {
        M_Bag mbag = GetModel<M_Bag>();
        ObjectInfo objinfo;
        //如果格子为空则返回
        if (mbag.m_Grid[index].IsEmpty) 
        {
            HideInfoPanel();
            return;
        }
        //先显示信息
        InfoPanel.SetActive(true);

        //加载物品信息
        objinfo = Game.Instance.StaticData.GetObjectInfo(mbag.m_Grid[index].objId);

        Transform t1= InfoPanel.transform.Find("title");
        t1.transform.GetComponent<Text>().text = objinfo.objName;

        Transform t2 = InfoPanel.transform.Find("info");
        t2.transform.GetComponent<Text>().text = objinfo.info;
    }

    //隐藏信息面板
    public void HideInfoPanel() 
    {
        InfoPanel.SetActive(false);
    }
    public void ShowBag() 
    {
        this.gameObject.SetActive(true);
    }

    public void HideBag() 
    {
        this.gameObject.SetActive(false);
    }

    //加载背包数据
    void LoadObjectData()
    {
        //获取Model
        M_Bag mbag = GetModel<M_Bag>();
        //获取背包容量
        Capacity = mbag.MaxCapacity;
        //获取背包模型中的格子
        grid = mbag.m_Grid;
    }

    //显示并更新物品
    public void refreshBag() 
    {
        //加载背包数据
        LoadObjectData();

        //遍历背包视图中的格子 并存储
        GameObject[] P_Child = new GameObject[panel.transform.childCount];
        Debug.Log(grid.Count);
        Debug.Log(panel.transform.childCount);
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            P_Child[i] = panel.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < grid.Count; i++)
        {
            //1.渲染物品图片
            if (grid[i].IsEmpty==true)
                //如果该格子是空的 则跳过
                continue;
            ObjectInfo objinfo = Game.Instance.StaticData.GetObjectInfo(grid[i].objId);
            //获取物品图片路径
            string path = "file://" + Consts.G_objectImageDir + "/" + objinfo.objName + ".jpg";
            //获取UI中的Image
            Transform imgTran = P_Child[i].transform.Find("Image");
            Image im = imgTran.transform.GetComponent<Image>();
            //开启协程加载图片
            StartCoroutine(Tools.LoadImage(path, im));

            //2.渲染物品数量
            Transform TexTran = P_Child[i].transform.Find("count");
            int t = grid[i].Cur_Count;
            string str = Convert.ToString(t);
            TexTran.transform.GetComponent<Text>().text = str;
        }
    }
    #endregion

    #region unity回调
    #endregion
    public override void RegisterEvents() 
    {
        AttentionEvents.Add(Consts.E_AddObject);
    }
    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName) 
        {
            case Consts.E_AddObject:
                AddObjectArgs e0 = data as AddObjectArgs;
                //在背包中显示添加的物品

                break;
        }
    }

}
