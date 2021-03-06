using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(StaticData))]
[RequireComponent(typeof(ObjectPool))]
public class Game:ApplicationBase<Game>
{
    //全局访问功能
    [HideInInspector] public StaticData StaticData = null;//静态数据
    [HideInInspector] public ObjectPool ObjectPool = null;//静态数据

    private void Start()
    {
        //全局单例赋值
        StaticData = StaticData.Instance;
        ObjectPool = ObjectPool.Instance;

        Debug.Log("对象池"+ObjectPool.name);


        //注册启动命令
        RegisterController(Consts.E_StartUp, typeof(StartUpCommand));
        SendEvent(Consts.E_StartUp);

    }
}
