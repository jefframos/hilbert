using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinoidLoop : MonoBehaviour {

    public Vector3 Sin;
    public Vector3 Velocity;
    public Vector3 Distance;
    private Vector3 startPosition;
    public bool RandonStart = false;

    // Update is called once per frame
    void Start()
    {
    }
    void LateUpdate()
    {
        if(startPosition == Vector3.zero && transform.position != Vector3.zero)
        {
            if (RandonStart)
            {
                Sin = new Vector3(UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1));
            }

            startPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }
	void Update ()
    {
        if (startPosition == Vector3.zero)
        {
            return;
        }
        Vector3 pos = transform.position;
        Sin += Velocity;
        pos.x = startPosition.x + Mathf.Sin(Sin.x) * Distance.x;
        pos.y = startPosition.y + Mathf.Sin(Sin.y) * Distance.y;
        pos.z = startPosition.z + Mathf.Sin(Sin.z) * Distance.z;
        transform.position = pos;
    }
}
