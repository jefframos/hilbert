using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // Use this for initialization
    public float lifeSpan = 2f;
    public float secSpan = 2f;
    private float stLife = 0;
    public float intensity = 3f;
    private Quaternion rot;
    public bool toKill;
    public float speed = 0.01f;
    public Light Light;
    Vector3 velocity;
    private float targetIntensity;
    public ParticleSystem ParticleEmitter;
    void Start () {
        Light.intensity = 0;
        stLife = lifeSpan;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(rot == null)
        {
            return;
        }


        lifeSpan -= Time.deltaTime;

        //if(velocity == Vector3.zero)
        //{
        //    Light.intensity += Time.deltaTime * lifeSpan / 5f;
        //}
        //else {
        //Time.deltaTime * lifeSpan/ intensity;
        if(Light.intensity >= intensity)
        {
            Light.intensity = intensity;
            lifeSpan = 0;
        }
        if (lifeSpan <= 0)
        {

            Light.intensity -= 0.1f;// Time.deltaTime * lifeSpan / 5f;
            print(Light.intensity);
            if(Light.intensity <= 0)
            {
                secSpan -= Time.deltaTime;
                if (secSpan <= 0)
                {
                    toKill = true;
                }

            }
        }
        else
        {
            Light.intensity = (lifeSpan / stLife) * intensity;
        }
        //rot.eu
        //Vector3 nextVel = new Vector3()
        //{
        //    x = Mathf.Cos(-(rot.eulerAngles.y) * Mathf.Deg2Rad) * speed,
        //    z = Mathf.Sin(-(rot.eulerAngles.y) * Mathf.Deg2Rad) * speed
        //};
        //print(rot.eulerAngles);
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.up); //Turns Left

        //Vector3.forward
        Vector3 nextPos = transform.position;
        nextPos += velocity;// nextVel;// + velocity;// * Time.deltaTime;
        transform.position = nextPos;
    }
    void OnTriggerEnter(Collider item)
    {
        string layerName = LayerMask.LayerToName(item.gameObject.layer);

        if(layerName == "Environment")
        {
            velocity = Vector3.zero;
            ParticleEmitter.Stop();// = false;

        }
        print(layerName);
    }

    internal void SetDirection(Quaternion rotation)
    {
        rot = rotation;

        velocity = new Vector3()
        {
            x = Mathf.Cos(-(rot.eulerAngles.y) * Mathf.Deg2Rad) * speed,
            z = Mathf.Sin(-(rot.eulerAngles.y) * Mathf.Deg2Rad) * speed
        };
    }

    internal void SetSpeed(Vector3 _velocity)
    {
        //velocity = _velocity;
        //throw new NotImplementedException();
    }
}
