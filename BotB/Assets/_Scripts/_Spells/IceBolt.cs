using UnityEngine;
using System.Collections;

public class IceBolt : Spell
{
    string m_key = "CDEBA";

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
        get { return "CDEBA"; }
    }
}