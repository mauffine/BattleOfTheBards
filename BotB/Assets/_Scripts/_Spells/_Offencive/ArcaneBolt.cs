using UnityEngine;
using System.Collections;

public class ArcaneBolt : Spell 
{
    
   // string m_key = "AADEB";
	// Use this for initialization
	void Start () 
    {
        base.Start();
        m_damage = 20;
        m_type = SpellType.Offencive;
	}
	
	// Update is called once per frame
	void Update () 
    {
        base.Update();
	}
    public override void Bounce()
    {
        transform.Rotate(new Vector3(0, 1, 0), 180);
    }
    public override Note[] Key
    {
        get { return new Note[] { Note.A, Note.A, Note.D, Note.E, Note.B, Note.D, Note.E, Note.B }; }
    }
    public override string Name
    {
        get { return "ArcaneBolt"; }
    }
}
