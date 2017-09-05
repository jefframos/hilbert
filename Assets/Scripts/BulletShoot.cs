using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour {
    public GameObject BulletPrefab;
    private float timer = 0;
    public float BulletSpanTime = 3f;
    private List<Bullet> bulletList;
    public Transform SpawnPoint;
    public MoveToClickPoint player;
	// Use this for initialization
	void Start () {
        bulletList = new List<Bullet>();
        timer = BulletSpanTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }
	void FixedUpdate ()
    {
        //print("UPDATE");
        for (int i = bulletList.Count - 1; i >=0 ; i--)
        {
            if (bulletList[i].toKill)
            {
                //do something
                Destroy(bulletList[i].gameObject);
                bulletList.RemoveAt(i);
            }
        }

        

        if (player.Velocity != Vector3.zero)
        {
            if (timer > BulletSpanTime && player.Velocity != Vector3.zero)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        print("SHOOT");
        GameObject go = TransformUtils.InstantiateAndAdd(BulletPrefab);
        go.transform.position = SpawnPoint.position;
        Bullet bullet = go.GetComponent<Bullet>();
        bullet.SetDirection(transform.parent.rotation);
        bullet.SetSpeed(player.Velocity);
        bulletList.Add(bullet);
    }
}
