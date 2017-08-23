using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour {

    private Vector3 mousePosition;
    private float currentRotation;
    public float moveSpeed = 0;
    public float maxSpeed = 0.1f;
    public float acc = 0.01f;
    public float rotationSpeed = 0.1f;
    private Vector2 velocity;
    private float stopTimer;
    public int side;
    // Use this for initialization
    void Start()
    {
        velocity = new Vector2();
    }

    // Update is called once per frame
    void Update()
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
            stopTimer = 1;
            side = 1;
            

            //print(mousePosition.x +" - "+ this.transform.position.x);

            //if (mousePosition.x < this.transform.position.x)
            //{
            //    side = -1;
            //}
            //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed/2);


            float tan = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
            
            Vector2 nextVel = new Vector2();
            nextVel.x = Mathf.Cos(tan) * moveSpeed;
            nextVel.y = Mathf.Sin(tan) * moveSpeed;


            moveSpeed = Mathf.Lerp(moveSpeed, maxSpeed, acc);

            velocity = nextVel;// Vector2.Lerp(velocity, nextVel, moveSpeed);




            //print(tan);
            //print(velocity);            
            print("ON");

            //currentRotation = transform.rotation.z;
            currentRotation += rotationSpeed * tan;

            //currentRotation += moveSpeed*2 * side;
            Quaternion rot = Quaternion.Euler(new Vector3(0, 0, currentRotation));
            transform.rotation = rot;// Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * speed);

            //transform.localRotation += moveSpeed;
            //print(transform.rotation);
            //print(currentRotation);
        }
        else
        {
            stopTimer = Mathf.Lerp(stopTimer, 0, acc*10000);
            print("OFF   " + stopTimer);
            velocity *= stopTimer;
        }

        Vector2 nextPos = transform.position;
        nextPos += velocity;
        transform.position = nextPos;

    }
}
