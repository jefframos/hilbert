using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinoidLoopRotation : MonoBehaviour {

    public Vector3 Sin;
    public Vector3 Velocity;
    public Vector3 Distance;
    private Vector3 startPosition;
    public bool RandonStart = false;
    void Start()
    {
        if (RandonStart)
        {
            Sin = new Vector3(UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1));
        }
        startPosition = transform.eulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.eulerAngles;
        Sin += Velocity;
        pos.x = startPosition.x + Mathf.Sin(Sin.x) * Distance.x;
        pos.y = startPosition.y + Mathf.Sin(Sin.y) * Distance.y;
        pos.z = startPosition.z + Mathf.Sin(Sin.z) * Distance.z;
        transform.eulerAngles = pos;
    }
}
