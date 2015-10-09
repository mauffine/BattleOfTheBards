using UnityEngine;
using System.Collections;

public class ArcaneShield : Spell {

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
        get { return new Note[] { Note.A, Note.A, Note.D, Note.E, Note.E }; }
    }
    public override string Name
    {
        get { return "ArcaneShield"; }
    }
    public override SpellType Type
    {
        get
        { return SpellType.Defensive; }
    }
}
