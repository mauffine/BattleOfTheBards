using UnityEngine;
using System.Collections;

public class Musician : MonoBehaviour
{
    [SerializeField]
    uint m_health;
    [SerializeField]
    string m_name;
    [SerializeField]
    Spell[] m_spellList;//Cap the spell list for now to make it easier to work with
    [SerializeField]
    uint m_spellNum;

    protected bool m_spellPlay = false;
    protected float m_noteTime = -1;
    protected static float turnTick = -1;
    protected uint m_spellLoc = 0, m_noteCount = 0, m_notesPlayed = 0;
	// Use this for initialization
    protected void Start() 
    {
        turnTick = TurnTimer.TimePerTurn;
	}
	// Update is called once per frame
    protected void Update() 
    {
        if (m_spellPlay)
        {
            if (m_notesPlayed >= m_noteCount)
            {
                m_spellPlay = false;
                m_noteTime = (turnTick / m_noteCount);
            }
            if (m_noteTime > 0)
            {
                m_noteTime -= Time.deltaTime;
            }
            else
            {
                int index = (int)(m_noteCount - m_notesPlayed);
                Note toPlay = (Note)m_spellList[m_spellLoc].Key[index];
                Battle.ReceiveKey(new TimedNote(toPlay, Time.deltaTime));
                ++m_notesPlayed;
                m_noteTime = (turnTick / m_noteCount);
            }
        }
        if(m_health < 0)
        {
            Die();
        }
	}
    //Takes damage
    public virtual void TakeDamage(uint a_damage)
    {
        m_health -= a_damage;
    }
    //Playes a spell
    public virtual void PlaySpell(string a_spellKey)
    {
        for (uint I = 0; I < m_spellNum; ++I)
        {
            if(m_spellList[I].Key == a_spellKey)
            {
                m_spellPlay = true;
                m_spellLoc = I;
                m_noteCount = (uint)m_spellList[I].Key.Length;
                m_noteTime = (turnTick / m_noteCount);
            }
        }
    }
    //*dies
    protected void Die() 
    { 
    }
}