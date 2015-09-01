using UnityEngine;
using System.Collections;

public class Musician : MonoBehaviour
{
    [SerializeField]
    string m_name;
    //[SerializeField]
    string[] m_spellList = {"BBEDC", "DEBCA", "CDEBA", "AADEB"};//Cap the spell list for now to make it easier to work with
    [SerializeField]
    GameObject m_sceneHandler;
    [SerializeField]
    protected float m_attack, m_defence = 0, m_health;

    protected bool m_spellPlay = false;
    protected float m_noteTime = -1;
    protected static float s_turnTick = -1;
    protected uint m_spellLoc = 0, m_noteCount = 0, m_notesPlayed = 0;


	// Use this for initialization
    protected void Start() 
    {
        s_turnTick = TurnTimer.TimePerTurn - 1;
        m_attack = 0;
        Active = true;
	}
	//Update is called once per frame
    protected void Update() 
    {
        if(Active)
        { 
            //choose a spell and play a spell
            if (!m_sceneHandler.GetComponent<Battle>().m_playerTurn)
                SpellAI();
            else
                m_spellPlay = false;
            if (m_health < 0)
                Die();
        }
	}
    //Takes damage
    public virtual void TakeDamage(int a_damage)
    {
        m_health += ( a_damage);//USE DEFENCE AND PASS THROUGH POSITIVE NUMBERS
    }
    //
    private void SpellAI()
    {
        if (m_spellPlay)
        {
            if (m_notesPlayed >= m_noteCount)
            {
                m_spellPlay = false;
                m_noteTime = (s_turnTick / m_noteCount);
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
                Battle.ReceiveKey(new TimedNote(toPlay, Time.deltaTime));
                ++m_notesPlayed;
                m_noteTime = (s_turnTick / m_noteCount);
            }
        }
        else
        {
            int rand = Random.Range(0, m_spellList.Length - 1);
            PlaySpell(m_spellList[rand]);
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
                m_noteTime = (s_turnTick / m_noteCount);
                m_notesPlayed = 0;
            }
        }
    }
    //*dies
    protected virtual void Die() 
    {
        Active = false;
    }

    public bool Active
    {
        get;
        set;
    }
}