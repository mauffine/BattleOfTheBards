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

    [SerializeField]
    float m_accuracy;
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
        m_accuracy = 0; // this should always be out of 100;
        m_flightTime = 0.7f;
    }

    // Update is called once per frame
    void Update()
    {

        if (TurnTimer.Instance.CurrentTurn == Turn.Menu)
            m_flightTime -= Time.deltaTime;

        if (m_flightTime <=0 && TurnTimer.Instance.CurrentTurn == Turn.Menu)
        {
            PaperScissorsRock();
        }

    }
    /// <summary> Receive's note from Battle.cs as a_note </summary>
    /// <param name="a_note"> The note to recieve</param>
    public void ReceiveKey(TimedNote a_note)
    {
        if (a_note.m_playerOwned)
        {
            m_playerNotes.Add(a_note);
            float noteTime = (a_note.m_time + 0.35f) * 95.0f * (1.0f / 60.0f);//gives how far off the beat that the note was played and which beat it was played on
            float noteAccuracy = noteTime % 1; //converts the accuracy into a float from 0-1, the closer to 1 or 0 the closer to the beat the note was played
            if (noteAccuracy > .5f) // convert the accuracy to a value between 0 and 0.5, the higher the more accurate
            {
                noteAccuracy -= .5f;
            }
            else
            {
                noteAccuracy = .5f - noteAccuracy;
            }
            noteAccuracy = ((noteAccuracy / .5f) * 100.0f); //convert it into a percentage
            m_accuracy += noteAccuracy / 5; //divided by the amount of notes in the spell
            //TODO: give a visual cue to how well the player has played
            if (noteAccuracy > 40)//okay
            {
                if (noteAccuracy > 60)//good
                {
                    if (noteAccuracy > 80)//great
                    {
                        if (noteAccuracy > 90)//perfect
                        {
                            //show perfect
                        }
                        else
                        {
                            //show great
                        }
                    }
                    else
                    {
                        //show good
                    }
                }
                else
                {
                    //show okay
                }
            }
        }
        else
            m_enemyNotes.Add(a_note);
    }
    /// <summary>Called at the end of the casting turn to check what spells have been cast</summary>
    public void CastSpells()
    {
        CheckSpells(m_playerNotes, m_spellList);
        CheckSpells(m_enemyNotes, m_spellList);
        m_flightTime = 1.0f;
    }
    /// <summary>checks if a spell has been cast in the list that's passed into this function</summary>
    /// <param name="a_currentNotes">List of notes to check for spells</param>
    /// <param name="a_spellList">dictionary list of spells that exist in the game, can be replaced by a limited list for spells known</param>
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
                                if (m_accuracy >= 50)
                                {
                                    switch (m_spellPrefabs[o].GetComponent<Spell>().Type)
                                    {
                                        case (SpellType.Offencive):
                                            {
                                                if (SpellMenu.Selection == SpellType.Offencive)
                                                {
                                                    m_playerSpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up));
                                                    m_playerSpell.GetComponent<Spell>().m_velocity = new Vector3(-0.04f, Random.Range(-0.00007f, 0.00007f), 0.0f) * 2;
                                                }
                                                break;
                                            }
                                        case SpellType.Defensive:
                                            {
                                                if (SpellMenu.Selection == SpellType.Defensive)
                                                {
                                                    m_playerSpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(1.5f, 1, 1.3f), Quaternion.AngleAxis(0, Vector3.up));
                                                    m_playerSpell.GetComponent<Spell>().m_velocity = Vector3.zero;
                                                }
                                                break;
                                            }
                                        case SpellType.Effect:
                                            {
                                                if (SpellMenu.Selection == SpellType.Effect)
                                                {
                                                    m_playerSpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(2, 0, 1), Quaternion.AngleAxis(0, Vector3.up));
                                                    m_playerSpell.GetComponent<Spell>().m_velocity = Vector3.zero;
                                                }
                                                break;
                                            }
                                    }
                                    
                                }
                            }
                            else
                            {
                                
                                switch (m_spellPrefabs[o].GetComponent<Spell>().Type)
                                {
                                    case (SpellType.Offencive):
                                        {
                                            m_enemySpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(-1.2f, 1f, 1.1f), Quaternion.AngleAxis(180, Vector3.up));
                                            m_enemySpell.GetComponent<Spell>().m_velocity = new Vector3(-.03f, Random.Range(-.007f, 0.0007f), 0.0f) * 2;
                                            break;
                                        }
                                    case SpellType.Defensive:
                                        {
                                            m_enemySpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(-1, 1, 1.2f), Quaternion.AngleAxis(135, Vector3.up));
                                            m_enemySpell.GetComponent<Spell>().m_velocity = Vector3.zero;
                                            break;
                                        }
                                    case SpellType.Effect:
                                        {
                                            m_enemySpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(-1.5f, 1, 1), Quaternion.AngleAxis(0, Vector3.up));
                                            m_enemySpell.GetComponent<Spell>().m_velocity = Vector3.zero;
                                            break;
                                        }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    /// <summary>Spells effect each character depending on what type they are</summary>
    void PaperScissorsRock()
    {
        if (m_playerSpell != null && m_enemySpell != null)
        {
            switch (m_playerSpell.GetComponent<Spell>().Type) //paper, scissors, rock aka Attack, Defense, Effect
            {
                case SpellType.Offencive:
                    {
                        if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Offencive)
                            Attack_Attack();
                        else if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Defensive)
                            Attack_Defence();
                        else
                            Attack_Effect();
                        }
                    break;
                case SpellType.Defensive:
                    {
                        if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Offencive)
                            Defence_Attack();
                        else if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Defensive)
                            Defence_Defence();
                        else
                            Defence_Effect();
                    }
                    break;
                case SpellType.Effect:
                    {
                        if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Offencive)
                            Effect_Attack();
                        else if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Defensive)
                            Effect_Defence();
                        else
                            Effect_Effect();
                    }
                    break;
            }
        }
        else
        {
            if (m_playerSpell != null)
            {
                Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);
                Destroy(m_playerSpell);
                m_playerSpell = null;
            }
            if (m_enemySpell != null)
            {
                Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);
                Destroy(m_enemySpell);
                m_enemySpell = null;
            }
            m_playerNotes.Clear();
            m_enemyNotes.Clear();
            m_flightTime = 0.7f;
            m_accuracy = 0;
        }
    }
    void Attack_Attack()
    {
        Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);

        Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);
        Destroy(m_playerSpell);
        m_playerSpell = null;
        
        Destroy(m_enemySpell);
        m_enemySpell = null;

        Debug.Log("Both attacks clash!");
    }
    void Attack_Defence()
    {
        Battle.Instance.DealDamage(m_playerNotes.Count, false);

        Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);
        
        Destroy(m_playerSpell);
        m_playerSpell = null;

        Destroy(m_enemySpell);
        m_enemySpell = null;
        Debug.Log("Your spell was deflected!");
    }
    void Attack_Effect()
    {
        Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);

        Battle.Instance.DealDamage(m_enemyNotes.Count, true);

        Destroy(m_playerSpell);
        m_playerSpell = null;

        Destroy(m_enemySpell);
        m_enemySpell = null;

        Debug.Log("Enemy's effect was blown away!");
    }
    void Defence_Attack()
    {
        Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);

        Battle.Instance.DealDamage(m_enemyNotes.Count, true);

        Destroy(m_enemySpell);
        m_enemySpell = null;

        Destroy(m_playerSpell);
        m_playerSpell = null;

        Debug.Log("Enemy spell deflected!");
    }
    void Defence_Defence()
    {
        Battle.Instance.DealDamage(m_playerNotes.Count, false);

        Battle.Instance.DealDamage(m_enemyNotes.Count, true);

        Destroy(m_enemySpell);
        m_enemySpell = null;

        Destroy(m_playerSpell);
        m_playerSpell = null;

        Debug.Log("Both of you defend...");
    }
    void Defence_Effect()
    {
        Battle.Instance.DealDamage(m_playerNotes.Count, false);

        Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);

        Destroy(m_playerSpell);
        m_playerSpell = null;

        Destroy(m_enemySpell);
        m_enemySpell = null;

        Debug.Log("The enemy's effect passes through your defences!");
    }
    void Effect_Attack()
    {
        Battle.Instance.DealDamage(m_playerNotes.Count, false);
        
        Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);

        Destroy(m_playerSpell);
        m_playerSpell = null;

        Destroy(m_enemySpell);
        m_enemySpell = null;

        Debug.Log("Your effect was blown away by the enemy attack!");
    }
    void Effect_Defence()
    {

        Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);

        Battle.Instance.DealDamage(m_enemyNotes.Count, true);

        Destroy(m_playerSpell);
        m_playerSpell = null;

        Destroy(m_enemySpell);
        m_enemySpell = null;

        Debug.Log("Your effect passed through the enemy's defences!");
    }
    void Effect_Effect()
    {
        Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);

        Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);

        Destroy(m_playerSpell);
        m_playerSpell = null;
        
        Destroy(m_enemySpell);
        m_enemySpell = null;

        Debug.Log("Both effects clash!");
    }
}