using UnityEngine;
using System.Collections;

public class TurnTimer : MonoBehaviour 
{
    //Attributes
    [SerializeField]
    static float turnTime = 4f; //the time in seconds for each turn
    [SerializeField]
    public static bool playerTurn = false; //bool for it it's player's turn or enemy's turn
    [SerializeField]
    static float turnCountdown = 4f; //the countdown variable used in the timer 

    static bool m_pause;

    //Behavious
    void Start() 
    {
        AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("_Sound/Metronome4s"), Vector3.zero, 0.1f);
	}

    void Update() 
    {
        TurnCountdown();
	}
    ///<summary> Counts down the time left in the turn, switches turns when the timer ends and resets the timer</summary>
    static void TurnCountdown()
    {
        //basic timer stuff
        turnCountdown -= Time.deltaTime;
        if (turnCountdown <= 0)
        {
            turnCountdown = turnTime;
            playerTurn = !playerTurn;
            Battle.BattleReference.RecieveTurnOver(playerTurn);
            AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("_Sound/Metronome4s"), Vector3.zero, 0.1f);
        }
    }

    static public float TimePerTurn
    {
        get {return turnTime; }
    } 
    //
    static public float CountdownTime { get { return turnCountdown; } }
    static public bool Paused { get { return m_pause; } }
}
