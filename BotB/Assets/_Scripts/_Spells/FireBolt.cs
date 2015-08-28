using UnityEngine;
using System.Collections;

public class FireBolt : Spell {
    //string m_key = "BBEDC";
    // Use this for initialization
    void Start()
    {
        base.Start();
        m_stats.Damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override string Key
    {
        get { return "BBEDC"; }
    }
}
