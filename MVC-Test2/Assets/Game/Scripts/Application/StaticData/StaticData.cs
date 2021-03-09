using System;
using System.Collections.Generic;

public class StaticData:Singleton<StaticData>
{
    public static Dictionary<int,ObjectInfo> objectInfo = new Dictionary<int,ObjectInfo>();

    protected override void Awake()
    {
        base.Awake();
        ObjectInfoInit();
    }

    //初始化物品信息
    void ObjectInfoInit() 
    {
        objectInfo.Add(0, new ObjectInfo() { ID = 0, info = "这是一本魔法书", imgName="book" });
        objectInfo.Add(1, new ObjectInfo() { ID = 1, info = "这是一瓶药水", imgName = "drug" });
        objectInfo.Add(2, new ObjectInfo() { ID = 2, info = "这是一个钥匙", imgName = "key" });
        objectInfo.Add(3, new ObjectInfo() { ID = 3, info = "这是一张地图", imgName = "map" });
    }
}
