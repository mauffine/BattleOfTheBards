using UnityEngine;
using System.Collections;

public class Musician : MonoBehaviour
{
    [SerializeField]
    int m_health;
    [SerializeField]
    string m_name;
    //[SerializeField]
    string[] m_spellList = {"BBEDC", "DEBCA", "CDEBA", "AADEB"};//Cap the spell list for now to make it easier to work with
    [SerializeField]
    GameObject m_sceneHandler;

    protected bool m_spellPlay = false;
    protected float m_noteTime = -1;
    protected static float turnTick = -1;
    protected uint m_spellLoc = 0, m_noteCount = 0, m_notesPlayed = 0;

	// Use this for initialization
    protected void Start() 
    {
        turnTick = TurnTimer.TimePerTurn - 1;
	}
	// Update is called once per frame
    protected void Update() 
    {
        //choose a spell and choose a spell
        
        if (!m_sceneHandler.GetComponent<Battle>().m_playerTurn)
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
                    Note toPlay = (Note)m_spellList[m_spellLoc][(int)m_notesPlayed];
                    //int index = (int)(m_noteCount - m_notesPlayed);
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
                    Battle.ReceiveKey(new TimedNote(toPlay, Time.deltaTime));
                    ++m_notesPlayed;
                    m_noteTime = (turnTick / m_noteCount);
                }
            }
            else
            {
                int rand = Random.Range(0, m_spellList.Length - 1);
                PlaySpell(m_spellList[rand]);
            }
                
        }
        else
        {
            m_spellPlay = false;
        }
        if(m_health < 0)
        {
            Die();
        }
	}
    //Takes damage
    public virtual void TakeDamage(uint a_damage)
    {
        m_health -= (int)a_damage;
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
    protected void Die() 
    { 

    }
    public int Health
    {
        get { return m_health; }
    }
}