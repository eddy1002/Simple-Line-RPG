using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Move : MonoBehaviour {

    public GameObject Player;
    public GameObject PoolDam;

    public SpriteRenderer PlayerBody;

    public Slider Sld_HPPoint;
    public Slider Sld_ExPoint;

    public Text Txt_Level;
    public Text Txt_Power;
    public Text Txt_Money;
    public Text Txt_HPPoint;
    public Text Txt_ExPoint;

    public Image Skill1Button;
    public Image Skill2Button;
    public Image Skill3Button;
    public Image PotionButton;

    public GameObject[] Mob;

    public int Level;
    public int Power;
    public int Money;
    public int Skill02Time;

    public bool Attacking;
    public bool LWalking;
    public bool RWalking;
    public bool Facing;
    public bool GodMod;

    public float ExPoint;
    public float ExMax;
    public float HPPoint;
    public float HPMax;
    public float GodTime;
    public float Skill1Cool;
    public float Skill2Cool;
    public float Skill3Cool;
    public float PotionCool;

    public float[] MobExp;

    public Color[] Colors;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (GodTime > 0.0f) {
            GodTime -= Time.deltaTime;

            float GodEffectTime = GodTime * 10 - Mathf.Floor(GodTime * 10);

            if (GodEffectTime < 0.5f || GodTime <= 0.0f)
                PlayerBody.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            else
                PlayerBody.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }

        if (Player.transform.position.x > 7.0f)
            Player.transform.position = new Vector2(7.0f, 0.0f);

        if (Player.transform.position.x < -7.0f)
            Player.transform.position = new Vector2(-7.0f, 0.0f);

        if (Skill1Cool > 0.0f)
        {
            Skill1Cool -= Time.deltaTime;

            Skill1Button.fillAmount = (2.0f - Skill1Cool) / 2.0f;
        }
        if (Skill2Cool > 0.0f)
        {
            Skill2Cool -= Time.deltaTime;

            Skill2Button.fillAmount = (15.0f - Skill2Cool) / 15.0f;
        }
        if (Skill3Cool > 0.0f)
        {
            Skill3Cool -= Time.deltaTime;

            Skill3Button.fillAmount = (30.0f - Skill3Cool) / 30.0f;
        }
        if (PotionCool > 0.0f)
        {
            PotionCool -= Time.deltaTime;

            PotionButton.fillAmount = (10.0f - PotionCool) / 10.0f;
        }
    }

    public void AttackStart()
    {
        Player.GetComponent<Animator>().SetFloat("Attack", 1);
        GodMod = false;
    }

    public void AttackEnd()
    {
        Player.GetComponent<Animator>().SetFloat("Attack", 0);
        GodMod = false;
    }

    public void Skill00Start()
    {
        Player.GetComponent<Animator>().SetFloat("Attack", 2);
        GodMod = false;
    }

    public void Skill00End()
    {
        Player.GetComponent<Animator>().SetFloat("Attack", 0);
        GodMod = false;
    }

    public void Skill01Do()
    {
        if (Skill1Cool <= 0.0f)
        {
            Player.GetComponent<Animator>().SetFloat("Attack", 3);
            GodMod = false;

            Skill1Cool = 2.0f;
        }
    }

    public void Skill02Start()
    {
        if (Skill2Cool <= 0.0f)
        {
            Player.GetComponent<Animator>().SetFloat("Attack", 4);
            Skill02Time = 20;
            GodMod = false;

            Skill2Cool = 15.0f;
        }
    }

    public void Skill02End()
    {
        Player.GetComponent<Animator>().SetFloat("Attack", 0);
        GodMod = false;
    }

    public void Skill03Start()
    {
        if (Skill3Cool <= 0.0f)
        {
            Player.GetComponent<Animator>().SetFloat("Attack", 5);
            GodMod = true;

            Skill3Cool = 30.0f;
        }
    }

    public void Skill03End()
    {
        Player.GetComponent<Animator>().SetFloat("Attack", 0);
        GodMod = false;
    }

    public void PotionStart()
    {
        if (PotionCool <= 0.0f)
        {
            HPPoint = HPMax;
            Sld_HPPoint.value = HPPoint / HPMax;
            Txt_HPPoint.text = HPPoint.ToString() + " / " + HPMax.ToString();

            PotionCool = 10.0f;
        }
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

        if (MobNum < 0)
            return;

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
            AttackMob(MobNum);
        }
    }

    public void Skill00()
    {
        for (int i = 0; i < Mob.Length; i++)
        {
            if (Mob[i].GetComponent<Mob_Move>().HPPoint <= 0)
                continue;

            float MobFacing = Mob[i].transform.position.x - Player.transform.position.x;
            float MobRange = Vector2.Distance(Mob[i].transform.position, Player.transform.position);
            if (Facing && MobFacing > -0.5f || !Facing && MobFacing < 0.5f)
            {
                if (MobRange < 2.75)
                {
                    int FinalPower = (int)Mathf.Round(Power * Random.Range(0.75f, 1.25f) * 1.5f);
                    PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[i].transform.position.x, 0.75f, FinalPower.ToString(), Colors[0]);

                    Mob[i].GetComponent<Mob_Move>().HPPoint -= FinalPower;
                    if (Mob[i].GetComponent<Mob_Move>().HPPoint <= 0)
                    {
                        Mob[i].GetComponent<Mob_Move>().Death();
                        MobKill(0);
                        PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[i].transform.position.x, 0.15f, "+ 10금", Colors[1]);
                    }
                    else
                    {
                        AttackMob(i);
                    }
                }
            }
        }
    }

    public void Skill01A()
    {
        if (!Facing)
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(-10.0f, 0.0f);
        else
            Player.GetComponent<Rigidbody2D>().velocity = new Vector2(10.0f, 0.0f);

        GodMod = true;
    }
    public void Skill01B()
    {
        Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Player.GetComponent<Animator>().SetFloat("Attack", 0);

        GodMod = false;
    }

    public void Skill01C()
    {
        for (int i = 0; i < Mob.Length; i++)
        {
            if (Mob[i].GetComponent<Mob_Move>().HPPoint <= 0)
                continue;

            float MobFacing = Mob[i].transform.position.x - Player.transform.position.x;
            float MobRange = Vector2.Distance(Mob[i].transform.position, Player.transform.position);
            if (Facing && MobFacing > -1.0f || !Facing && MobFacing < 1.0f)
            {
                if (MobRange < 5)
                {
                    int FinalPower = (int)Mathf.Round(Power * Random.Range(0.75f, 1.25f) * 1.5f);
                    PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[i].transform.position.x, 0.75f, FinalPower.ToString(), Colors[0]);

                    Mob[i].GetComponent<Mob_Move>().HPPoint -= FinalPower;
                    if (Mob[i].GetComponent<Mob_Move>().HPPoint <= 0)
                    {
                        Mob[i].GetComponent<Mob_Move>().Death();
                        MobKill(0);
                        PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[i].transform.position.x, 0.15f, "+ 10금", Colors[1]);
                    }
                    else
                    {
                        AttackMob(i);
                    }
                }
            }
        }
    }

    public void Skill02A()
    {
        for (int i = 0; i < Mob.Length; i++)
        {
            if (Mob[i].GetComponent<Mob_Move>().HPPoint <= 0)
                continue;

            float MobRange = Vector2.Distance(Mob[i].transform.position, Player.transform.position);
            if (MobRange < 2.5)
            {
                int FinalPower = (int)Mathf.Round(Power * Random.Range(0.75f, 1.25f) * 0.25f);
                PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[i].transform.position.x, 0.75f, FinalPower.ToString(), Colors[0]);

                Mob[i].GetComponent<Mob_Move>().HPPoint -= FinalPower;
                if (Mob[i].GetComponent<Mob_Move>().HPPoint <= 0)
                {
                    Mob[i].GetComponent<Mob_Move>().Death();
                    MobKill(0);
                    PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[i].transform.position.x, 0.15f, "+ 10금", Colors[1]);
                }
                else
                {
                    AttackMob(i);
                }
            }
        }
    }
    public void Skill02B()
    {
        Skill02Time -= 1;

        if (Skill02Time <= 0)
            Skill02End();
    }

    public void Skill03()
    {
        for (int i = 0; i < Mob.Length; i++)
        {
            if (Mob[i].GetComponent<Mob_Move>().HPPoint <= 0)
                continue;

            int FinalPower = (int)Mathf.Round(Power * Random.Range(0.75f, 1.25f) * 15.0f);
            PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[i].transform.position.x, 0.75f, FinalPower.ToString(), Colors[0]);

            Mob[i].GetComponent<Mob_Move>().HPPoint -= FinalPower;
            if (Mob[i].GetComponent<Mob_Move>().HPPoint <= 0)
            {
                Mob[i].GetComponent<Mob_Move>().Death();
                MobKill(0);
                PoolDam.GetComponent<PoolDam_Move>().MakeDam(Mob[i].transform.position.x, 0.15f, "+ 10금", Colors[1]);
            }
            else
            {
                AttackMob(i);
            }
        }
    }

    public void AttackMob(int MobNum)
    {
        Mob[MobNum].GetComponent<Mob_Move>().MoveTime = 0.0f;
        float NuckFacing = Mob[MobNum].transform.position.x - Player.transform.position.x;
        if (NuckFacing > 0.0f)
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

    public int FindMob()
    {
        int MobNum = -1;
        float MobRangeBank = -1.0f;
        for (int i = 0; i < Mob.Length; i++)
        {
            if (Mob[i].GetComponent<Mob_Move>().HPPoint <= 0)
                continue;

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

            HPMax += 10;
            HPPoint = HPMax;
            Sld_HPPoint.value = HPPoint / HPMax;
            Txt_HPPoint.text = HPPoint.ToString() + " / " + HPMax.ToString();
        }
        Sld_ExPoint.value = ExPoint / ExMax;
        Txt_ExPoint.text = ExPoint.ToString() + " / " + ExMax.ToString();
    }
}
