using UnityEngine;
using System.Collections;

public class ArcaneBolt : Spell 
{
    
   // string m_key = "AADEB";
	// Use this for initialization
	void Start () 
    {
        base.Start();
        m_stats.Damage = 20;
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
