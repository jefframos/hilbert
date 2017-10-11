using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ChangeStateTrigger))]

public class ChangeStateTriggerEditor : Editor
{

    ChangeStateTrigger stateTrigger;
    ColorManager colorManager;
     

    // Use this for initialization
    void Awake () {
        stateTrigger = target as ChangeStateTrigger;
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

        for (int i = 0; i < colorManager.ColorDataList.Count; i++)
        {
            ColorManager.ColorData cData = colorManager.ColorDataList[i];

            if (cData.FireStateType == stateTrigger.StateType)
            {
                for (int j = 0; j < stateTrigger.PilarMaterials.Length; j++)
                {
                    //stateTrigger.PilarMaterials[j].material = cData.TriggerWallMaterial;
                }
                
                ParticleSystem.MainModule settings = stateTrigger.ParticleSystem.main;
                settings.startColor = cData.LightColor;// new ParticleSystem.MinMaxGradient(cData.LightColor);

            }
        }
    }
}
