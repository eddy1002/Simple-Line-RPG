using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Move : MonoBehaviour {

    public int HPPoint;
    public int MoveRandom;

    public float MoveTime;
    public float NuckLeft;
    public float NuckRight;
    public float MadLeft;
    public float MadRight;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (NuckLeft <= 0.0f && NuckRight <= 0.0f && MadLeft <= 0.0f && MadRight <= 0.0f)
        {
            if (MoveTime <= 0.0f)
            {
                MoveTime = Random.Range(2.0f, 4.0f);
                MoveRandom = Random.Range(0, 3);
                if (MoveRandom == 0)
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                else if (MoveRandom == 1)
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, 0.0f);
                    gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
                else
                {
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0.0f);
                    gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                }
            }
            else
            {
                MoveTime -= Time.deltaTime;
                if (MoveRandom == 1 && gameObject.transform.position.x > 7.0f || MoveRandom == 2 && gameObject.transform.position.x < -7.0f)
                    gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
        else if (NuckLeft > 0.0f || NuckRight > 0.0f)
        {
            if (NuckLeft > 0.0f)
                NuckLeft -= Time.deltaTime;
            if (NuckRight > 0.0f)
                NuckRight -= Time.deltaTime;
            if (NuckLeft <= 0.0f && NuckRight <= 0.0f)
            {
                if (MadLeft > 0.0f)
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, 0.0f);
                if (MadRight > 0.0f)
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0.0f);
            }
        }
        else
        {
            if (MadLeft > 0.0f)
                MadLeft -= Time.deltaTime;
            if (MadRight > 0.0f)
                MadRight -= Time.deltaTime;
        }
	}

    public void Death()
    {
        gameObject.SetActive(false);
    }
    
    public void MobSetting()
    {
        HPPoint = 50;

        MoveTime = 0.0f;
        NuckLeft = 0.0f;
        NuckRight = 0.0f;
        MadLeft = 0.0f;
        MadRight = 0.0f;

        float BornRandom = Random.value;
        if (BornRandom >= 0.5f)
            gameObject.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else
            gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }
}
