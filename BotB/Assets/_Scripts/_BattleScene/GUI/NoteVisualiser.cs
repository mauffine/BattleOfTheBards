using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NoteVisualiser : MonoBehaviour
{
    private bool m_putDown = false;
    private Turn m_turn;
    private TimedNote[] m_noteType;
    // Use this for initialization
    static NoteVisualiser refToMe;
    NotePool m_notePool;

    void Start()
    {
        refToMe = this;

        m_turn = TurnTimer.Instance.CurrentTurn;
        m_notePool = GetComponentInParent<NotePool>();

        m_noteType = new TimedNote[2];
        m_noteType[0].m_active = false;
        m_noteType[1].m_active = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Battle.Instance.m_activeBattle)
        {
            Show();
            if (m_putDown)
            {
                for (int i = 0; i < m_noteType.Length; i++)
                {
                    if (m_noteType[i].m_active)
                        PushNote(m_noteType[i]);
                }

                m_putDown = false;
                m_noteType[0].m_active = false;
                m_noteType[1].m_active = false;

            }

            if (m_turn != TurnTimer.Instance.CurrentTurn)
            {
                m_turn = TurnTimer.Instance.CurrentTurn;
                m_notePool.RemoveAllNotes();
                if (m_turn == Turn.Casting)
                    ShowCombo();
            }
        }
        else
        {
            Hide();
        }


    }
    public void ReceiveNote(TimedNote a_note) //This is called and overwrites m_noteType if both player and enemy play a note at the same time
    {
        m_putDown = true;

        if (m_noteType[0].m_active == false)
            m_noteType[0] = a_note;
        else
            m_noteType[1] = a_note;

    }
    private void PushNote(TimedNote a_Note)
    {
        Vector3 modifier = (a_Note.m_playerOwned) ? new Vector3(0, 25, 0) : new Vector3(0, -110, 0);
        Vector3 pos = (Slider.LocalPosition) - new Vector3(0, 0, 0.5f) + modifier;
        Note toPlay = (a_Note.m_playerOwned) ? a_Note.m_note : Note.BLANK;

        m_notePool.AddNote(pos, toPlay, false);
    }
    public void ShowCombo()
    {
        float scrollSpeed = 1;

        switch (SpellMenu.Selection)
        {
            case SpellType.Offencive:
                {
                    if (SpellMenu.Instance.UpSelected)
                    {
                        m_notePool.AddNote(new Vector3(-640, -305, 0), Note.B, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(-320, -305, 0), Note.B, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(0, -305, 0), Note.E, true   , scrollSpeed);
                        m_notePool.AddNote(new Vector3(320, -305, 0), Note.D, true , scrollSpeed);
                        m_notePool.AddNote(new Vector3(640, -305, 0), Note.C, true , scrollSpeed);
                    }                                                              
                    else if (SpellMenu.Instance.LeftSelected)                      
                    {                                                              
                        m_notePool.AddNote(new Vector3(-640, -305, 0), Note.C, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(-320, -305, 0), Note.D, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(0, -305, 0), Note.E, true   , scrollSpeed);
                        m_notePool.AddNote(new Vector3(320, -305, 0), Note.B, true , scrollSpeed);
                        m_notePool.AddNote(new Vector3(640, -305, 0), Note.A, true , scrollSpeed);
                    }                                                              
                    else if (SpellMenu.Instance.RightSelected)                     
                    {                                                              
                        m_notePool.AddNote(new Vector3(-640, -305, 0), Note.A, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(-320, -305, 0), Note.A, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(0, -305, 0), Note.D, true   , scrollSpeed);
                        m_notePool.AddNote(new Vector3(320, -305, 0), Note.E, true , scrollSpeed);
                        m_notePool.AddNote(new Vector3(640, -305, 0),  Note.B, true, scrollSpeed);
                    }                                                              
                    break;                                                         
                }                                                                  
            case SpellType.Defensive:                                              
                {                                                                  
                    if (SpellMenu.Instance.UpSelected)                             
                    {                                                              
                        m_notePool.AddNote(new Vector3(-640, -305, 0), Note.B, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(-320, -305, 0), Note.B, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(0, -305, 0), Note.E, true   , scrollSpeed);
                        m_notePool.AddNote(new Vector3(320, -305, 0), Note.D, true , scrollSpeed);
                        m_notePool.AddNote(new Vector3(640, -305, 0), Note.D, true , scrollSpeed);
                    }                                                              
                    else if (SpellMenu.Instance.LeftSelected)                      
                    {                                                              
                        m_notePool.AddNote(new Vector3(-640, -305, 0), Note.C, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(-320, -305, 0), Note.D, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(0, -305, 0), Note.E, true   , scrollSpeed);
                        m_notePool.AddNote(new Vector3(320, -305, 0), Note.B, true , scrollSpeed);
                        m_notePool.AddNote(new Vector3(640, -305, 0), Note.B, true , scrollSpeed);
                    }                                                              
                    else if (SpellMenu.Instance.RightSelected)                     
                    {                                                              
                        m_notePool.AddNote(new Vector3(-640, -305, 0), Note.A, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(-320, -305, 0), Note.A, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(0, -305, 0), Note.D, true   , scrollSpeed);
                        m_notePool.AddNote(new Vector3(320, -305, 0), Note.E, true , scrollSpeed);
                        m_notePool.AddNote(new Vector3(640, -305, 0), Note.E, true , scrollSpeed);
                    }                                                              
                                                                                   
                    break;                                                         
                }                                                                  
            case SpellType.Effect:                                                 
                {                                                                  
                    if (SpellMenu.Instance.UpSelected)                             
                    {                                                              
                        m_notePool.AddNote(new Vector3(-640, -305, 0), Note.A, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(-320, -305, 0), Note.E, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(0, -305, 0), Note.D, true   , scrollSpeed);
                        m_notePool.AddNote(new Vector3(320, -305, 0), Note.E, true , scrollSpeed);
                        m_notePool.AddNote(new Vector3(640, -305, 0), Note.B, true , scrollSpeed);
                    }                                                              
                    else if (SpellMenu.Instance.LeftSelected)                      
                    {                                                              
                        m_notePool.AddNote(new Vector3(-640, -305, 0), Note.B, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(-320, -305, 0), Note.E, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(0, -305, 0),    Note.E, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(320, -305, 0),  Note.D, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(640, -305, 0),  Note.C, true, scrollSpeed);
                    }                                                              
                    else if (SpellMenu.Instance.RightSelected)                     
                    {                                                              
                        m_notePool.AddNote(new Vector3(-640, -305, 0), Note.A, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(-320, -305, 0), Note.B, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(0, -305, 0),    Note.A, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(320, -305, 0),  Note.E, true, scrollSpeed);
                        m_notePool.AddNote(new Vector3(640, -305, 0),  Note.A, true, scrollSpeed);
                    }
                    break;
                }
        }
    }
    public static NoteVisualiser Reference
    {
        get { return refToMe; }
    }

    void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
