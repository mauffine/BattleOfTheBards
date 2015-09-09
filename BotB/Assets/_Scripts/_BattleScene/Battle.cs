using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public enum Turn
{
    Casting, Menu
}
/// <summary>A musical note in an enum, _ means the note is flat</summary>
public enum Note : byte
{
    A_,
    A,
    B_,
    B,
    C,
    D_,
    D,
    E,
    F_,
    F,
    G,
    BLANK,
};
/// <summary>A struct for the time a note was played and the note itself</summary>
public struct TimedNote
{
    public Note m_note;
    public float m_time;
    public bool m_playerOwned; //need a better name...
    public TimedNote(Note a_note, float a_time, bool a_playerOwned = false)
    {
        m_note = a_note;
        m_time = a_time;
        m_playerOwned = a_playerOwned;
    }
}
//*********************************************************************************
//actual class starts here
public class Battle : MonoBehaviour
{
    //Attributes
    public static Battle Instance;
    public GameObject m_Player, m_currentEnemy;
    public bool m_win, m_playing;
    //Behavious
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Application.targetFrameRate = 300;

    }
    void Update()
    {

    }
    public void ReceiveKey(TimedNote a_note)
    {
        SpellSystem.Instance.ReceiveKey(a_note);
        SoundManager.Reference.ReceiveNote(a_note);
        NoteVisualiser.Reference.ReceiveNote(a_note);
    }
    public void ReceiveTurnOver()
    {
        SpellSystem.Instance.CastSpells();
    }
    public void DealDamage(int a_damage, bool a_toPlayer)
    {
        if (a_toPlayer)
            m_Player.GetComponent<TheBard>().TakeDamage(a_damage);
        else
            m_currentEnemy.GetComponent<TheSlime>().TakeDamage(a_damage);
    }
}