using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class SpellSystem : MonoBehaviour
{
    //Attributes
    List<TimedNote> m_playerNotes = new List<TimedNote>();
    [SerializeField]
    List<GameObject> m_playerSpells = new List<GameObject>();

    List<TimedNote> m_enemyNotes = new List<TimedNote>();
    [SerializeField]
    List<GameObject> m_enemySpells = new List<GameObject>();

    public List<GameObject> m_spellPrefabs = new List<GameObject>();

    Dictionary<string, Note[]> m_spellList = new Dictionary<string, Note[]>();

    int m_damage;
    float m_flightTime;
    public static SpellSystem Instance;
    //Behaviours
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Note[] IceBolt = new Note[] { Note.C, Note.D, Note.E, Note.B, Note.A };
        m_spellList.Add("IceBolt", IceBolt);
        Note[] ArcaneBolt = new Note[] { Note.A, Note.A, Note.D, Note.E, Note.B };
        m_spellList.Add("ArcaneBolt", ArcaneBolt);
        Note[] FireBolt = new Note[] { Note.B, Note.B, Note.E, Note.D, Note.C };
        m_spellList.Add("FireBolt", FireBolt);
        Note[] WindBolt = new Note[] { Note.D, Note.E, Note.B, Note.C, Note.A };
        m_spellList.Add("WindBolt", WindBolt);
        m_damage = 0;
        m_flightTime = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //(broken) after a time when the spells are cast deal damage to the appropriate characters and remove the instantiated spells from the scene
        if (TurnTimer.Instance.CurrentTurn == Turn.Menu)
            m_flightTime -= Time.deltaTime;
        if (m_flightTime <= 0 && TurnTimer.Instance.CurrentTurn == Turn.Menu)
        {
            if (m_playerSpells.Count != 0)
            {
                var playerSpellEnum = m_playerSpells.GetEnumerator();
                while (playerSpellEnum.MoveNext())
                {
                    m_damage = playerSpellEnum.Current.GetComponent<Spell>().Damage;
                    m_damage += m_playerNotes.Count;
                    Battle.Instance.DealDamage(m_damage, false);
                    m_damage = 0;
                    Destroy(playerSpellEnum.Current);
                }
                m_playerSpells.Clear();
            }
            else if (m_enemySpells.Count != 0)
            {
                var enemySpellEnum = m_enemySpells.GetEnumerator();
                while (enemySpellEnum.MoveNext())
                {
                    m_damage = enemySpellEnum.Current.GetComponent<Spell>().Damage;
                    m_damage += m_playerNotes.Count;
                    Battle.Instance.DealDamage(m_damage, true);
                    m_damage = 0;
                    Destroy(enemySpellEnum.Current);
                }
                m_enemySpells.Clear();
            }
            m_playerNotes.Clear();
            m_enemyNotes.Clear();
            m_flightTime = 1.0f;
        }

    }
    /// <summary> Receive's note from Battle.cs as a_note </summary>
    /// <param name="a_note"> The note to recieve</param>
    public void ReceiveKey(TimedNote a_note)
    {
        if (a_note.m_playerOwned)
            m_playerNotes.Add(a_note);
        else
            m_enemyNotes.Add(a_note);
    }
    public void CastSpells()
    {
        CheckSpells(m_playerNotes, m_spellList);
        CheckSpells(m_enemyNotes, m_spellList);
        m_flightTime = 1.0f;
    }
    void CheckSpells(List<TimedNote> a_currentNotes, Dictionary<string, Note[]> a_spellList)
    {
        var spellEnumerator = a_spellList.GetEnumerator();
        while (spellEnumerator.MoveNext())
        {
            //Check the full list of notes 4 at a time
            for (int i = 0; i + 4 < a_currentNotes.Count; ++i)
            {
                Note[] currentSequence = new Note[] { a_currentNotes[i].m_note, a_currentNotes[i + 1].m_note, a_currentNotes[i + 2].m_note, a_currentNotes[i + 3].m_note, a_currentNotes[i + 4].m_note };
                if (currentSequence.SequenceEqual(spellEnumerator.Current.Value))
                {
                    //Check what spell is being cast and by who
                    switch (spellEnumerator.Current.Key)
                    {
                        case "IceBolt":
                            {
                                if (a_currentNotes[i].m_playerOwned)
                                {
                                    m_playerSpells.Add((GameObject)Instantiate(m_spellPrefabs[1], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_playerSpells.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f) * 2;
                                }
                                else
                                {
                                    m_enemySpells.Add((GameObject)Instantiate(m_spellPrefabs[1], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_enemySpells.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0007f), 0.0f) * 2;
                                }
                                break;
                            }
                        case "ArcaneBolt":
                            {
                                if (a_currentNotes[i].m_playerOwned)
                                {
                                    m_playerSpells.Add((GameObject)Instantiate(m_spellPrefabs[2], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_playerSpells.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f) * 2;
                                }
                                else
                                {
                                    m_enemySpells.Add((GameObject)Instantiate(m_spellPrefabs[2], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_enemySpells.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.007f), 0.0f) * 2;
                                }
                                break;
                            }
                        case "Firebolt":
                            {
                                if (a_currentNotes[i].m_playerOwned)
                                {
                                    m_playerSpells.Add((GameObject)Instantiate(m_spellPrefabs[3], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_playerSpells.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f) * 2;
                                }
                                else
                                {
                                    m_enemySpells.Add((GameObject)Instantiate(m_spellPrefabs[3], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_enemySpells.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.007f), 0.0f) * 2;
                                }
                                break;
                            }
                        case "WindBolt":
                            {
                                if (a_currentNotes[i].m_playerOwned)
                                {
                                    m_playerSpells.Add((GameObject)Instantiate(m_spellPrefabs[4], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up)));
                                    m_playerSpells.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f) * 2;
                                }
                                else
                                {
                                    m_enemySpells.Add((GameObject)Instantiate(m_spellPrefabs[4], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up)));
                                    m_enemySpells.Last<GameObject>().GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.007f), 0.0f) * 2;
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
