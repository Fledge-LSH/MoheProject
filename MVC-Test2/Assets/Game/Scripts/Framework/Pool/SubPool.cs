using System;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    //Transform m_parent;

    //名字标识
    public string Name 
    {
        get { return m_prefab.name; }
    }
    //预制体
    GameObject m_prefab;

    //集合
    List<GameObject> m_objects = new List<GameObject>();

    //构造
    public SubPool(GameObject prefab)
    {
        m_prefab = prefab;
    }

    //取对象
    public GameObject OnSpawn() 
    {
        GameObject go=null;
        foreach (GameObject obj in m_objects)
        {
            if (!obj.activeSelf) 
            {
                go = obj;
                break;
            }
        }

        if (go==null) 
        {
            go = GameObject.Instantiate<GameObject>(m_prefab);
            Debug.Log("执行到这里了"); 
            //go.transform.parent = m_parent;
            m_objects.Add(go);
        }

        go.SetActive(true);
        go.SendMessage("OnSpawn",SendMessageOptions.DontRequireReceiver);
        return go;
    }


    //回收对象
    public void Unspawn(GameObject go) 
    {
        if (Contains(go))
        {
            go.SendMessage("UnOnspawn", SendMessageOptions.DontRequireReceiver);
            go.SetActive(false);
        }
    }

    //回收所有对象
    public void UnOnspawnAll() 
    {
        foreach (GameObject obj in m_objects) 
        {
            if (obj.activeSelf)
                Unspawn(obj);
        }
    }

    //是否包含
    public bool Contains(GameObject go) 
    {
        return m_objects.Contains(go);
    }


}
