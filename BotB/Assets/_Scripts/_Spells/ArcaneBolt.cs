using UnityEngine;
using System.Collections;

public class ArcaneBolt : Spell {

	// Use this for initialization
	void Start () {
        m_damage = 20;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(m_velocity);
	}
}
