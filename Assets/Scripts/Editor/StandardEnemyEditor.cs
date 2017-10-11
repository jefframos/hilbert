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
        Debug.Log("VALIDATE");
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (colorManager == null)
        {
            return;
        }
        Debug.Log("UPDATEEEEE");
        for (int i = 0; i < colorManager.ColorDataList.Count; i++)
        {
            ColorManager.ColorData cData = colorManager.ColorDataList[i];

            if (cData.FireStateType == standardEnemy.Type)
            {
                standardEnemy.Renderer.material = cData.GhostMaterial;
                //for (int j = 0; j < standardEnemy.PilarMaterials.Length; j++)
                //{
                //    //stateTrigger.PilarMaterials[j].material = cData.TriggerWallMaterial;
                //}

                ParticleSystem.MainModule settings = standardEnemy.Mist.main;
                settings.startColor = cData.LightColor;// new ParticleSystem.MinMaxGradient(cData.LightColor);

            }
        }
    }
}
