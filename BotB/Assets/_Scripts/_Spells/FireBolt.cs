using UnityEngine;
using System.Collections;

public class FireBolt : Spell {
    string m_key = "BBEDC";
    // Use this for initialization
    void Start()
    {
        m_damage = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //move that spell forward
        transform.Translate(m_velocity);
    }
    public override string Key
    {
        get { return "BBEDC"; }
    }
}
