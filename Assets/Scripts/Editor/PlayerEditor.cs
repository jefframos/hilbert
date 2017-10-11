using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerController))]

public class PlayerEditor : Editor
{

    PlayerController playerItem;
    ColorManager colorManager;

    public void Awake()
    {
        playerItem = target as PlayerController;

        colorManager = FindObjectOfType<ColorManager>();


        if (colorManager == null)
        {
            return;
        }

        if (playerItem.StatesData == null)
        {
            playerItem.StatesData = new List<PlayerController.EntityStateData>();
        }

        for (int i = 0; i < colorManager.ColorDataList.Count; i++)
        {
            ColorManager.ColorData cData = colorManager.ColorDataList[i];

            if (i >= playerItem.StatesData.Count)
            {
                playerItem.StatesData.Add(new PlayerController.EntityStateData
                {
                    FireStateType = cData.FireStateType,
                    TrailColor = cData.TrailColor,
                    LightColor = cData.LightColor
                });
            }
            else
            {
                playerItem.StatesData[i].FireStateType = cData.FireStateType;
                playerItem.StatesData[i].TrailColor = cData.TrailColor;
                playerItem.StatesData[i].LightColor = cData.LightColor;
            }

        }

    }

    void OnValidate()
    {
        //CountCoins();
    }
    override public void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorUtility.SetDirty(playerItem);
    }
}
