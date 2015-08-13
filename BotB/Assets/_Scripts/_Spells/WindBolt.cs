using UnityEngine;
using System.Collections;

public class WindBolt : Spell
{
    
    string m_key = "DEBCA";
    // Use this for initialization
    void Start()
    {
        m_damage = 20;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_velocity);
    }
    public override string Key
    {
        get { return "DEBCA"; }
    }
}
