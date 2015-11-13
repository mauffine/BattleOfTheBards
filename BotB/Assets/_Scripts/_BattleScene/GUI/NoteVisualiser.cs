using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NoteVisualiser : MonoBehaviour
{
    [SerializeField]
    private GameObject m_offensiveUpSpell, m_offensiveLeftSpell, m_offensiveRightSpell,
                       m_defensiveUpSpell, m_defensiveLeftSpell, m_defensiveRightSpell,
                       m_effectUpSpell,    m_effectLeftSpell,    m_effectRightSpell;
    private bool m_putDown = false;
    private Turn m_turn;
    private TimedNote[] m_noteType;
    // Use this for initialization
    static NoteVisualiser refToMe;
    private NotePool m_notePool;

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
                if (m_turn == Turn.Casting)
                    m_notePool.RemoveAllNotes();

                m_turn = TurnTimer.Instance.CurrentTurn;

                if (m_turn == Turn.Pause)
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
    {//CHANGE THIS BIT TO GET PLAYER NOTE HIGHT WORKING
        Vector3 modifier = (a_Note.m_playerOwned) ? new Vector3(0, 25, 0) : new Vector3(0, -110, 0);
        Vector3 pos = (MusicSlider.LocalPosition) - new Vector3(0, 0, 0.5f) + modifier;
        Note toPlay = (a_Note.m_playerOwned) ? a_Note.m_note : Note.BLANK;

        if (a_Note.m_playerOwned)
            m_notePool.AddNote(pos.x, toPlay, false);
        //else
            //m_notePool.AddNote(pos, toPlay, false);
    }
    public void ShowCombo()
    {

        float pos1 = -746.6666666666667f;
        float pos2 = -533.3333333333334f;
        float pos3 = -320.0000000000001f;
        float pos4 = -106.6666666666668f;
        float pos5 = 106.6666666666665f;
        float pos6 = 320.0000000000001f;
        float pos7 = 533.3333333333334f;
        float pos8 = 746.6666666666667f;

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
                        spell = m_effectUpSpell;
                    }                                                              
                    else if (SpellMenu.Instance.LeftSelected)                      
                    {
                        spell = m_effectLeftSpell;
                    }                                                              
                    else if (SpellMenu.Instance.RightSelected)                     
                    {
                        spell = m_effectRightSpell;
                    }
                    break;
                }
        }
        m_notePool.AddNote(pos1, spell.GetComponent<Spell>().Key[0], true);
        m_notePool.AddNote(pos2, spell.GetComponent<Spell>().Key[1], true);
        m_notePool.AddNote(pos3, spell.GetComponent<Spell>().Key[2], true);
        m_notePool.AddNote(pos4, spell.GetComponent<Spell>().Key[3], true);
        m_notePool.AddNote(pos5, spell.GetComponent<Spell>().Key[4], true);
        m_notePool.AddNote(pos6, spell.GetComponent<Spell>().Key[5], true);
        m_notePool.AddNote(pos7, spell.GetComponent<Spell>().Key[6], true);
        m_notePool.AddNote(pos8, spell.GetComponent<Spell>().Key[7], true);
        
    }
    public static NoteVisualiser Reference
    {
        get { return refToMe; }
    }

    void Hide()
    {
        GetComponent<CanvasRenderer>().SetAlpha(0);
    }

    void Show()
    {
        GetComponent<CanvasRenderer>().SetAlpha(255);

    }
}
