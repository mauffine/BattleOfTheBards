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
public class Battle : MonoBehaviour 
{
    //Attributes
    static Battle s_battleRef;

    [SerializeField]
    public GameObject m_player;

    [SerializeField]
    public GameObject m_slime;
    [SerializeField]
    GameObject m_guiRef;
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
        s_battleRef = this;
        m_gameOverTimer = 1.5f;
        m_displayingText = false;
	}
	void Update() 
    {
        if (!m_playing)
        { 
            m_gameOverTimer -= Time.deltaTime;
            if (!m_displayingText)
                m_guiRef.GetComponentInChildren<Image>().enabled = true;
        }
        if (m_gameOverTimer <= 0)
            Application.Quit();
	}

    /// <summary> Receive valid key presses from InputManager and passes them out to all the relevant scripts if it's a note or quits and loads the menu if it's the escape button </summary>
    /// <param name="a_note"></param>
    static public void ReceiveKey(TimedNote a_note)
    {
        if (s_battleRef.PlayerTurn)
        {
            switch (a_note.m_note)
            {
                case Note.A_:
                    break;
                case Note.A:
                    {
                        BattleReference.m_player.GetComponent<Animator>().SetTrigger("Chord1");
                        //BattleReference.m_player.GetComponent<Animator>().Play(Animator.StringToHash("Chord1"));
                    }
                    break;
                case Note.B_:
                    break;
                case Note.B:
                    {
                        BattleReference.m_player.GetComponent<Animator>().SetTrigger("Chord2");
                    }
                    break;
                case Note.C:
                    {
                        BattleReference.m_player.GetComponent<Animator>().SetTrigger("Cord3");
                    }
                    break;
                case Note.D_:
                    break;
                case Note.D:
                    {
                        BattleReference.m_player.GetComponent<Animator>().SetTrigger("Chord1");
                    }
                    break;
                case Note.E:
                    {
                        BattleReference.m_player.GetComponent<Animator>().SetTrigger("Chord4");
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
                        BattleReference.m_slime.GetComponent<Animator>().SetTrigger("Attack");
                    }
                    break;
                case Note.B_:
                    break;
                case Note.B:
                    {
                        BattleReference.m_slime.GetComponent<Animator>().SetTrigger("Attack");
                    }
                    break;
                case Note.C:
                    {
                        BattleReference.m_slime.GetComponent<Animator>().SetTrigger("Attack");
                    }
                    break;
                case Note.D_:
                    break;
                case Note.D:
                    {
                        BattleReference.m_slime.GetComponent<Animator>().SetTrigger("Attack");
                    }
                    break;
                case Note.E:
                    {
                        BattleReference.m_slime.GetComponent<Animator>().SetTrigger("Attack");
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
        SendNote(a_note);
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

    static void SendNote(TimedNote a_note)//rename this later
    {
        SpellSystem.Reference.ReceiveNote(a_note);
        SoundManager.Reference.ReceiveNote(a_note);
        NoteVisualiser.Reference.ReceiveNote(a_note);
    }

    public static Battle BattleReference
    {
        get {return s_battleRef; }
    }

    public bool PlayerTurn
    {
        get { return m_playerTurn; }
    }
}
