using UnityEngine;
using System.Collections;

public class TheSlime : Musician
{

    // Use this for initialization
    void Start()
    {
        base.Start();
        m_stats.Health = 300;
    }

    // Update is called once per frame
    void Update() 
    {
        base.Update();
    }

    protected override void Die()
    {
        if (m_stats.Health <= 0)
        {
        }
    }
}
