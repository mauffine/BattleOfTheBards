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
    public override Note[] Key
    {
        get { return new Note[] {Note.A, Note.A, Note.A, Note.E, Note.A}; }
    }
}
