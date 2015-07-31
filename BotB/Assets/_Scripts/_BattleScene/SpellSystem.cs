using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

struct SpellData
{
    Vector3 m_colour;
    int m_damage;

}
public class SpellSystem : MonoBehaviour {
    //Attributes
    Dictionary<Note[], string> m_spellList = new Dictionary<Note[], string>(); //list of all the spells

    [SerializeField]
    List<Note> m_currentNotes; //list of notes played this turn

    [SerializeField]
    List<GameObject> m_Emitter = new List<GameObject>(); //list of current spell effects
    //Behaviours
    void Start () 
    {
        Note[] ArcaneMissile = new Note[] {Note.A, Note.B, Note.B, Note.C};
        m_spellList.Add(ArcaneMissile, "ArcaneMissile");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    /// <summary> Receive's note from Battle.cs as a_note </summary>
    /// <param name="a_note"></param>
    void ReceiveNote (TimedNote a_note)
    {
        m_currentNotes.Add(a_note.m_note);
    }
    /// <summary>casts spells, clears notes</summary>
    public void TurnOver()
    {
        CheckForSpell(m_currentNotes, m_spellList);
        m_currentNotes.Clear();
    }
    /// <summary>Checks the played notes for spells</summary>
    /// <param name="a_CurrentNotes"></param>
    /// <param name="a_spellList"></param>
    void CheckForSpell(List<Note> a_currentNotes, Dictionary<Note[], string> a_spellList)
    {
        var spellEnumerator = a_spellList.GetEnumerator();
        while (spellEnumerator.MoveNext())
        {
            //Check the full list of notes 4 at a time
            for (int i = 0; i + 3 < a_currentNotes.Count; ++i)
            {
                Note[] currentSequence = new Note[] { a_currentNotes[i], a_currentNotes[i + 1], a_currentNotes[i + 2], a_currentNotes[i + 3] };
                if (currentSequence.SequenceEqual(spellEnumerator.Current.Key))
                {
                    m_Emitter.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Emitter")));
                }
            }
        }
    }
}
