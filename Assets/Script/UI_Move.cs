using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Move : MonoBehaviour {

    public GameObject UI_Upgrade;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowUpgrade()
    {
        if (UI_Upgrade.activeSelf)
            UI_Upgrade.SetActive(false);
        else
            UI_Upgrade.SetActive(true);
    }
}
