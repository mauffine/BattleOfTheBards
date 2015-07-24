using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Battle : MonoBehaviour 
{
    //Attributes
    Dictionary<KeyCode, string> m_notes;
    
    bool m_win;
    bool m_playing;

    [SerializeField]
    bool m_playerTurn;
    //Behavious
	void Start () 
    {
        m_playerTurn = true;
	}
	
	void Update () 
    {
	
	}
    //Receives valid key presses from InputManager and passes them out to all the relevant scripts if
    //it's a not or quits and loads the menu if it's the escape button
    public void ReceiveKey(KeyCode a_key)
    {
        if (m_notes.ContainsKey(a_key))
        {
            string note;
            m_notes.TryGetValue(a_key, out note); //grab the note out of a dictionary
            SendMessage("PlayNote", note); //send it to everyone with a "PlayNote" method
        }
        else if (a_key == KeyCode.Escape)
        {
            //go back to menu
            //Application.LoadLevel("Menu")
        }
    }
    public void RecieveTurnOver(bool a_playerTurn)
    {
        m_playerTurn = a_playerTurn;
        //change turn and notify SpellSystem to cast spells
    }
}
