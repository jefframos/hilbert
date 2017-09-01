using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour {
    int dir = 1;
    float timer = 0;
    public float spd = 0.0001f;
    private Vector2 velocity;

    // Use this for initialization
    void Start () {
        timer = Random.Range(0, 4);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        print("Q");
        timer -= (1f / 2) * Time.unscaledDeltaTime;
        //print(Time.fixedDeltaTime);

        if (timer < 0)
        {
            //print("CHANGE");
            dir = -dir;
            timer = 1.5f;
        }
        velocity = new Vector2(dir * spd, 0);

        Vector2 nextPos = transform.position;
        nextPos.x += velocity.x;// * Time.unscaledDeltaTime;
        transform.position = nextPos;
    }
}
