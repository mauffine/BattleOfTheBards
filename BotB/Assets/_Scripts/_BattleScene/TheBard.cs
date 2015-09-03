using UnityEngine;
using System.Collections;

public class TheBard : Musician
{
	// Use this for initialization
	void Start() 
    {
        base.Start();
        m_stats.Health = 500;
	}
	// Update is called once per frame
	void Update() 
    {
	}
    //
    protected override void Die()
    {
    }
}
