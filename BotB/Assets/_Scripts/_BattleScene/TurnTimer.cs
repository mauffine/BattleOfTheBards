using UnityEngine;
using System.Collections;

public class TurnTimer : MonoBehaviour
{
    //Attributes
    public static TurnTimer Instance { get; private set; }
    [SerializeField]
    Turn m_currentTurn;
    [SerializeField]
    float m_castingTime; //how much time there is to cast,
    [SerializeField]
    float m_menuTime; //how much time there is to select spells from menu,
    [SerializeField]
    float m_countDown; //were in the current turn the game is
    //Behavious
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        m_countDown = m_menuTime;
        m_currentTurn = Turn.Menu;  
    }
    void Update()
    {
        if (Battle.Instance.m_activeBattle)
        {
            if (m_currentTurn == Turn.Casting)
            m_countDown -= Time.deltaTime;
            if (m_countDown <= 0 && m_currentTurn == Turn.Casting)
            {
                m_countDown = m_menuTime;
                NextTurn();
            }
        }
        
    }
    public void NextTurn()
    {
        switch (m_currentTurn)
        {
            case Turn.Casting:
                m_currentTurn = Turn.SpellEffect;
                Battle.Instance.ReceiveTurnOver();
                break;
            case Turn.Menu:
                m_currentTurn = Turn.Casting;
                m_countDown = m_castingTime;
                break;
            case Turn.SpellEffect:
                m_currentTurn = Turn.Menu;
                break;
            default:
                break;
        }
    }
    public Turn CurrentTurn //the current turn
    { 
        get { return m_currentTurn; } 
    }
    public float CurrentTime //the current time in the turn
    {
        get { return m_countDown; }
    }
    public float CastingTime //how much time the casting turn lasts for
    {
        get { return m_castingTime; }
    }
}