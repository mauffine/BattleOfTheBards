using UnityEngine;
using System.Collections;

public class TurnTimer : MonoBehaviour 
{
    //Attributes
    [SerializeField]
    static float s_turnTime = 4.0f; //the time in seconds for each turn
    [SerializeField]
    static float s_menuTime = 2.0f;
    [SerializeField]
    public static bool s_playerTurn = false; //bool for it it's player's turn or enemy's turn
    [SerializeField]
    static float s_turnCountdown = 4.0f; //the countdown variable used in the timer 
    static Turn s_currentTurn; //sets whether the player is playing a song or picking one from the menu
    //Behavious
    void Start() 
    {
        AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("_Sound/Metronome4s"), Vector3.zero, 0.1f);
        s_currentTurn = Turn.Menu;
	}

    void Update() 
    {
        TurnCountdown();
	}
    ///<summary> Counts down the time left in the turn, switches turns when the timer ends and resets the timer</summary>
    static void TurnCountdown()
    {
        //basic timer stuff
        s_turnCountdown -= Time.deltaTime;
        if (s_turnCountdown <= 0)
        {
            s_turnCountdown = s_turnTime;
            s_playerTurn = !s_playerTurn;
            Battle.BattleReference.RecieveTurnOver(s_playerTurn);
            AudioSource.PlayClipAtPoint((AudioClip)Resources.Load("_Sound/Metronome4s"), Vector3.zero, 0.1f);
        }
    }

    static public float TimePerTurn
    {
        get {return s_turnTime; }
    } 
    static public float CountdownTime { get { return s_turnCountdown; } }
    static public Turn CurrentTurn { get { return s_currentTurn; } }
}
