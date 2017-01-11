using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {

    public GameObject Player;
    public GameObject PoolDam;

    public GameObject[] Mob;

    public int Power;

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
        int MobNum = FindMob();
        if (MobNum >= 0)
        {
            int FinalPower = (int)Mathf.Round(Power * Random.Range(0.75f, 1.25f));
            PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[MobNum].transform.position.x, 0.75f, FinalPower);

            Mob[MobNum].GetComponent<Mob_Move>().HPPoint -= FinalPower;
            if (Mob[MobNum].GetComponent<Mob_Move>().HPPoint <= 0)
            {
                Mob[MobNum].GetComponent<Mob_Move>().Death();
            }
        }
    }

    public int FindMob()
    {
        int MobNum = -1;
        float MobRangeBank = -1.0f;
        for (int i = 0; i < Mob.Length; i++)
        {
            if (Mob[i].GetComponent<Mob_Move>().HPPoint > 0)
            {
                float MobFacing = Mob[i].transform.position.x - Player.transform.position.x;
                float MobRange = Vector2.Distance(Mob[i].transform.position, Player.transform.position);
                if (Facing && MobFacing > 0.0f || !Facing && MobFacing <= 0.0f)
                {
                    if (MobRange < 1.75 && (MobRangeBank == -1.0f || MobRangeBank > MobRange))
                    {
                        MobRangeBank = MobRange;
                        MobNum = i;
                    }
                }
            }
        }

        return MobNum;
    }
}
