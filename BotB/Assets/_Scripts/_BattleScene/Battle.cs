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
    public static Battle Instance; //singleton instance
    public GameObject m_Player, m_currentEnemy; //the characters in the scene
    public bool m_win, m_playing; //bools for the end of the battle
    //Behavious
    void Awake()
    {
        Instance = this; //set singleton instance to this
    }
    void Start()
    {
        Application.targetFrameRate = 300; //attampt this framerate

    }
    void Update()
    {

    }
    public void ReceiveKey(TimedNote a_note)
    {
        //send the keypress notifications to the following scripts
        SpellSystem.Instance.ReceiveKey(a_note);
        SoundManager.Reference.ReceiveNote(a_note);
        NoteVisualiser.Reference.ReceiveNote(a_note);
    }
    public void ReceiveTurnOver()
    {
        //notify these scripts that the casting turn is over
        SpellSystem.Instance.CastSpells();
    }
    public void DealDamage(int a_damage, bool a_toPlayer)
    {
        //deal damage to the approriate character in the scene
        if (a_toPlayer)
            m_Player.GetComponent<TheBard>().TakeDamage(a_damage);
        else
            m_currentEnemy.GetComponent<TheSlime>().TakeDamage(a_damage);
    }
}