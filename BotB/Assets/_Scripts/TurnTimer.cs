using UnityEngine;
using System.Collections;


public class TurnTimer : MonoBehaviour 
{
    //Attributes
    public float m_turnTime; //the time in seconds for each turn
    public bool m_playerTurn; //bool for it it's player's turn or enemy's turn

    [SerializeField]
    float m_turnCountdown; //the countdown variable used in the timer 
    
    //Behavious
	void Start () 
    {
        m_turnTime = 8; //8 seconds per turn   
        m_playerTurn = true; //start on the player turn
        m_turnCountdown = m_turnTime; //set the countdown to the turn time
	}
	
	void Update () 
    {
        TurnCountdown();
	}

    void TurnCountdown() //Function to count down the time left in the turn, switches turns when the timer ends and restarts it
    {
        //basic timer stuff
        m_turnCountdown -= Time.deltaTime;
        if (m_turnCountdown <= 0)
        {
            m_turnCountdown = m_turnTime;
            m_playerTurn = !m_playerTurn;
            Battle battleScript = gameObject.GetComponent<Battle>();
            battleScript.RecieveTurnOver(m_playerTurn);
        }
    }
}
