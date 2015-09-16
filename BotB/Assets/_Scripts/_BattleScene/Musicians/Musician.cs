using UnityEngine;
using System.Collections;

public class Musician : MonoBehaviour
{
    [SerializeField]
    protected string m_name;
    [SerializeField]
    protected string[] m_spellList = { "BBEDC", "DEBCA", "CDEBA", "AADEB" };//Cap the spell list for now to make it easier to work with
    [SerializeField]
    GameObject m_sceneHandler;
    [SerializeField]
    protected int m_health, m_defence, m_attack;
    [SerializeField]
    protected SpellType m_spellBehavior;

    private bool m_reset = true;
    private bool m_spellPlay = false;
    private float m_noteTime = -1;
    private static float turnTick = -1;
    private uint m_spellLoc = 0, m_noteCount = 0, m_notesPlayed = 0;
    
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
        if (m_health < 0)
            Die();
	}
    //Takes damage
    public virtual void TakeDamage(int a_damage)
    {
        m_health -= a_damage;
    }
    //
    private void SpellAI()
    {
        if (TurnTimer.Instance.CurrentTurn == Turn.Casting)
        {
            if (m_spellPlay)
            {
                m_reset = true;
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
        }
        else
        {
            if(m_reset)
            {
                SpellType toMake = SpellType.Offencive;
                switch (m_spellBehavior)
                {
                    case SpellType.Offencive:
                        PlaySpell(m_spellList[0]);
                        toMake = SpellType.Defensive;
                        break;
                    case SpellType.Defensive:
                        PlaySpell(m_spellList[0]);
                        toMake = SpellType.Effect;
                        break;
                    case SpellType.Effect:
                        PlaySpell(m_spellList[2]);
                        toMake = SpellType.Offencive;
                        break;
                    default:
                        break;
                }
                m_spellBehavior = toMake;
                m_reset = false;
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
    public int Health
    {
        get { return m_health; }
    }
    public int Defence
    {
        get { return m_defence; }
    }
    public int Attack
    {
        get { return m_attack; }
    }
    public SpellType CurrentSpellType
    {
        get { return m_spellBehavior; }
        set { m_spellBehavior = value; }
    }
}