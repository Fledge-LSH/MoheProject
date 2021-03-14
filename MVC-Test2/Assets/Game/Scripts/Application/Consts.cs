using System;
using UnityEngine;

public class Consts
{

    //目录
    public static readonly string G_objectPath = "Prefabs";
    public static readonly string G_objectImageDir = Application.dataPath + @"\Game\Resources\Res\G_object";
    //Model
    public const string M_Player = "M_Player";
    public const string M_Bag = "M_Bag";

    //View
    public const string V_Bag = "V_Bag";

    //Controller;
    public const string E_AddObject = "E_AddObject";//AddObjectArgs
    public const string E_StartUp = "E_StartUp";
    public const string E_ShowInfoPanel = "E_ShowInfoPanel";//GridIndexArgs



    //物品类型

}
 public enum ObjectType 
{
    Eqiup,
    Mat,
    Drug
}