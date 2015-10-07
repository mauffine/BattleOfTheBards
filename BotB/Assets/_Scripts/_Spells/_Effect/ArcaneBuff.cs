using UnityEngine;
using System.Collections;

public class ArcaneBuff : Spell 
{
    //string m_key = "AAAEA";

    // Use this for initialization
    void Start()
    {
        base.Start();
        m_type = SpellType.Effect;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override string Name
    {
        get { return "ArcaneBuff"; }
    }
    public override Note[] Key
    {
        get { return new Note[] {Note.A, Note.E, Note.D, Note.E, Note.B}; }
    }
    public override SpellType Type
    {
        get{ return SpellType.Effect; }
    }
}
