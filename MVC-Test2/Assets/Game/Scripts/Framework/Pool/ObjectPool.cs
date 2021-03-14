using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool:Singleton<ObjectPool>
{
    //资源目录
    public string ResourceDir = "Prefabs/G_objects";

    //集合
    Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

    //取对象
    public GameObject OnSpawn(string name) 
    {
        if (!m_pools.ContainsKey(name)) 
        {
            RegisterNew(name);
        }

        SubPool pool = m_pools[name];
        return pool.OnSpawn();
    }
    //回收对象
    public void UnOnspwan(GameObject go) 
    {
        SubPool pool = null;
        foreach (SubPool p in m_pools.Values) 
        {
            if (p.Contains(go)) 
            {
                pool = p;
                break;
            }
        }

        pool.Unspawn(go);
    }

    //回收所有对象
    void UnOnspawnAll() 
    {
        foreach (SubPool pool in m_pools.Values) 
            pool.UnOnspawnAll();
    }

    //创建新子池子
    void RegisterNew(string name) 
    {
        //预设路径
        string path = null;
        if (string.IsNullOrEmpty(ResourceDir.Trim()))
            path = name;
        else 
            path = ResourceDir + "/" + name;

        Debug.Log(path);

        //加载预设
        GameObject go = Resources.Load<GameObject>(path);

        //创建子对象池
        SubPool pool = new SubPool(go);
        m_pools.Add(pool.Name,pool);
    }       
}
