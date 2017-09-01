using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {

    private Vector3 mousePosition;
    //private float currentRotation;
    public float moveSpeed = 0.5f;
    //public float maxSpeed = 0.1f;
    //public float acc = 0.01f;
    //public float rotationSpeed = 0.1f;
    private Vector2 velocity;

    //public TimeManager TimeManager;
    // Use this for initialization
    void Start()
    {
        velocity = new Vector2();
        //Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool touched = Input.GetMouseButton(0);

        if (touched)
        {
            //TimeManager.DoNormalSpeed();
        }
        else
        {
           // TimeManager.DoSlowmotion();
        }
        CastRayToWorld();
        //print(velocity.x);
    }
    void CastRayToWorld()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * Camera.main.transform.position.z);
        
    }
    void FixedUpdate()
    {
        bool touched = Input.GetMouseButton(0);        

        if (touched)
        {

            mousePosition = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 point  = ray.origin + (ray.direction * Camera.main.transform.position.z);

            mousePosition = point;//Camera.main.ScreenToWorldPoint(mousePosition);

            if (Vector2.Distance(mousePosition, transform.position) < 0.5)
            {
                touched = false;
            }
        }

        if (touched)
        {           

            float tan = Mathf.Atan2(mousePosition.z - transform.position.z, mousePosition.x - transform.position.x);

            Vector3 nextVel = new Vector3()
            {
                x = Mathf.Cos(tan) * moveSpeed,
                z = Mathf.Sin(tan) * moveSpeed
            };

            //moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, acc);


            Debug.Log("World point " + nextVel.x +" - "+ nextVel.y + " - " + nextVel.z);

            velocity = nextVel;

            


            //print(nextVel);

            //if (tan < 0)
            //{
            //    tan += Mathf.PI;
            //}
            //currentRotation += rotationSpeed * tan;
            //Quaternion rot = Quaternion.Euler(new Vector3(0, 0, currentRotation));
            //transform.rotation = rot;

            

        }
        else
        {

            
        }

        

        //Vector2 nextPos = transform.position;
        //nextPos += velocity;// * Time.deltaTime;
        //transform.position = nextPos;

    }
}
