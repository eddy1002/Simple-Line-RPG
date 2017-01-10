using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public GameObject Player;
    public GameObject PoolDam;

    public GameObject Mob;

    public bool Attacking;
    public bool LWalking;
    public bool RWalking;
    public bool Facing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AttackStart()
    {
        Player.GetComponent<Animator>().SetFloat("Attack", 1);
    }

    public void AttackEnd()
    {
        Player.GetComponent<Animator>().SetFloat("Attack", 0);
    }

    public void MoveLeftStart()
    {
        LWalking = true;
        Facing = false;
        Player.GetComponent<Transform>().localScale = new Vector3(1.0f, 1.0f, 1.0f);
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f, 0.0f);
    }

    public void MoveRightStart()
    {
        RWalking = true;
        Facing = true;
        Player.GetComponent<Transform>().localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f, 0.0f);
    }

    public void MoveLeftEnd()
    {
        LWalking = false;
        if(!LWalking && !RWalking)
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }

    public void MoveRightEnd()
    {
        RWalking = false;
        if (!LWalking && !RWalking)
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);
    }

    public void Attack()
    {
        if (Vector2.Distance(Mob.transform.position, Player.transform.position) < 1.75)
        {
            if (Facing && Mob.transform.position.x - Player.transform.position.x > 0 || !Facing && Mob.transform.position.x - Player.transform.position.x < 0)
            {
                PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob.transform.position.x, 0.75f);
            }
        }
    }
}
