using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinoidLoopRotation : MonoBehaviour {

    public Vector3 Sin;
    public Vector3 Velocity;
    public Vector3 Distance;
    private Vector3 startPosition;
    //private float speed = 20f;
    public bool RandonStart = false;
    void Start()
    {
        if (RandonStart)
        {
            Sin = new Vector3(UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f), UnityEngine.Random.Range(0f, 1f));
        }
        startPosition = transform.localEulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.localEulerAngles;
        Sin += Velocity;
        Sin.x %= 360f;
        Sin.y %= 360f;
        Sin.z %= 360f;
        pos.x = startPosition.x + Mathf.Sin(Sin.x) * Distance.x;
        pos.y = startPosition.y + Mathf.Sin(Sin.y) * Distance.y;
        pos.z = startPosition.z + Mathf.Sin(Sin.z) * Distance.z;

        if (pos.x < 0) pos.x += 360f;
        if (pos.y < 0)
        {
            pos.y += 360f;
        }
        if (pos.z < 0) pos.z += 360f;
    }
}
