using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {
    [Serializable]
    public class ColorData
    {
        public FireStateType FireStateType;
        public Gradient TrailColor;
        public Color LightColor;
        public Color TriggerColor;
        public Color GhostColor;
        public Material GhostMaterial;
        public Material TriggerWallMaterial;
    }
    public List<ColorData> ColorDataList;
}
