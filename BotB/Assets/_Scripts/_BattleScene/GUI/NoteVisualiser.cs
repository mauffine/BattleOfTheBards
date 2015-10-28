﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NoteVisualiser : MonoBehaviour
{
    [SerializeField]
    private GameObject m_offensiveUpSpell, m_offensiveLeftSpell, m_offensiveRightSpell,
    m_defensiveUpSpell, m_defensiveLeftSpell, m_defensiveRightSpell,
    m_EffectUpSpell, m_EffectLeftSpell, m_EffectRightSpell;
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

        Vector3 pos1 = new Vector3(-746.6666666666667f, -305, 0);
        Vector3 pos2 = new Vector3(-533.3333333333334f, -305, 0);
        Vector3 pos3 = new Vector3(-320.0000000000001f, -305, 0);
        Vector3 pos4 = new Vector3(-106.6666666666668f, -305, 0);
        Vector3 pos5 = new Vector3(106.6666666666665f, -305, 0);

        GameObject spell = null;

        switch (SpellMenu.Selection)
        {
            case SpellType.Offencive:
                {
                    if (SpellMenu.Instance.UpSelected)
                    {
                        spell = m_offensiveUpSpell;
                    }                                                              
                    else if (SpellMenu.Instance.LeftSelected)                      
                    {
                        spell = m_offensiveLeftSpell;
                    }                                                              
                    else if (SpellMenu.Instance.RightSelected)                     
                    {
                        spell = m_offensiveRightSpell;
                    }                                                              
                    break;
                }                                                                  
            case SpellType.Defensive:                                              
                {                                                                  
                    if (SpellMenu.Instance.UpSelected)
                    {
                        spell = m_defensiveUpSpell;
                    }                                                              
                    else if (SpellMenu.Instance.LeftSelected)                      
                    {
                        spell = m_defensiveLeftSpell;
                    }                                                              
                    else if (SpellMenu.Instance.RightSelected)                     
                    {
                        spell = m_defensiveRightSpell;
                    }                                                              
                                                                                   
                    break;                                                         
                }                                                                  
            case SpellType.Effect:                                                 
                {                                                                  
                    if (SpellMenu.Instance.UpSelected)                             
                    {
                        spell = m_EffectUpSpell;
                    }                                                              
                    else if (SpellMenu.Instance.LeftSelected)                      
                    {
                        spell = m_EffectLeftSpell;
                    }                                                              
                    else if (SpellMenu.Instance.RightSelected)                     
                    {
                        spell = m_EffectRightSpell;
                    }
                    break;
                }
        }
        m_notePool.AddNote(pos1, spell.GetComponent<Spell>().Key[0], true, scrollSpeed);
        m_notePool.AddNote(pos2, spell.GetComponent<Spell>().Key[1], true, scrollSpeed);
        m_notePool.AddNote(pos3, spell.GetComponent<Spell>().Key[2], true, scrollSpeed);
        m_notePool.AddNote(pos4, spell.GetComponent<Spell>().Key[3], true, scrollSpeed);
        m_notePool.AddNote(pos5, spell.GetComponent<Spell>().Key[4], true, scrollSpeed);
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
