using System;
using System.Collections.Generic;

public class V_Bag : View
{



    public override string Name 
    {
        get { return Consts.V_Bag; }
    }

    void LoadObject() 
    {
        

        M_Bag mbag = GetModel<M_Bag>();
        foreach (Grid g in mbag.m_Grid) 
        {
            foreach(ObjectInfo objInfo in StaticData.objectInfo.Values)
            {
                if (g.Gobj.ID==objInfo.ID)
                { 
                    //找到物品信息

                }
            }
        }
    }
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

    public void showInfoPanel() 
    {

    }
}
