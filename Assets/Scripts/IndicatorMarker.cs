using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorMarker : MonoBehaviour {
    public ParticleSystem ParticleSystem;

    internal void Place(Color lightColor)
    {
        if (ParticleSystem == null)
        {
            ParticleSystem = GetComponentInChildren<ParticleSystem>();
        }
        ParticleSystem.Play();
        ParticleSystem.MainModule settings = ParticleSystem.main;
        settings.startColor = lightColor;
    }
}
