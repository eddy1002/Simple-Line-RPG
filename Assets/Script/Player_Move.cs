using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour {

    public GameObject Player;
    public GameObject PoolDam;

    public Slider Sld_HPPoint;
    public Slider Sld_ExPoint;

    public Text Txt_Level;
    public Text Txt_Power;
    public Text Txt_Money;

    public GameObject[] Mob;

    public int Level;
    public int Power;
    public int Money;

    public bool Attacking;
    public bool LWalking;
    public bool RWalking;
    public bool Facing;

    public float ExPoint;
    public float ExMax;

    public float[] MobExp;

    public Color[] Colors;

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
            PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[MobNum].transform.position.x, 0.75f, FinalPower.ToString(), Colors[0]);

            Mob[MobNum].GetComponent<Mob_Move>().HPPoint -= FinalPower;
            if (Mob[MobNum].GetComponent<Mob_Move>().HPPoint <= 0)
            {
                Mob[MobNum].GetComponent<Mob_Move>().Death();
                MobKill(0);
                PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[MobNum].transform.position.x, 0.15f, "+ 10금", Colors[1]);
            }
            else
            {
                Mob[MobNum].GetComponent<Mob_Move>().MoveTime = 0.0f;
                if (Facing)
                {
                    Mob[MobNum].GetComponent<Mob_Move>().NuckLeft = 0.0f;
                    Mob[MobNum].GetComponent<Mob_Move>().NuckRight = 0.2f;
                    Mob[MobNum].GetComponent<Mob_Move>().MadLeft = 0.0f;
                    Mob[MobNum].GetComponent<Mob_Move>().MadRight = 1.5f;

                    Mob[MobNum].GetComponent<Rigidbody2D>().velocity = new Vector2(1.5f, 0.0f);
                    Mob[MobNum].transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                }
                else
                {
                    Mob[MobNum].GetComponent<Mob_Move>().NuckLeft = 0.2f;
                    Mob[MobNum].GetComponent<Mob_Move>().NuckRight = 0.0f;
                    Mob[MobNum].GetComponent<Mob_Move>().MadLeft = 1.5f;
                    Mob[MobNum].GetComponent<Mob_Move>().MadRight = 0.0f;

                    Mob[MobNum].GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, 0.0f);
                    Mob[MobNum].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
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

    public void MobKill(int MobNum)
    {
        ExPoint += MobExp[MobNum];
        Money += 10;
        Txt_Money.text = Money.ToString();
        if (ExPoint >= ExMax)
        {
            ExPoint = 0.0f;
            Level++;
            Txt_Level.text = "Lv " + Level.ToString();
            ExMax = Mathf.Floor(ExMax * 1.15f);

            Power = 10 + Level * 2;
            Txt_Power.text = Power.ToString();
        }
        Sld_ExPoint.value = ExPoint / ExMax;
    }
}
