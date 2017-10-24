using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour {
    private Animator animator;
    public Patrol Patrol;
    public FireStateType CurrentStateType = FireStateType.BLUE;
    public SkinnedMeshRenderer Renderer;
    public ParticleSystem Mist;

    [Serializable]
    public class EntityStateData
    {

        public FireStateType FireStateType;
        public Material Material;
        public Color LightColor;
    }
    public List<EntityStateData> StatesData;
    public EntityStateData CurrentStateData;
    private ChangeStateTrigger lastTrigger;

    public bool isAlive;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        Reset();
    }
    void Reset()
    {
        UpdateStateContainers();
        Patrol.Enable();
        isAlive = true;
    }

    private void UpdateStateContainers()
    {
        for (int i = 0; i < StatesData.Count; i++)
        {
            if (StatesData[i].FireStateType == CurrentStateType)
            {
                CurrentStateData = StatesData[i];

                Renderer.material = CurrentStateData.Material;
                print(CurrentStateData.Material);
                //Renderer.up
                ParticleSystem.MainModule settings = Mist.main;
                settings.startColor = CurrentStateData.LightColor;
                Mist.Stop();
                Mist.Clear();
                Mist.Play();
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    internal void Hit()
    {
        animator.SetTrigger("Die");
        Patrol.Stop = true;
        Patrol.Disable();
        isAlive = false;
    }

    void OnTriggerEnter(Collider item)
    {
        string layerName = LayerMask.LayerToName(item.gameObject.layer);
        

        if (layerName == "ChangeStateTrigger")
        {
            ChangeStateTrigger tempTrigger = item.GetComponent<ChangeStateTrigger>();
            if(lastTrigger && lastTrigger == tempTrigger)
            {
                return;
            }

            if (tempTrigger != null)
            {
                lastTrigger = tempTrigger;
                if(CurrentStateType == tempTrigger.StateType)
                {
                    return;
                }
                CurrentStateType = tempTrigger.StateType;
                UpdateStateContainers();
            }


        }

    }
}
