using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    public float lifeSpan = 2f;
    private Quaternion rot;
    public bool toKill;
    public float speed = 0.01f;
    public Light Light;
    Vector3 velocity;
    void Start () {
        Light.intensity = 0;

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(rot == null)
        {
            return;
        }


        lifeSpan -= Time.deltaTime;
        Light.intensity += Time.deltaTime * lifeSpan/ 5f;

        if (lifeSpan <= 0)
        {
            toKill = true;
        }
        //rot.eu
        Vector3 nextVel = new Vector3()
        {
            x = Mathf.Cos(-(rot.eulerAngles.y) * Mathf.Deg2Rad) * speed,
            z = Mathf.Sin(-(rot.eulerAngles.y) * Mathf.Deg2Rad) * speed
        };
        //print(rot.eulerAngles);
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.up); //Turns Left

        //Vector3.forward
        Vector3 nextPos = transform.position;
        nextPos += nextVel;// + velocity;// * Time.deltaTime;
        transform.position = nextPos;
    }

    internal void SetDirection(Quaternion rotation)
    {
        rot = rotation;
    }

    internal void SetSpeed(Vector3 _velocity)
    {
        velocity = _velocity;
        //throw new NotImplementedException();
    }
}
