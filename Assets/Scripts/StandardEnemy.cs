using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : MonoBehaviour {
    private Animator animator;
    public Patrol Patrol;
    public FireStateType Type = FireStateType.BLUE;
    public SkinnedMeshRenderer Renderer;
    public ParticleSystem Mist;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void Hit()
    {
        animator.SetTrigger("Die");
        Patrol.Stop = true;        
    }
}
