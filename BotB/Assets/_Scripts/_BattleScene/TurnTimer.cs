using UnityEngine;
using System.Collections;

public class TurnTimer : MonoBehaviour 
{
    //Attributes
    [SerializeField]
    float m_turnTime = 8; //the time in seconds for each turn
    [SerializeField]
    bool m_playerTurn = true; //bool for it it's player's turn or enemy's turn
    [SerializeField]
    float m_turnCountdown = 8; //the countdown variable used in the timer 

    public float CountdownTime { get { return m_turnCountdown; } }
    //Behavious
	void Start() 
    {
	}
	
	void Update() 
    {
        TurnCountdown();
	}
    ///<summary> Counts down the time left in the turn, switches turns when the timer ends and resets the timer</summary>
    void TurnCountdown()
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

    public float TimePerTurn
    {
        get {return m_turnTime; }
    }
}
