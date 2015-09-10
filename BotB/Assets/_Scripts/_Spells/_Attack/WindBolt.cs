using UnityEngine;
using System.Collections;

public class WindBolt : Spell
{
    // Use this for initialization
    void Start()
    {
        base.Start();
        m_damage = 20;
        m_type = SpellType.Offencive;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override Note[] Key
    {
        get { return new Note[] { Note.D, Note.E, Note.B, Note.C, Note.A }; }
    }
    public override string Name
    {
        get { return "WindBolt"; }
    }
}
