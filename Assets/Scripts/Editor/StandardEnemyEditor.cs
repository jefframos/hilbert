using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(StandardEnemy))]

public class StandardEnemyEditor : Editor
{

    StandardEnemy standardEnemy;
    ColorManager colorManager;


    // Use this for initialization
    void Awake()
    {
        standardEnemy = target as StandardEnemy;
        colorManager = FindObjectOfType<ColorManager>();
        UpdateColor();
    }
    void OnChange()
    {
        UpdateColor();
    }

    void OnValidate()
    {
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (colorManager == null)
        {
            return;
        }

        if (standardEnemy.StatesData == null)
        {
            standardEnemy.StatesData = new List<StandardEnemy.EntityStateData>();
        }

        for (int i = 0; i < colorManager.ColorDataList.Count; i++)
        {
            ColorManager.ColorData cData = colorManager.ColorDataList[i];

            if (i >= standardEnemy.StatesData.Count)
            {
                standardEnemy.StatesData.Add(new StandardEnemy.EntityStateData
                {
                    FireStateType = cData.FireStateType,
                    Material = cData.GhostMaterial,
                    LightColor = cData.LightColor
                });
            }
            else
            {
                standardEnemy.StatesData[i].FireStateType = cData.FireStateType;
                standardEnemy.StatesData[i].Material = cData.GhostMaterial;
                standardEnemy.StatesData[i].LightColor = cData.LightColor;
            }

            if (cData.FireStateType == standardEnemy.CurrentStateType)
            {
                standardEnemy.Renderer.material = cData.GhostMaterial;
                ParticleSystem.MainModule settings = standardEnemy.Mist.main;
                settings.startColor = cData.LightColor;// new ParticleSystem.MinMaxGradient(cData.LightColor);

            }

        }


        //for (int i = 0; i < colorManager.ColorDataList.Count; i++)
        //{
        //    ColorManager.ColorData cData = colorManager.ColorDataList[i];

        //    if (i >= playerItem.StatesData.Count)
        //    {


        //        if (cData.FireStateType == standardEnemy.Type)
        //    {
        //        standardEnemy.Renderer.material = cData.GhostMaterial;
        //        //for (int j = 0; j < standardEnemy.PilarMaterials.Length; j++)
        //        //{
        //        //    //stateTrigger.PilarMaterials[j].material = cData.TriggerWallMaterial;
        //        //}

        //        ParticleSystem.MainModule settings = standardEnemy.Mist.main;
        //        settings.startColor = cData.LightColor;// new ParticleSystem.MinMaxGradient(cData.LightColor);

        //    }
        //}
    }
}
