using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;

struct SpellData
{
    Vector3 m_colour;
    int m_damage;
}
public class SpellSystem : MonoBehaviour 
{
    //Attributes
    [SerializeField]
    Dictionary<Note[], string> m_spellList = new Dictionary<Note[], string>(); //list of all the spells
    [SerializeField]
    List<Note> m_currentNotes; //list of notes played this turn
    [SerializeField]
    List<GameObject> m_emitters = new List<GameObject>(); //list of current spell effects
    float m_lifetime = 2.0f;
    [SerializeField]
    uint m_damage;
    //Behaviours
    void Start ()
    {
        Note[] IceBolt = new Note[] { Note.C, Note.D, Note.E, Note.B, Note.A };
        m_spellList.Add(IceBolt, "IceBolt");
        Note[] ArcaneMissile = new Note[] {Note.A, Note.A, Note.D, Note.E, Note.B};
        m_spellList.Add(ArcaneMissile, "ArcaneMissile");
        Note[] FireBolt = new Note[] {Note.B, Note.B, Note.E, Note.D, Note.C};
        m_spellList.Add(FireBolt, "FireBolt");
        Note[] WindBolt = new Note[] { Note.D, Note.E, Note.B, Note.C, Note.A };
        m_spellList.Add(WindBolt, "WindBolt");
        m_damage = 0;
	}
	
	// Update is called once per frame
	void Update () 
    {
        m_lifetime -= Time.deltaTime;

        if (m_lifetime <= 0)
        {
            var emitterEnumerator = m_emitters.GetEnumerator();
            while (emitterEnumerator.MoveNext())
            {
                //Turn all of the emitter's emissions off
                emitterEnumerator.Current.GetComponent<ParticleSystem>().enableEmission = false;
                m_damage += emitterEnumerator.Current.GetComponent<Spell>().m_damage;
            }
        }
        if (m_lifetime <= -1)
        {
            var emitterEnumerator = m_emitters.GetEnumerator();
            while (emitterEnumerator.MoveNext())
            {
                //Delete all the emitters
                Destroy(emitterEnumerator.Current.gameObject);
            }
            m_emitters.Clear();
            GetComponent<Battle>().DealDamage(m_damage);
            m_damage = 0;
        }
	}
    /// <summary> Receive's note from Battle.cs as a_note </summary>
    /// <param name="a_note"></param>
    void ReceiveNote (TimedNote a_note)
    {
        if (GetComponent<Battle>().m_playerTurn)
            m_currentNotes.Add(a_note.m_note);
    }
    /// <summary>casts spells, clears notes</summary>
    public void TurnOver()
    {
        CheckForSpell(m_currentNotes, m_spellList);
        m_currentNotes.Clear();
        m_lifetime = 2.0f;
    }
    /// <summary>Checks the played notes for spells</summary>
    /// <param name="a_CurrentNotes"></param>
    /// <param name="a_spellList"></param>
    void CheckForSpell(List<Note> a_currentNotes, Dictionary<Note[], string> a_spellList)
    {
        var spellEnumerator = a_spellList.GetEnumerator();
        while (spellEnumerator.MoveNext())
        {
            //Check the full list of notes 4 at a time
            for (int i = 0; i + 4 < a_currentNotes.Count; ++i)
            {
                Note[] currentSequence = new Note[] { a_currentNotes[i], a_currentNotes[i + 1], a_currentNotes[i + 2], a_currentNotes[i + 3], a_currentNotes[i + 4]};
                if (currentSequence.SequenceEqual(spellEnumerator.Current.Key))
                {
                    //Check what spell is being cast
                    switch (spellEnumerator.Current.Value)
                    {
                        case "ArcaneMissile":
                            {
                                if (GetComponent<Battle>().m_playerTurn)
                                { 
                                    m_emitters.Add((GameObject)Instantiate(Resources.Load("_Prefabs/ArcaneBolt"), new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0007f), 0.0f);
                                }
                                else
                                {
                                   m_emitters.Add((GameObject)Instantiate(Resources.Load("_Prefabs/ArcaneBolt"), new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f);
                                }
                                break;
                            }
                        case "FireBolt":
                            {
                                if (GetComponent<Battle>().m_playerTurn)
                                {
                                    m_emitters.Add((GameObject)Instantiate(Resources.Load("_Prefabs/FireBolt"), new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.007f), 0.0f);
                                }
                                else
                                {
                                    m_emitters.Add((GameObject)Instantiate(Resources.Load("_Prefabs/FireBolt"), new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f);
                                }
                                break;
                            }
                        case "IceBolt":
                            {
                                if (GetComponent<Battle>().m_playerTurn)
                                {
                                    m_emitters.Add((GameObject)Instantiate(Resources.Load("_Prefabs/IceBolt"), new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.007f), 0.0f);
                                }
                                else
                                {
                                    m_emitters.Add((GameObject)Instantiate(Resources.Load("_Prefabs/IceBolt"), new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f);
                                }
                                break; 
                            }
                        case "WindBolt":
                            {
                                if (GetComponent<Battle>().m_playerTurn)
                                { 
                                    m_emitters.Add((GameObject)Instantiate(Resources.Load("_Prefabs/WindBolt"), new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.007f), 0.0f);
                                }
                                else
                                {
                                    m_emitters.Add((GameObject)Instantiate(Resources.Load("_Prefabs/WindBolt"), new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_emitters.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f);
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
}
