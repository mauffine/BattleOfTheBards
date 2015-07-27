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

public class Battle : MonoBehaviour 
{
    //Attributes
    static Dictionary<Note, string> m_notes;
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
    //Receives valid key presses from InputManager and passes them out to all the relevant scripts if
    //it's a not or quits and loads the menu if it's the escape button
    static public void ReceiveKey(Note a_key)
    {
        if (m_notes.ContainsKey(a_key))
        {
            string note;
            m_notes.TryGetValue(a_key, out note); //grab the note out of a dictionary
            m_battleRef.SendMessage("PlayNote", note); //send it to everyone with a "PlayNote" method
        }
    }
    public void RecieveTurnOver(bool a_playerTurn)
    {
        m_playerTurn = a_playerTurn;
        //change turn and notify SpellSystem to cast spells
    }
}
