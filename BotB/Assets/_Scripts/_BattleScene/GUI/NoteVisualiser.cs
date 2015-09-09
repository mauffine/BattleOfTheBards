using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NoteVisualiser : MonoBehaviour 
{
    private bool m_putDown = false;
    private Turn m_turn;
    private TimedNote m_noteType;
	// Use this for initialization
    static NoteVisualiser refToMe;

    NotePool m_notePool;

	void Start()
    {
        refToMe = this;

        m_turn = TurnTimer.Instance.CurrentTurn;
        m_notePool = transform.parent.GetComponent<NotePool>();
	}
	
	// Update is called once per frame
	void Update()
    {
        if(m_putDown)
        {
            m_putDown = false;
            PushNote(m_noteType);
        }

        if (m_turn != TurnTimer.Instance.CurrentTurn)
        {
            m_turn = TurnTimer.Instance.CurrentTurn;
            m_notePool.RemoveAllNotes();
        }
	}
    public void ReceiveNote(TimedNote a_note)
    {
        m_putDown = true;
        m_noteType = a_note;
    }

    private void PushNote(TimedNote a_Note)
    {
        Vector3 modifier = (a_Note.m_playerOwned)? new Vector3(0, 25, 0) : new Vector3(0, -110, 0);
        Vector3 pos = (Slider.LocalPosition) - new Vector3(0, 0, 0.5f) + modifier;
        Note toPlay = (a_Note.m_playerOwned)? a_Note.m_note : Note.BLANK;

        m_notePool.AddNote(pos, toPlay);
    }

    public static NoteVisualiser Reference
    {
        get { return refToMe; }
    }
}
