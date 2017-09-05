using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public MoveToClickPoint target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Vector3 next = target.transform.position;
        //print(next);
        //next.y = 10;//transform.position.y;
        //transform.position = next;

        Vector3 nextPos = target.transform.position;
        nextPos.y = transform.position.y;
        //nextPos += target.Velocity;// * Time.deltaTime;
        transform.position = nextPos;
    }
}
