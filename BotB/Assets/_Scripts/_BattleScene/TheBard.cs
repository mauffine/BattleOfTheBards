using UnityEngine;
using System.Collections;

public class TheBard : Musician
{
	// Use this for initialization
	void Start() 
    {
        base.Start();
        m_health = 500;
	}
	// Update is called once per frame
	void Update() 
    {
	}
    //
    protected override void Die()
    {
        if (m_health <= 0)
        {
            Battle.BattleReference.m_win = false;
            Battle.BattleReference.m_playing = false;
        }
    }
}
