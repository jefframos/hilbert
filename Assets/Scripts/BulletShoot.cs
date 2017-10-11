using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour {
    public GameObject BulletPrefab;
    private float timer = 0;
    public float BulletSpanTime = 3f;
    private List<Bullet> bulletList;
    private List<Bullet> bulletPool;
    public Transform SpawnPoint;
    public PlayerController player;
    private bool ableToShoot = false;
   
	// Use this for initialization
	void Start () {
        bulletList = new List<Bullet>();
        bulletPool = new List<Bullet>();
        player = GetComponentInParent<PlayerController>();
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
                bulletPool.Add(bulletList[i]);
                bulletList[i].gameObject.SetActive(false);
                bulletList.RemoveAt(i);
            }
        }

        if (timer > BulletSpanTime)
        {
            
            ableToShoot = true;
            //Shoot();
        }
    }

    public void Shoot()
    {
        if (!ableToShoot)
        {
            return;
        }
        ableToShoot = false;
        Bullet bullet;
        if (bulletPool.Count > 0)
        {
            bullet = bulletPool[0];
            bulletPool.RemoveAt(0);
        }
        else
        {
            GameObject go = TransformUtils.InstantiateAndAdd(BulletPrefab);
            bullet = go.GetComponent<Bullet>();
        }

        bullet.transform.position = SpawnPoint.position;
        Quaternion rot = transform.parent.parent.rotation;
        Vector3 euler = rot.eulerAngles;
        euler.y += 90;
        rot.eulerAngles = euler;
        bullet.transform.rotation = rot;// transform.parent.parent.rotation + Quaternion.Euler(new Vector3(0, 90, 0));

        //bullet.SetDir(transform.forward);
        bullet.CurrentStateType = player.CurrentStateType;
        bullet.Reset();

        bullet.gameObject.SetActive(true);
        bullet.SetDirection(transform.parent.rotation);
        //bullet.SetSpeed(player.Velocity);
        bulletList.Add(bullet);
        timer = 0;
    }
}
