using UnityEngine;
using System.Collections;

public class TurnTimer : MonoBehaviour
{
    //Attributes
    public static TurnTimer Instance { get; private set; }
    Turn m_currentTurn;
    [SerializeField]
    float m_castingTime, m_menuTime, m_countDown;
    //Behavious
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        m_countDown = m_menuTime;
    }
    void Update()
    {
        m_countDown -= Time.deltaTime;
        if(m_countDown <= 0 && m_currentTurn == Turn.Menu)
        {
            m_countDown = m_castingTime;
            m_currentTurn = Turn.Casting;
        }
        else if (m_countDown <= 0 && m_currentTurn == Turn.Casting)
        {
            m_countDown = m_menuTime;
            m_currentTurn = Turn.Menu;
            Battle.Instance.ReceiveTurnOver();
        }
        
    }
    public Turn CurrentTurn 
    { 
        get { return m_currentTurn; } 
    }
    public float CurrentTime
    {
        get { return m_countDown; }
    }
    public float CastingTime
    {
        get { return m_castingTime; }
    }
}