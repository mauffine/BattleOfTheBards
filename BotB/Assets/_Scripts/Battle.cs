using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>A musical note in an enum</summary>
public enum Note : byte
{
    A,
    C,
    B,
    D,
    E
};
public struct TimedNote
{
    Note m_note;
    float m_time;
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

    bool m_win = false;
    bool m_playing = true;
    [SerializeField]
    bool m_playerTurn;
    //Behavious
	void Start () 
    {
        m_playerTurn = true;
        m_battleRef = this;
	}
	void Update () 
    {
	
	}

    /// <summary> Receive valid key presses from InputManager and passes them out to all the relevant scripts if it's a note or quits and loads the menu if it's the escape button </summary>
    /// <param name="a_note"></param>
    static public void ReceiveKey(Note a_note)
    {
        TimedNote newNote = 
        m_battleRef.SendMessage("ReceiveNote", a_note); //send it to everyone with a "PlayNote" method
    }
    /// <summary>
    /// Recieves when the current turn ends through a bool and thus also knows who's turn it is
    /// </summary>
    /// <param name="a_playerTurn"></param>
    public void RecieveTurnOver(bool a_playerTurn)
    {
        m_playerTurn = a_playerTurn;
        this.GetComponent<SpellSystem>().TurnOver();
        //change turn and notify SpellSystem to cast spells
    }
}
