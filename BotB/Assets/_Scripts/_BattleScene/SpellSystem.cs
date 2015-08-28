﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class SpellSystem : MonoBehaviour 
{
    //Attributes
    static SpellSystem s_spellSysRef;
    [SerializeField]
    Dictionary<Note[], string> m_spellList = new Dictionary<Note[], string>(); //list of all the spells
    [SerializeField]
    List<TimedNote> m_currentNotes = new List<TimedNote>(); //list of notes played this turn
    [SerializeField]
    List<GameObject> m_emitters = new List<GameObject>(); //list of current spell effects
    float m_lifetime = 0.7f;
    public List<GameObject> m_spellPrefabs = new List<GameObject>();

    [SerializeField]
    List<GameObject> m_noteSpells = new List<GameObject>();

    List<TimedNote> m_notesPlayedLast = new List<TimedNote>();

    List<TimedNote> m_playerNotes = new List<TimedNote>();
    List<TimedNote> m_enemyNotes = new List<TimedNote>();
    [SerializeField]
    int m_damage; //damage sum for when damage is dealt
    //Behaviours
    void Start ()
    {
        //create the spells
        Note[] IceBolt = new Note[] { Note.C, Note.D, Note.E, Note.B, Note.A };
        m_spellList.Add(IceBolt, "IceBolt");
        Note[] ArcaneBolt = new Note[] {Note.A, Note.A, Note.D, Note.E, Note.B};
        m_spellList.Add(ArcaneBolt, "ArcaneBolt");
        Note[] FireBolt = new Note[] {Note.B, Note.B, Note.E, Note.D, Note.C};
        m_spellList.Add(FireBolt, "FireBolt");
        Note[] WindBolt = new Note[] { Note.D, Note.E, Note.B, Note.C, Note.A };
        m_spellList.Add(WindBolt, "WindBolt");
        m_damage = 0;
        s_spellSysRef = this;
	}
	
	// Update is called once per frame
	void Update()
    {
        m_lifetime -= Time.deltaTime;
        if (m_emitters.Count > -0.7f)
            m_lifetime -= Time.deltaTime;

        if (m_lifetime <= -0.7f)
        {
            var emitterEnumerator = m_emitters.GetEnumerator();
            while (emitterEnumerator.MoveNext())
            {
                //Sum up damage of the spells before deletion;
                m_damage += emitterEnumerator.Current.GetComponent<Spell>().Damage;
                //Delete current spell
                Destroy(emitterEnumerator.Current.gameObject);
            }
            if (m_emitters.Count != 0)
            {
                if (Battle.BattleReference.PlayerTurn)
                {
                    Battle.BattleReference.m_player.GetComponent<Animator>().SetTrigger("Hurt");
                }
                else
                    Battle.BattleReference.m_slime.GetComponent<Animator>().SetTrigger("Hurt");
            }
            if (m_notesPlayedLast.Count != 0)
            {

                foreach (TimedNote note in m_notesPlayedLast)
                {
                    float noteTime = note.m_time * 120.0f * (1.0f / 60.0f);
                    if (noteTime % 1 >= 0.9f || noteTime % 1 <= 0.1f)
                        m_damage -= 2;
                    else
                        m_damage -= 1;
                }
                GetComponent<Battle>().DealDamage(m_damage);
                m_notesPlayedLast.Clear();
            }
            m_emitters.Clear();
            m_damage = 0;
        }

	}
    /// <summary> Receive's note from Battle.cs as a_note </summary>
    /// <param name="a_note"> The note to recieve</param>
    public void ReceiveNote (TimedNote a_note)
    {

        m_currentNotes.Add(a_note);
        if (a_note.m_playerOwned)
        {
            m_playerNotes.Add(a_note);
            GameObject temp = (GameObject)Instantiate(m_spellPrefabs[0], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up));
            temp.GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f);
        }
        else
        {
            m_enemyNotes.Add(a_note);
            GameObject temp = (GameObject)Instantiate(m_spellPrefabs[0], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up));
            temp.GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0007f), 0.0f);
        }
    }
    /// <summary>casts spells, clears notes and resets the lifetime of the spells in flight</summary>
    public void TurnOver()
    {
        //CheckForSpell(m_currentNotes, m_spellList);
        CheckForSpell(m_playerNotes, m_spellList);
        CheckForSpell(m_enemyNotes, m_spellList);

        m_notesPlayedLast.AddRange(m_currentNotes);
        m_currentNotes.Clear();
        m_enemyNotes.Clear();
        m_playerNotes.Clear();
        m_lifetime = 0.7f;
    } 
    /// <summary>Checks the played notes for spells</summary>
    /// <param name="a_CurrentNotes">The list of notes played this turn</param>
    /// <param name="a_spellList">The list of Spells that can be played and their note comninations</param>
    void CheckForSpell(List<TimedNote> a_currentNotes, Dictionary<Note[], string> a_spellList)
    {
        var spellEnumerator = a_spellList.GetEnumerator();
        while (spellEnumerator.MoveNext())
        {
            //Check the full list of notes 4 at a time
            for (int i = 0; i + 4 < a_currentNotes.Count; ++i)
            {
                Note[] currentSequence = new Note[] { a_currentNotes[i].m_note, a_currentNotes[i + 1].m_note, a_currentNotes[i + 2].m_note, a_currentNotes[i + 3].m_note, a_currentNotes[i + 4].m_note };
                if (currentSequence.SequenceEqual(spellEnumerator.Current.Key))
                {
                    //Check what spell is being cast and by who
                    switch (spellEnumerator.Current.Value)
                    {
                        case "IceBolt":
                            {
                                if (a_currentNotes[i].m_playerOwned)
                                {
                                    m_emitters.Add((GameObject)Instantiate(m_spellPrefabs[1], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f) * 2;
                                }
                                else
                                {
                                    m_emitters.Add((GameObject)Instantiate(m_spellPrefabs[1], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0007f), 0.0f) * 2;
                                }
                                break;
                            }
                        case "ArcaneBolt":
                            {
                                if (a_currentNotes[i].m_playerOwned)
                                {
                                    m_emitters.Add((GameObject)Instantiate(m_spellPrefabs[2], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f) * 2;
                                }
                                else
                                {
                                    m_emitters.Add((GameObject)Instantiate(m_spellPrefabs[2], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.007f), 0.0f) * 2;
                                }
                                break;
                            }
                        case "Firebolt":
                            {
                                if (a_currentNotes[i].m_playerOwned)
                                {
                                    m_emitters.Add((GameObject)Instantiate(m_spellPrefabs[3], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f) * 2;
                                }
                                else
                                {
                                    m_emitters.Add((GameObject)Instantiate(m_spellPrefabs[3], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.007f), 0.0f) * 2;
                                }
                                break; 
                            }
                        case "WindBolt":
                            {
                                if (a_currentNotes[i].m_playerOwned)
                                {
                                    m_emitters.Add((GameObject)Instantiate(m_spellPrefabs[4], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f) * 2;
                                }
                                else
                                {
                                    m_emitters.Add((GameObject)Instantiate(m_spellPrefabs[4], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.007f), 0.0f) * 2;
                                }
                                break;
                            }
                            
                        default:
                            break;
                    }
                    
                }
            }
        }
    }

    public static SpellSystem Reference
    {
        get { return s_spellSysRef; }
    }
}
