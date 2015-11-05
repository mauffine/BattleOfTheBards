using UnityEngine;
using System.Collections;

public class SpellAnimator : MonoBehaviour
{
    GameObject m_playerSpell;
    GameObject m_enemySpell;

    float m_timer;
    bool m_countdown;
    public static SpellAnimator Instance; // temp solution
	// Use this for initialization
    void Awake()
    {
        Instance = this;
    }
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_countdown == true)
            m_timer -= Time.deltaTime;

        if (m_enemySpell != null && m_playerSpell != null)
        {

        }
        if (m_timer <= 0 && m_countdown == true)
        {
            m_countdown = false;
            TurnTimer.Instance.NextTurn();
        }

	}
    public GameObject PlayerSpell
    { 
        set { m_playerSpell = value; }
        get { return m_playerSpell; }
    }
    public GameObject EnemySpell
    { 
        set { m_enemySpell = value; }
        get { return m_enemySpell; }
    }
}
