using UnityEngine;
using System.Collections;

public class Hasten : Spell 
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
        get { return "Hasten"; }
    }
    public override Note[] Key
    {
        get { return new Note[] {Note.A, Note.B, Note.A, Note.E, Note.A}; }
    }
    public override SpellType Type
    {
        get{ return SpellType.Effect; }
    }
}
