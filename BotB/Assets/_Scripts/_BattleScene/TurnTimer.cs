using UnityEngine;
using System.Collections;

public class TurnTimer : MonoBehaviour 
{
    //Attributes
    [SerializeField]
    static float turnTime = 6; //the time in seconds for each turn
    [SerializeField]
    public static bool playerTurn = false; //bool for it it's player's turn or enemy's turn
    [SerializeField]
    static float turnCountdown = 6; //the countdown variable used in the timer 

   
    //Behavious
    void Start() 
    {
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
        }
    }

    static public float TimePerTurn
    {
        get {return turnTime; }
    } 
    //
    static public float CountdownTime { get { return turnCountdown; } }
}
