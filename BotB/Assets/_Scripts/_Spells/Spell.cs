﻿using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour 
{
    public float m_speedModifier;//modifier for deltaTime
    public Vector3 m_velocity;//The velocity
    public string m_name;
    protected string m_key;//A key to show what key
    protected Stats m_stats;
	// Use this for initialization
	protected void Start() 
    {
        m_speedModifier = 80;
        m_stats.Damage = 20;
	}
	// Update is called once per frame
	protected void Update() 
    {
        transform.Translate(m_velocity * Time.deltaTime * m_speedModifier);
	}
    public int Damage
    {
        get { return m_stats.Damage; }
    }
    public virtual string Key
    {
        get { return m_key; }
    }
}
