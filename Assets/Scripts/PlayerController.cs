using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerDownHandler
{
    public float acceleration = 2f;
    public float deceleration = 60f;
    public float closeEnoughMeters = 4f;

    [Serializable]
    public class EntityStateData
    {

        public FireStateType FireStateType;
        public Gradient TrailColor;
        public Color LightColor;
    }

    public List<EntityStateData> StatesData;
    public EntityStateData CurrentStateData;

    public Transform Container;
    public Animator Animator;
    int tapCount = 0;
    float touchCounter;
    public NavMeshAgent Agent;
    public BulletShoot BulletShot;

    public Light[] Lights;

    public FireStateType CurrentStateType = FireStateType.BLUE;

    public IndicatorMarker Indicator;
    void Start()
    {
        Lights = GetComponentsInChildren<Light>();
        Reset();
    }

    public void Reset()
    {
        UpdateStateContainers();
        
    }

    private void UpdateStateContainers()
    {
        for (int i = 0; i < StatesData.Count; i++)
        {
            if (StatesData[i].FireStateType == CurrentStateType)
            {
                print(CurrentStateType);
                print(Lights.Length);
                CurrentStateData = StatesData[i];


                for (int j = 0; j < Lights.Length; j++)
                {
                    Lights[j].color = CurrentStateData.LightColor;
                    //DOTween.To(() => Lights[j].color, x => Lights[j].color = x, CurrentStateData.LightColor, 1f).SetUpdate(true);
                }
            }
        }
    }

    public void TapActionMethod()
    {
        
        if (tapCount >= 2)
            return;
        if (tapCount <= 0)
            Invoke("TapCheck", 0.3f);
        tapCount++;
    }

    void TapCheck()
    {
        switch (tapCount)
        {
            case 1:
                Debug.Log("single tap");
                break;
            case 2:
                Debug.Log("Double tap");
                break;
        }
        tapCount = 0;
    }

    void Update()
    {
        bool touched = Input.GetMouseButton(0);
        if(Input.mousePosition != null)
        {
            Rotate();
        }
        

        if (touched)
        {
            
            touchCounter += Time.deltaTime;
            if(touchCounter > 0.3f)
            {
                Shoot();
            }
        }
        else
        {

            if(touchCounter > 0 && touchCounter < 0.3f)
            {
                GoToPosition();
            }
            touchCounter = 0;
        }

        if (Agent)
        {

            if (!Agent.pathPending)
            {
                if(Indicator.transform.position != Agent.pathEndPosition)
                {
                    Indicator.transform.position = Agent.pathEndPosition;
                    Indicator.Place(CurrentStateData.LightColor);
                }
               
            }

        }
    }

    private void Rotate()
    {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100);

        float dis = Vector3.Distance(hit.point, transform.position);
        float tan = Mathf.Atan2(hit.point.z - Container.transform.position.z, hit.point.x - Container.transform.position.x);
        tan += Mathf.PI;
        float angle = tan * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.down);

        Container.transform.rotation = Quaternion.Lerp(Container.transform.rotation, targetRot, 0.2f);
    }
    private void Shoot()
    {

        Rotate();
        BulletShot.Shoot();
    }

    private void GoToPosition()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Agent.SetDestination(hit.point);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        TapActionMethod();
    }


    void OnCollisionEnter(Collision item)
    {
        string layerName = LayerMask.LayerToName(item.gameObject.layer);
        print("OnCollisionEnter");
        if (layerName == "Environment")
        {


           

        }

    }

    void OnTriggerEnter(Collider item)
    {
        string layerName = LayerMask.LayerToName(item.gameObject.layer);


        print(layerName);


        if (layerName == "ChangeStateTrigger")
        {
            ChangeStateTrigger tempTrigger = item.GetComponent<ChangeStateTrigger>();
            if (tempTrigger != null)
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
                if (standardEnemy.Type == CurrentStateType)
                {
                    standardEnemy.Hit();
                }
                else
                {
                    //Explode();
                }
                //Explode();

            }


        }

    }
}
