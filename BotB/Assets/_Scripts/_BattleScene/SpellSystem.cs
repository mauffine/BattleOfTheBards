using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class SpellSystem : MonoBehaviour
{
    //Attributes
    List<TimedNote> m_playerNotes = new List<TimedNote>();
    [SerializeField]
    GameObject m_playerSpell;

    List<TimedNote> m_enemyNotes = new List<TimedNote>();
    [SerializeField]
    GameObject m_enemySpell;

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
        for (int i = 0; i < m_spellPrefabs.Count; ++i)
        {
            m_spellList.Add(m_spellPrefabs[i].GetComponent<Spell>().Name, m_spellPrefabs[i].GetComponent<Spell>().Key);
        }
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
            if (m_playerSpell != null)
            {
                m_damage = m_playerSpell.GetComponent<Spell>().Damage;
                m_damage += m_playerNotes.Count;
                Battle.Instance.DealDamage(m_damage, false);
                m_damage = 0;
                Destroy(m_playerSpell);
                m_playerSpell = null;
            }
            if (m_enemySpell != null)
            {
                m_damage = m_enemySpell.GetComponent<Spell>().Damage;
                m_damage += m_enemyNotes.Count;
                Battle.Instance.DealDamage(m_damage, true);
                m_damage = 0;
                Destroy(m_enemySpell);
                m_enemySpell = null;
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
            for (int i = 0; i + 4 < a_currentNotes.Count; ++i)
            {
                Note[] currentSequence = new Note[] { a_currentNotes[i].m_note, a_currentNotes[i + 1].m_note, a_currentNotes[i + 2].m_note, a_currentNotes[i + 3].m_note, a_currentNotes[i + 4].m_note };
                if (currentSequence.SequenceEqual(spellEnumerator.Current.Value))
                {
                    for (int o = 0; o < m_spellPrefabs.Count; ++o)
                    {
                        if (spellEnumerator.Current.Key == m_spellPrefabs[o].GetComponent<Spell>().Name)
                        {
                            if (a_currentNotes[i].m_playerOwned)
                            {
                                m_playerSpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up));
                                m_playerSpell.GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0f), 0.0f) * 2;
                            }
                            else
                            {
                                m_enemySpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up));
                                m_enemySpell.GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0007f), 0.0f) * 2;
                            }
                        }
                    }
                }
            }
        }
    }
}