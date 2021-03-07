using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Player : Model
{

    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    string Player_name;
    int Hp;

    #endregion

    #region 属性
    //model标识
    public override string Name
    {
        get { return Consts.M_Player; }
    }
    //玩家名字
    public string Player_Name 
    {
        set 
        {
            Player_name = value;
        }
        get { return null; }
    }
    //HP
    public int HP 
    {
        set 
        {

        }
    }
    #endregion

    #region 方法
    public void Player(string player_name)
    {
        Player_name = player_name;
    }
    #endregion

    #region unity回调
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    #endregion
}
