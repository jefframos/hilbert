using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{    
    public float speed = 20f;
    public Transform target;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    
        //transform.localEulerAngles = pos;
        float step = speed * Time.deltaTime;
        transform.RotateAround(target.transform.position, Vector3.up, step);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, target.localRotation, step);
    }
}
