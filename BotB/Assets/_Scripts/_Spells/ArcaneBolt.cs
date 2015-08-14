using UnityEngine;
using System.Collections;

public class ArcaneBolt : Spell {
    
    string m_key = "AADEB";
	// Use this for initialization
	void Start () 
    {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();
	}
    public override string Key
    {
        get { return "AADEB"; }
    }
}
