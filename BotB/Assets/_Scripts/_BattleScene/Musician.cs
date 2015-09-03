using UnityEngine;
using System.Collections;

public struct Stats
{
    private int health, str, def, dex, time;
    public Stats(int a_health = 0, int a_str = 0, int a_def = 0, int a_dex = 0, int a_time = 0)
    {
        health = a_health;
        str = a_str;
        def = a_def;
        dex = a_dex;
        time = a_time;
    }
    public int Damage
    {
        get { return -health; }
        set { health = value;}
    }
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    public int Str
    {
        get { return str; }
        set { str = value; }
    }
    public int Def
    {
        get { return def; }
        set { def = value; }
    }
    public int Dex
    {
        get { return dex; }
        set { dex = value; }
    }
    public int TimeMod
    {
        get { return time; }
        set { time = value; }

    }

    public static Stats operator +(Stats a_LHS, Stats a_RHS)
    {
        Stats returnVal = new Stats(a_LHS.health + a_RHS.Health, a_LHS.str + a_RHS.Str, a_LHS.def + a_RHS.Def, a_LHS.dex + a_RHS.Dex, a_LHS.time + a_RHS.TimeMod);
        return returnVal;
    }
}
public class Musician : MonoBehaviour
{
    [SerializeField]
    string m_name;
    //[SerializeField]
    string[] m_spellList = {"BBEDC", "DEBCA", "CDEBA", "AADEB"};//Cap the spell list for now to make it easier to work with
    [SerializeField]
    GameObject m_sceneHandler;
    [SerializeField]
    protected Stats m_stats;
    protected Stats m_mods;

    protected bool m_spellPlay = false;
    protected float m_noteTime = -1;
    protected static float turnTick = -1;
    protected uint m_spellLoc = 0, m_noteCount = 0, m_notesPlayed = 0;


	// Use this for initialization
    protected void Start() 
    {
        turnTick = TurnTimer.Instance.CastingTime - 1;
	}
	// Update is called once per frame
    protected void Update() 
    {
        //choose a spell and play a spell
        SpellAI();
        if (m_stats.Health < 0)
            Die();
	}
    //Takes damage
    public virtual void TakeDamage(int a_damage)
    {
        m_stats.Health += a_damage;
    }
    //
    private void SpellAI()
    {
        if (TurnTimer.Instance.CurrentTurn == Turn.Casting)
        {
            if (m_spellPlay)
            {
                if (m_notesPlayed >= m_noteCount)
                {
                    m_spellPlay = false;
                    m_noteTime = (turnTick / m_noteCount);
                }
                else if (m_noteTime > 0)
                {
                    m_noteTime -= Time.deltaTime;
                }
                else
                {
                    //set toPlay to something
                    Note toPlay = (Note)m_spellList[m_spellLoc][(int)m_notesPlayed];
                    //int index = (int)(m_noteCount - m_notesPlayed);
                    //convert characters into notes
                    switch (m_spellList[m_spellLoc][(int)m_notesPlayed])
                    {
                        case 'A':
                            {
                                toPlay = Note.A;
                                break;
                            }
                        case 'B':
                            {
                                toPlay = Note.B;
                                break;
                            }
                        case 'C':
                            {
                                toPlay = Note.C;
                                break;
                            }
                        case 'D':
                            {
                                toPlay = Note.D;
                                break;
                            }
                        case 'E':
                            {
                                toPlay = Note.E;
                                break;
                            }
                        default:
                            break;
                    }
                    Battle.Instance.ReceiveKey(new TimedNote(toPlay, Time.deltaTime, false));
                    ++m_notesPlayed;
                    m_noteTime = (turnTick / m_noteCount);
                }
            }
            else
            {
                int rand = Random.Range(0, m_spellList.Length);
                PlaySpell(m_spellList[rand]);
            }
        }
            
    }
    //Playes a spell
    public virtual void PlaySpell(string a_spellKey)
    {
        for (uint I = 0; I < m_spellList.Length; ++I)
        {
            if (m_spellList[I] == a_spellKey)
            {
                m_spellPlay = true;
                m_spellLoc = I;
                m_noteCount = (uint)m_spellList[I].Length;
                m_noteTime = (turnTick / m_noteCount);
                m_notesPlayed = 0;
            }
        }
    }
    //*dies
    protected virtual void Die() 
    { 
    }
    public Stats Statistics
    {
        get { return m_stats; }
    }
    public Stats CurrentModifers
    {
        get { return m_mods; }
    }

}