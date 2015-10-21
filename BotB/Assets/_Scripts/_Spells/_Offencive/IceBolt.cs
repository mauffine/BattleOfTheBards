using UnityEngine;
using System.Collections;

public class IceBolt : Spell
{
    //string m_key = "CDEBA";

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
        get { return new Note[] { Note.C, Note.D, Note.E, Note.B, Note.A, Note.D, Note.E, Note.B }; }
    }
    public override string Name
    {
        get { return "Icebolt"; }
    }
}