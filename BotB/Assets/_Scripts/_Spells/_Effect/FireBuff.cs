using UnityEngine;
using System.Collections;

public class FireBuff : Spell 
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
        get { return "FireBuff"; }
    }
    public override Note[] Key
    {
        get { return new Note[] { Note.B, Note.E, Note.E, Note.D, Note.C, Note.A, Note.B, Note.C }; }
    }
    public override SpellType Type
    {
        get{ return SpellType.Effect; }
    }
}
