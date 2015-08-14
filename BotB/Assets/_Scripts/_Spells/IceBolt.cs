using UnityEngine;
using System.Collections;

public class IceBolt : Spell
{
    string m_key = "CDEBA";

    // Use this for initialization
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override string Key
    {
        get { return "CDEBA"; }
    }
}