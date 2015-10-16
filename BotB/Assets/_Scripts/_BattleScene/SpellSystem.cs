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

    float m_spellDeletionTimer;
    bool m_spellInFlight;

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

        if (m_flightTime <= 0 && TurnTimer.Instance.CurrentTurn == Turn.Menu && m_spellInFlight == true)
        {
            PaperScissorsRock();
        }

        m_spellDeletionTimer -= Time.deltaTime;
        if(m_spellDeletionTimer < 0 && (m_playerSpell != null || m_enemySpell != null))
        {
            Destroy(m_playerSpell);
            m_playerSpell = null;

            Destroy(m_enemySpell);
            m_enemySpell = null;
        }

    }
    /// <summary> Receive's note from Battle.cs as a_note </summary>
    /// <param name="a_note"> The note to recieve</param>
    public void ReceiveKey(TimedNote a_note)
    {
        if (a_note.m_playerOwned)
        {
            m_playerNotes.Add(a_note);

            CheckAccuracy(a_note.m_time);
        }
        else
            m_enemyNotes.Add(a_note);
    }
    ///<summary>Called at the end of the casting turn to check what spells have been cast</summary>
    public void CastSpells()
    {
        CheckSpells(m_playerNotes, m_spellList);
        CheckSpells(m_enemyNotes, m_spellList);
        m_flightTime = 0.7f;
        m_spellDeletionTimer = 3;
        m_spellInFlight = true;
    }
    /// <summary>checks if a spell has been cast in the list that's passed into this function</summary>
    /// <param name="a_currentNotes">List of notes to check for spells</param>
    /// <param name="a_spellList">dictionary list of spells that exist in the game, can be replaced by a limited list for spells known</param>
    void CheckSpells(List<TimedNote> a_currentNotes, Dictionary<string, Note[]> a_spellList)
    {
        var spellEnumerator = a_spellList.GetEnumerator();
        while (spellEnumerator.MoveNext())
        {
            //run through the list of notes in a_current and check in groups of 5 for spells
            for (int i = 0; i + 4 < a_currentNotes.Count; ++i) 
            {
                //the group of five notes that we're currently checking 
                Note[] currentSequence = new Note[] { a_currentNotes[i].m_note, a_currentNotes[i + 1].m_note, a_currentNotes[i + 2].m_note, 
                    a_currentNotes[i + 3].m_note, a_currentNotes[i + 4].m_note }; 

                if (currentSequence.SequenceEqual(spellEnumerator.Current.Value))
                {
                    //run through list of available spells and get the prefab for the spell that was cast
                    for (int o = 0; o < m_spellPrefabs.Count; ++o)
                    {
                        //get that prefab and check if the player cast it
                        if (spellEnumerator.Current.Key == m_spellPrefabs[o].GetComponent<Spell>().Name && 
                            a_currentNotes[i].m_playerOwned && m_accuracy >= 50)
                        {
                            //instantiate it as the spell type that was selected
                            switch (m_spellPrefabs[o].GetComponent<Spell>().Type)
                            {
                                //Instantiates the spell in a position appropriate to the player and sets it's velocity
                                case (SpellType.Offencive):
                                    {
                                        //if (SpellMenu.Selection == SpellType.Offencive)
                                        {
                                            m_playerSpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(2, 1.3f, 1), Quaternion.AngleAxis(0, Vector3.up));
                                            m_playerSpell.GetComponent<Spell>().m_velocity = new Vector3(-0.03f, Random.Range(-0.00007f, 0.00007f), 0.0f) * 2;
                                        }
                                        break;
                                    }
                                case SpellType.Defensive:
                                    {
                                        //if (SpellMenu.Selection == SpellType.Defensive)
                                        {
                                            m_playerSpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(1.5f, 1, 1.3f), Quaternion.AngleAxis(0, Vector3.up));
                                            m_playerSpell.GetComponent<Spell>().m_velocity = Vector3.zero;
                                        }
                                        break;
                                    }
                                case SpellType.Effect:
                                    {
                                        //if (SpellMenu.Selection == SpellType.Effect)
                                        {
                                            m_playerSpell = (GameObject)Instantiate(m_spellPrefabs[o], new Vector3(2, 0, 1), Quaternion.AngleAxis(0, Vector3.up));
                                            m_playerSpell.GetComponent<Spell>().m_velocity = Vector3.zero;
                                        }
                                        break;
                                    }                            
                                }
                         }
                        else if (spellEnumerator.Current.Key == m_spellPrefabs[o].GetComponent<Spell>().Name && !a_currentNotes[i].m_playerOwned)
                        {
                            Battle.Instance.EnemyRef.GetComponent<Musician>().Animate(4);
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
    ///<summary>Spells effect each character depending on what type they are</summary>
    void PaperScissorsRock()
    {
        //both characters cast a spell
        if (m_playerSpell != null && m_enemySpell != null) 
        {
            //check the spells against each other
            switch (m_playerSpell.GetComponent<Spell>().Type)
            {
                //player casts an offencive spell
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
                //player casts a defensive spell
                case SpellType.Defensive: 
                    {
                        if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Offencive)
                            Attack_Defence(false);

                        else if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Defensive)
                            Defence_Defence();

                        else
                            Defence_Effect();
                    }
                    break;
                //player casts an effect spell
                case SpellType.Effect: 
                    {
                        if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Offencive)
                            Attack_Effect(false);

                        else if (m_enemySpell.GetComponent<Spell>().Type == SpellType.Defensive)
                            Defence_Effect(false);

                        else
                            Effect_Effect();
                    }
                    break;
            }

            m_playerSpell.GetComponent<Spell>().TurnOffEmission();
            m_enemySpell.GetComponent<Spell>().TurnOffEmission();
        }
        else
        {
            if (m_playerSpell != null)
            {
                Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);

                //Destroy(m_playerSpell);
                //m_playerSpell = null;
                m_playerSpell.GetComponent<Spell>().TurnOffEmission();
            }
            if (m_enemySpell != null)
            {
                Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);
                //Destroy(m_enemySpell);
                //m_enemySpell = null;
                m_enemySpell.GetComponent<Spell>().TurnOffEmission();
            }
            
            m_flightTime = 0.7f;
            m_accuracy = 0;
        }

        m_playerNotes.Clear();
        m_enemyNotes.Clear();
        m_spellInFlight = false;
    }
    void Attack_Attack()
    {
        //both attacks hit their intended targets and do damage
        Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);
        Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);

        Debug.Log("Both attacks clash!");
    }
    void Attack_Defence(bool a_playerAttacks = true)
    {
        //the attack is reflected back to the 
        if (a_playerAttacks)
        {
            Battle.Instance.DealDamage(m_playerNotes.Count, false, false);
            Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);
        }
        else
        {
            Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);
            Battle.Instance.DealDamage(m_enemyNotes.Count, true, false);
        }
		
        Debug.Log("Your spell was deflected!");
    }
    void Attack_Effect(bool a_playerAttacks = true)
    {
        if (a_playerAttacks)
        {
            Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);
            Battle.Instance.DealDamage(m_enemyNotes.Count, true, false);
        }
        else
        {
            Battle.Instance.DealDamage(m_playerNotes.Count, false, false);
            Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);
        }
        Debug.Log("Enemy's effect was blown away!");
    }
    void Defence_Defence()
    {
        Battle.Instance.DealDamage(m_playerNotes.Count, false, false);
        Battle.Instance.DealDamage(m_enemyNotes.Count, true, false);

        Debug.Log("Both of you defend...");
    }
    void Defence_Effect(bool a_playerDefends = true)
    {
        if (a_playerDefends)
        {
            Battle.Instance.DealDamage(m_playerNotes.Count, false, false);
            Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);
        }
        else
        {
            Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);
            Battle.Instance.DealDamage(m_enemyNotes.Count, true, false);
        }

        Debug.Log("The enemy's effect passes through your defences!");
    }
    void Effect_Effect()
    {
        Battle.Instance.DealDamage(m_playerSpell.GetComponent<Spell>().Damage + m_playerNotes.Count, false);
        Battle.Instance.DealDamage(m_enemySpell.GetComponent<Spell>().Damage + m_enemyNotes.Count, true);
        Debug.Log("Both effects clash!");
    }
    void CheckAccuracy(float a_noteTime)
    {
        float noteTime = (a_noteTime + 0.35f) * 95.0f * (1.0f / 60.0f);//gives how far off the beat that the note was played and which beat it was played on
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
        Battle.Instance.AccuracyText(noteAccuracy);
    }
}