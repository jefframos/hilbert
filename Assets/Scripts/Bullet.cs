using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    public float lifeSpan = 6f;
    private float currentLifeSpan = 0;
    public float intensity = 3f;
    //private Quaternion rot;
    public bool toKill;

    public Rigidbody rb;
    public float thrust = 0.001f;

    public int Bounces = 1;
    private int currentBounce = 1;

    public float speed = 0.01f;
    //private Vector3 velocity;
    
    [Serializable]
    public class EntityStateData
    {

        public FireStateType FireStateType;
        public Gradient TrailColor;
        public Color LightColor;
    }

    public List<EntityStateData> StatesData;
    public EntityStateData CurrentStateData;

    private int currentState;

    public GameObject DefaultContainer;

    public TrailRenderer Trail;
    public Light ParticleLight;

    public GameObject ExplosionContainer;
    public ParticleSystem[] ExplosionParticles;
    public ParticleSystem TrailParticle;

    public FireStateType CurrentStateType;

    public void Update()
    {
        //currentLifeSpan -= Time.deltaTime;
        //if(currentLifeSpan < 0)
        //{
        //    toKill = true;
        //}
    }
    public void Reset()
    {
        //CurrentStateType = FireStateType.BLUE;
        UpdateStateContainers();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        toKill = false;
        currentLifeSpan = lifeSpan;
        currentBounce = Bounces;
        TrailParticle.Play();
    }

    private void ChangeState()
    {
        currentState++;
        if(currentState >= StatesData.Count)
        {
            currentState = 0;
        }

        CurrentStateType = StatesData[currentState].FireStateType;
        UpdateStateContainers();
    }
    private void UpdateStateContainers()
    {
        DefaultContainer.SetActive(true);
        ExplosionContainer.SetActive(false);
        for (int i = 0; i < StatesData.Count; i++)
        {
            if(StatesData[i].FireStateType == CurrentStateType)
            {
                currentState = i;
                CurrentStateData = StatesData[i];
                Trail.colorGradient = CurrentStateData.TrailColor;
                
                DOTween.To(() => Trail.startColor, x => Trail.startColor = x, CurrentStateData.LightColor, 1f).SetUpdate(true);
                DOTween.To(() => ParticleLight.color, x => ParticleLight.color = x, CurrentStateData.LightColor, 1f).SetUpdate(true);

                //Trail.startColor = CurrentStateData.LightColor;
                //ParticleLight.color = CurrentStateData.LightColor;
            }
        }
    }

    void OnCollisionEnter(Collision item)
    {
        string layerName = LayerMask.LayerToName(item.gameObject.layer);

        if (layerName == "Environment")
        {


            currentBounce--;

            if (currentBounce <= 0)
            {
                Explode();
            }
            else
            {
                //ChangeState();
            }

            print("OnCollisionEnter" + currentBounce);

        }

    }

    private void Explode()
    {

        ExplosionParticles = ExplosionContainer.GetComponentsInChildren<ParticleSystem>();

        for (int j = 0; j < ExplosionParticles.Length; j++)
        {
            ParticleSystem.MainModule settings = ExplosionParticles[j].main;
            Color tempColor = CurrentStateData.LightColor;
            tempColor.a = 0.5f;
            settings.startColor = CurrentStateData.TrailColor;// new ParticleSystem.MinMaxGradient(cData.LightColor);
        }

        TrailParticle.Stop();

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        ExplosionContainer.SetActive(true);
        Invoke("ToKill", 4);
    }

    void OnTriggerEnter(Collider item)
    {
        string layerName = LayerMask.LayerToName(item.gameObject.layer);

        print(layerName);

        if (layerName == "ChangeStateTrigger")
        {
            ChangeStateTrigger tempTrigger = item.GetComponent<ChangeStateTrigger>();
            if(tempTrigger != null)
            {
                CurrentStateType = tempTrigger.StateType;
                UpdateStateContainers();
            }
            

        }

        if (layerName == "Enemies")
        {
            print("Enemy");
            StandardEnemy standardEnemy = item.GetComponent<StandardEnemy>();
            if (standardEnemy != null)
            {
                if(standardEnemy.Type == CurrentStateType)
                {
                    standardEnemy.Hit();
                }
                else
                {
                    //Explode();
                }
                Explode();

            }


        }

    }

    internal void SetDirection(Quaternion rotation)
    {
        //rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.ResetCenterOfMass();
        rb.ResetInertiaTensor();
        rb.AddForce(transform.forward * thrust);


        //rot = rotation;

        //velocity = new Vector3()
        //{
        //    x = Mathf.Cos(-(rot.eulerAngles.y) * Mathf.Deg2Rad) * speed,
        //    z = Mathf.Sin(-(rot.eulerAngles.y) * Mathf.Deg2Rad) * speed
        //};
    }
    void ToKill()
    {
        toKill = true;
    }

}
