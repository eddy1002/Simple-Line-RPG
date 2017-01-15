using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMob_Move : MonoBehaviour {
    public GameObject[] Mobs;

    public float ZenTime;
    public float ZenMax;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (ZenTime >= ZenMax)
        {
            ZenTime = 0.0f;
            for (int i = 0; i < Mobs.Length; i++)
            {
                if (!Mobs[i].activeSelf)
                {
                    Mobs[i].transform.position = new Vector3(Random.Range(-7.0f, 7.0f), 0.0f, 0.0f);
                    Mobs[i].SetActive(true);
                    Mobs[i].GetComponent<Mob_Move>().MobSetting();
                }
            }
        }
        else
            ZenTime += Time.deltaTime;
	}
}
