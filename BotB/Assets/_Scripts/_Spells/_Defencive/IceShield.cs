using UnityEngine;
using System.Collections;

public class IceShield : Spell {

    void Start()
    {
        base.Start();
        m_damage = 0;
        m_type = SpellType.Defensive;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override Note[] Key
    {
        get { return new Note[] { Note.C, Note.D, Note.E, Note.B, Note.B, Note.C, Note.D, Note.E }; }
    }
    public override string Name
    {
        get { return "IceShield"; }
    }
    public override SpellType Type
    {
        get
        { return SpellType.Defensive; }
    }
}
