using UnityEngine;
using System.Collections;

public class FireBolt : Spell {
    //string m_key = "BBEDC";
    // Use this for initialization
    void Start()
    {
        base.Start();
        m_damage = 20;
        m_type = SpellType.Attack;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override Note[] Key
    {
        get { return new Note[] { Note.B, Note.B, Note.E, Note.D, Note.C }; }
    }
    public override string Name
    {
        get { return "FireBolt"; }
    }
}
