using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveToClickPoint : MonoBehaviour, IPointerDownHandler
{
    public Vector3 Velocity;
    public TimeManager TimeManager;
    public float Speed = 0.01f;
    public Transform Container;
    public Animator Animator;
    int tapCount = 0;
    void Start()
    {
        //agent = GetComponent<NavMeshAgent>();

        
        

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

        //if (Vector2.Distance(Input.mousePosition, transform.position) < 0.5)
        //{
        //    touched = false;
        //}

        if (touched)
        {
            
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //agent.destination = hit.point;
                float dis = Vector3.Distance(hit.point, transform.position);
                float tan = Mathf.Atan2(hit.point.z - Container.transform.position.z, hit.point.x - Container.transform.position.x);
                if (dis > 0.5f)
                {
                    float angle = tan * Mathf.Rad2Deg;
                    Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.down);
                    Container.transform.rotation = Quaternion.Lerp(Container.transform.rotation, targetRot, 0.1f); // Turns Right
                }
                if (dis < 1f)
                {
                    Velocity = Vector3.zero;

                    //TimeManager.DoSlowmotion();
                    return;
                }

                

                Vector3 nextVel = new Vector3()
                {
                    x = Mathf.Cos(tan) * Speed,
                    z = Mathf.Sin(tan) * Speed
                };

                Velocity = nextVel;

                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.up); //Turns Left

                //Vector3.forward

                
                Vector3 nextPos = transform.position;
                nextPos += Velocity;// * Time.deltaTime;
                transform.position = nextPos;

                //this.transform.position = hit.point;
            }

            //TimeManager.DoNormalSpeed();
        }
        else
        {
            Velocity = Vector3.zero;
            //TimeManager.DoSlowmotion();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new NotImplementedException();
        print(tapCount);
        TapActionMethod();
    }
}
