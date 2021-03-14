using System;
using System.Collections.Generic;
using UnityEngine;

public class StaticData:Singleton<StaticData>
{
    public static Dictionary<int ,ObjectInfo> objectInfo = new Dictionary<int,ObjectInfo>();

    protected override void Awake()
    {
        base.Awake();
        ObjectInfoInit();
    }

    //初始化物品信息
    public void ObjectInfoInit() 
    {
        //读表
        Tools.LoadObjectInfo();

        //objectInfo.Add(0, new ObjectInfo() { ID = 0, objName = "book" , info = "这是一本魔法书", IconName="book" });
        //objectInfo.Add(1, new ObjectInfo() { ID = 1, objName = "drug"   , info = "这是一瓶药水", IconName = "drug" });
        //objectInfo.Add(2, new ObjectInfo() { ID = 2, objName = "key"   , info = "这是一个钥匙", IconName = "key" });
        //objectInfo.Add(3, new ObjectInfo() { ID = 3, objName = "map"   , info = "这是一张地图", IconName = "map" });
    }

    //获取物体信息
    public ObjectInfo GetObjectInfo(int id) 
    {
        ObjectInfo objInfo;
        //遍历物品信息字典
        foreach (ObjectInfo info in StaticData.objectInfo.Values)
        {
            if (info.ID == id)
            {
                objInfo = info;
                return objInfo;
            }
        }
        return null;
    }


}
