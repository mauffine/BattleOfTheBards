using UnityEngine;
using System.Collections;

public class ArcaneBolt : Spell {
    
    string m_key = "AADEB";
	// Use this for initialization
	void Start () {
        m_damage = 20;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(m_velocity);
	}
    public override string Key
    {
        get { return "AADEB"; }
    }
}
