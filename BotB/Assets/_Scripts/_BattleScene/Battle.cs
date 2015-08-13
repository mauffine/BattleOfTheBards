using UnityEngine;
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

    bool m_win = false;
    [SerializeField]
    bool m_playerTurn;
    //Behavious
	void Start () 
    {
        Application.targetFrameRate = 300;
        m_playerTurn = true;
        m_battleRef = this;
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
        this.GetComponent<SpellSystem>().TurnOver();
        //change turn and notify SpellSystem to cast spells
    }

    public static Battle BattleReference
    {
        get {return m_battleRef; }
    }

    public bool PlayerTurn
    {
        get { return m_playerTurn; }
    }
}
