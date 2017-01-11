﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamText_Move : MonoBehaviour {

    public float LifeTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (LifeTime > 0.0f)
        {
            LifeTime -= Time.deltaTime;
            if (LifeTime <= 0.0f)
            {
                gameObject.SetActive(false);
            }
        }
	}

    public void DamStart(int Power)
    {
        LifeTime = 0.75f;
        gameObject.GetComponent<TextMesh>().text = Power.ToString();
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.5f);
    }
}
