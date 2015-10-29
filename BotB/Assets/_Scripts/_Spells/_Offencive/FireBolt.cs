using UnityEngine;
using System.Collections;

public class FireBolt : Spell {
    //string m_key = "BBEDC";
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
    public override void Bounce()
    {
        transform.Rotate(new Vector3(0, 1, 0), 180);
    }
    public override Note[] Key
    {
        get { return new Note[] { Note.B, Note.B, Note.E, Note.D, Note.C, Note.A, Note.B, Note.C }; }
    }
    public override string Name
    {
        get { return "FireBolt"; }
    }
}
