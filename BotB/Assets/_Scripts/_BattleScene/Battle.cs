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
    public GameObject m_player;

    [SerializeField]
    public GameObject m_slime;
    [SerializeField]
    Canvas m_GUICanvas;
    TextGen m_damageDisplay;

    public bool m_win = false;
    public bool m_playing = true;
    bool m_displayingText = false;
    [SerializeField]
    public bool m_playerTurn;

    float m_gameOverTimer;
    //Behavious
	void Start() 
    {
        Application.targetFrameRate = 300;
        m_damageDisplay = m_GUICanvas.GetComponent<TextGen>();
        m_playerTurn = false;
        m_battleRef = this;
        m_gameOverTimer = 1.5f;
        m_displayingText = false;
	}
	void Update() 
    {
        if (!m_playing)
        { 
            m_gameOverTimer -= Time.deltaTime;
            if (!m_displayingText)
                m_damageDisplay.YouWin();
        }
        if (m_gameOverTimer <= 0)
            Application.Quit();
	}

    /// <summary> Receive valid key presses from InputManager and passes them out to all the relevant scripts if it's a note or quits and loads the menu if it's the escape button </summary>
    /// <param name="a_note"></param>
    static public void ReceiveKey(TimedNote a_note)
    {
        if (m_battleRef.PlayerTurn)
        {
            switch (a_note.m_note)
            {
                case Note.A_:
                    break;
                case Note.A:
                    {
                        BattleReference.m_player.GetComponent<Animator>().Play(Animator.StringToHash("Chord1"));
                    }
                    break;
                case Note.B_:
                    break;
                case Note.B:
                    {
                        BattleReference.m_player.GetComponent<Animator>().Play(Animator.StringToHash("Chord2"));
                    }
                    break;
                case Note.C:
                    {
                        BattleReference.m_player.GetComponent<Animator>().Play(Animator.StringToHash("Chord3"));
                    }
                    break;
                case Note.D_:
                    break;
                case Note.D:
                    {
                        BattleReference.m_player.GetComponent<Animator>().Play(Animator.StringToHash("Chord1"));
                    }
                    break;
                case Note.E:
                    {
                        BattleReference.m_player.GetComponent<Animator>().Play(Animator.StringToHash("Chord4"));
                    }
                    break;
                case Note.F_:
                    break;
                case Note.F:
                    break;
                case Note.G:
                    break;
                default:
                    break;
            }

        }
        else
        {
            switch (a_note.m_note)
            {
                case Note.A_:
                    break;
                case Note.A:
                    {
                        BattleReference.m_slime.GetComponent<Animator>().Play(Animator.StringToHash("Attack"));
                    }
                    break;
                case Note.B_:
                    break;
                case Note.B:
                    {
                        BattleReference.m_slime.GetComponent<Animator>().Play(Animator.StringToHash("Attack"));
                    }
                    break;
                case Note.C:
                    {
                        BattleReference.m_slime.GetComponent<Animator>().Play(Animator.StringToHash("Attack"));
                    }
                    break;
                case Note.D_:
                    break;
                case Note.D:
                    {
                        BattleReference.m_slime.GetComponent<Animator>().Play(Animator.StringToHash("Attack"));
                    }
                    break;
                case Note.E:
                    {
                        BattleReference.m_slime.GetComponent<Animator>().Play(Animator.StringToHash("Attack"));
                    }
                    break;
                case Note.F_:
                    break;
                case Note.F:
                    break;
                case Note.G:
                    break;
                default:
                    break;
            }
        }
        m_battleRef.SendMessage("ReceiveNote", a_note); //send it to everyone with a "PlayNote" method
        NoteVisualiser.Reference.ReceiveNote(a_note);
    }

    ///<summary> Recieves when the current turn ends through a bool and thus also knows who's turn it is</summary>
    /// <param name="a_playerTurn"></param>
    public void RecieveTurnOver(bool a_playerTurn)
    {
        m_playerTurn = a_playerTurn;
        GetComponent<SpellSystem>().TurnOver();
    }

    /// <summary>Called by spellsystem, Deals damage to character bassed on who's turn it is</summary>
    /// <param name="a_damage">Damage dealt to character</param>
    public void DealDamage(int a_damage)
    {
        if (m_playerTurn)
        {
            m_player.GetComponent<TheBard>().TakeDamage(a_damage);
            m_damageDisplay.TakeDamage(a_damage, m_player.transform.position,4);
        }
        else
        {
            m_slime.GetComponent<TheSlime>().TakeDamage(a_damage);
            m_damageDisplay.TakeDamage(a_damage, m_slime.transform.position, 1.15f);
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
