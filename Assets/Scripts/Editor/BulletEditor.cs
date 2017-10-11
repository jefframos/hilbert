using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Bullet))]

    public class BulletEditor : Editor {

    Bullet bulletItem;
    ColorManager colorManager;

    public void Awake()
    {
        bulletItem = target as Bullet;

        colorManager = FindObjectOfType<ColorManager>();
        

        if(colorManager == null)
        {
            return;
        }

        //bulletItem.StatesData = new List<Bullet.StateData>();
        if(bulletItem.StatesData == null)
        {
            bulletItem.StatesData = new List<Bullet.EntityStateData>();
        }
        for (int i = 0; i < colorManager.ColorDataList.Count; i++)
        {
            ColorManager.ColorData cData = colorManager.ColorDataList[i];

            if (i >= bulletItem.StatesData.Count)
            {
                bulletItem.StatesData.Add(new Bullet.EntityStateData
                {
                    FireStateType = cData.FireStateType,
                    TrailColor = cData.TrailColor,
                    LightColor = cData.LightColor
                });
            }
            else
            {
                bulletItem.StatesData[i].FireStateType = cData.FireStateType;
                bulletItem.StatesData[i].TrailColor = cData.TrailColor;
                bulletItem.StatesData[i].LightColor = cData.LightColor;
            }
            
        }

        Debug.Log(bulletItem.StatesData.Count);
        
    }

    void OnValidate()
    {
        //CountCoins();
    }
    override public void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorUtility.SetDirty(bulletItem);
    }
}
