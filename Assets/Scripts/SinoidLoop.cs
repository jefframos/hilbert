using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinoidLoop : MonoBehaviour
{

    public Vector3 Sin;
    public Vector3 Velocity;
    public Vector3 Distance;
    private Vector3 startPosition;
    public bool RandonStart = false;

    // Update is called once per frame
    void Awake()
    {
        startPosition = Vector3.negativeInfinity;
    }
    void LateUpdate()
    {
        if (startPosition.x == float.NegativeInfinity)
        {
            if (RandonStart)
            {
                Sin = new Vector3(UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, 1));
            }

            startPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
    }
    void Update()
    {
        if (startPosition.x == float.NegativeInfinity)
        {
            return;
        }
        Vector3 pos = transform.localPosition;
        Sin += Velocity;

        Sin.x %= 360f;
        Sin.y %= 360f;
        Sin.z %= 360f;

        pos.x = startPosition.x + Mathf.Sin(Sin.x) * Distance.x;
        pos.y = startPosition.y + Mathf.Sin(Sin.y) * Distance.y;
        pos.z = startPosition.z + Mathf.Sin(Sin.z) * Distance.z;
        transform.localPosition = pos;
        //transform.localPosition
    }
}
