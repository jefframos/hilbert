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

    public TimeManager TimeManager;
    // Use this for initialization
    void Start()
    {
        velocity = new Vector2();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bool touched = Input.GetMouseButton(0);

        if (touched)
        {
            TimeManager.DoNormalSpeed();
        }
        else
        {
            TimeManager.DoSlowmotion();
        }
        print(velocity.x);
    }
    void FixedUpdate()
    {
        bool touched = Input.GetMouseButton(0);        

        if (touched)
        {

            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            if (Vector2.Distance(mousePosition, transform.position) < 0.5)
            {
                touched = false;
            }
        }

        if (touched)
        {           

            float tan = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);

            Vector2 nextVel = new Vector2()
            {
                x = Mathf.Cos(tan) * moveSpeed,
                y = Mathf.Sin(tan) * moveSpeed
            };

            //moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, acc);
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

        
        
        Vector2 nextPos = transform.position;
        nextPos += velocity;// * Time.deltaTime;
        transform.position = nextPos;

    }
}
