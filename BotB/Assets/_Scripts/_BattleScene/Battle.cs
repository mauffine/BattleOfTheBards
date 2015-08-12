using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
    public TimedNote(Note a_note, float a_time)
    {
        m_note = a_note;
        m_time = a_time;
    }
}
public class Battle : MonoBehaviour 
{
    //Attributes
    static Battle m_battleRef;

    [SerializeField]
    Text m_debugText;

    bool m_win = false;
    bool m_playing = true;
    [SerializeField]
    public bool m_playerTurn;
    //Behavious
	void Start () 
    {
        m_playerTurn = true;
        m_battleRef = this;
        m_debugText.text = "PlayerTurn";
	}
	void Update () 
    {
	
	}

    /// <summary> Receive valid key presses from InputManager and passes them out to all the relevant scripts if it's a note or quits and loads the menu if it's the escape button </summary>
    /// <param name="a_note"></param>
    static public void ReceiveKey(TimedNote a_note)
    {
        m_battleRef.SendMessage("ReceiveNote", a_note); //send it to everyone with a "PlayNote" method
    }
    /// <summary>
    /// Recieves when the current turn ends through a bool and thus also knows who's turn it is
    /// </summary>
    /// <param name="a_playerTurn"></param>
    public void RecieveTurnOver(bool a_playerTurn)
    {
        m_playerTurn = a_playerTurn;
        GetComponent<SpellSystem>().TurnOver();
        if (!m_playerTurn)
            m_debugText.text = "EnemyTurn";
        else
            m_debugText.text = "PlayerTurn";
        //change turn and notify SpellSystem to cast spells
    }
    /// <summary>Called by spellsystem, Deals damage to character bassed on who's turn it is</summary>
    /// <param name="a_damage">Damage dealt to character</param>
    public void DealDamage(uint a_damage)
    {
        //pass the damage off to a character bassed on turn
    }
}
