using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Move : MonoBehaviour {

    public int HPPoint;
    public int MoveRandom;

    public float MoveTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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

    public void Death()
    {
        gameObject.SetActive(false);
    }
}
