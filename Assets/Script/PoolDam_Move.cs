using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolDam_Move : MonoBehaviour {

    public GameObject[] DamText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MakeDam(float X, float Y, string Text, Color Color)
    {
        for (int DamNum = 0; DamNum < DamText.Length; DamNum++)
        {
            if (!DamText[DamNum].activeSelf)
            {
                DamText[DamNum].transform.position = new Vector2(X, Y);
                DamText[DamNum].SetActive(true);
                DamText[DamNum].GetComponent<DamText_Move>().DamStart(Text, Color);
                break;    
            }
        }
    }
}
