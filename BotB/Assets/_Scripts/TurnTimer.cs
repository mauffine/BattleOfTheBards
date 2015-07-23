using UnityEngine;
using System.Collections;

public class TurnTimer : MonoBehaviour {
    //Attributes
    public float m_turnTime; //the time in seconds for each turn
    public bool m_playerTurn; //bool for it it's player's turn or enemy's turn

    float m_turnCountdown; //the countdown variable used in the timer 
    
    //Behavious
	void Start () {
        m_turnTime = 8;
        m_playerTurn = true;
        m_turnCountdown = m_turnTime;
	}
	
	void Update () {
        TurnCountdown();
	}

    void TurnCountdown()
    {
        m_turnCountdown -= Time.deltaTime;
        if (m_turnCountdown <= 0)
        {
            m_turnCountdown = m_turnTime;
            m_playerTurn = !m_playerTurn;
        }
    }
}
