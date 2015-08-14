﻿using UnityEngine;
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
    GameObject m_player;

    [SerializeField]
    GameObject m_slime;

    [SerializeField]
    Text m_debugText;

    public bool m_win = false;
    public bool m_playing = true;
    [SerializeField]
    public bool m_playerTurn;

    float m_gameOverTimer;
    //Behavious
	void Start () 
    {
        Application.targetFrameRate = 300;
        m_playerTurn = false;
        m_battleRef = this;
        //m_debugText.text = "PlayerTurn";
        m_gameOverTimer = 4.0f;
	}
	void Update () 
    {
        if (!m_playing)
        {
            m_gameOverTimer -= Time.deltaTime;
        }
        if (m_gameOverTimer <= 0)
        {
            Application.Quit();
        }
	}

    /// <summary> Receive valid key presses from InputManager and passes them out to all the relevant scripts if it's a note or quits and loads the menu if it's the escape button </summary>
    /// <param name="a_note"></param>
    static public void ReceiveKey(TimedNote a_note)
    {
        m_battleRef.SendMessage("ReceiveNote", a_note); //send it to everyone with a "PlayNote" method
        NoteVisualiser.Reference.ReceiveNote(a_note);
    }

    ///<summary> Recieves when the current turn ends through a bool and thus also knows who's turn it is</summary>
    /// <param name="a_playerTurn"></param>
    public void RecieveTurnOver(bool a_playerTurn)
    {
        m_playerTurn = a_playerTurn;
        GetComponent<SpellSystem>().TurnOver();/*
        if (!m_playerTurn)
            m_debugText.text = "EnemyTurn";
        else
            m_debugText.text = "PlayerTurn"; */
        //change turn and notify SpellSystem to cast spells
    }

    /// <summary>Called by spellsystem, Deals damage to character bassed on who's turn it is</summary>
    /// <param name="a_damage">Damage dealt to character</param>
    public void DealDamage(uint a_damage)
    {
        if (m_playerTurn) 
        {
            m_player.GetComponent<TheBard>().TakeDamage(a_damage);
        }
        else
        {
            m_slime.GetComponent<TheSlime>().TakeDamage(a_damage);
        }
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
