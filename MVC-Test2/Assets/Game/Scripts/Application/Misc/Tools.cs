using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;


public class Tools
{

    //读取物品信息
    public static void LoadObjectInfo() 
    {
        TextAsset objectInfoListText;//新建TextAsset变量
        objectInfoListText = Resources.Load("ObjectInfo")as TextAsset;
        string text = objectInfoListText.text;
        string[] objInfoArray = text.Split('\n');//以\n为分割符将文本分割为一个数组
        foreach (string str in objInfoArray) //遍历每一行并将每一个物品信息放入到类中
        {
            ObjectInfo info = new ObjectInfo();
            string[] propertyArray = str.Split('\t');//通过Tab键分割
            Debug.Log(propertyArray[0]);
            int id = int.Parse(propertyArray[0]);
            string iconNamge = propertyArray[1];
            string Name = propertyArray[2];
            string str_type = propertyArray[3];
            string baseInfo = propertyArray[4];

            ObjectType type=ObjectType.Drug;

            switch (str_type) 
            {
                case "Eqiup":
                    type = ObjectType.Eqiup;
                    break;
                case "Mat":
                    type = ObjectType.Mat;
                    break;
                case "Drug":
                    type = ObjectType.Drug;
                    break;
            }

            info.ID = id;
            info.IconName = iconNamge;
            info.objName = Name;
            info.type = type;
            info.info = baseInfo;

            if (type==ObjectType.Drug) 
            {
                int hp = int.Parse(propertyArray[7]);

                info.Hp = hp;
            }

            StaticData.objectInfo.Add(info.ID,info);
        }
    }

    //加载图片
    public static IEnumerator LoadImage(string url, SpriteRenderer render)
    {
        WWW www = new WWW(url);

        while (!www.isDone)
            yield return www;

        Texture2D texture = www.texture;
        Sprite sp = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        render.sprite = sp;
    }

    public static IEnumerator LoadImage(string url, Image image)
    {
        WWW www = new WWW(url);

        while (!www.isDone)
            yield return www;

        Texture2D texture = www.texture;
        Sprite sp = Sprite.Create(
            texture,
            new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        image.sprite = sp;
    }
}