using UnityEngine;
using System.Collections;

public class FireShield : Spell {

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
        get { return new Note[] { Note.B, Note.B, Note.E, Note.D, Note.D, Note.A, Note.B, Note.C }; }
    }
    public override string Name
    {
        get { return "FireShield"; }
    }
    public override SpellType Type
    {
        get { return SpellType.Defensive; }
    }
}
