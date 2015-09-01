using UnityEngine;
using System.Collections;

public class TheSlime : Musician
{

    // Use this for initialization
    void Start()
    {
        base.Start();
        m_health = 300;
    }

    // Update is called once per frame
    void Update() 
    {
        base.Update();
    }

    protected override void Die()
    {
        Battle.BattleReference.m_win = true;
        Battle.BattleReference.m_playing = false;
    }
}
