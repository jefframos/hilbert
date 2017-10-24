using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public PlayerController target;

    public float smoothTimeX = 0.005f;
    public float smoothTimeY = 0.005f;
    private Vector3 cameraVelocity;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(target == null)
        {
            return;
        }
        //Vector3 next = target.transform.position;
        //print(next);
        //next.y = 10;//transform.position.y;
        //transform.position = next;

        float posX = Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref cameraVelocity.x, smoothTimeX);
        float posZ = Mathf.SmoothDamp(transform.position.z, target.transform.position.z, ref cameraVelocity.z, smoothTimeY);

        transform.position = new Vector3(posX, transform.position.y, posZ);

        //Vector3 nextPos = target.transform.position;
        //nextPos.y = transform.position.y;
        ////nextPos += target.Velocity;// * Time.deltaTime;
        //transform.position = nextPos;
    }
}
