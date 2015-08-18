using UnityEngine;
using System.Collections;

public class Hasten : Spell 
{
    string m_key = "AAAEA";

    // Use this for initialization
    void Start()
    {
        TurnTimer.playerTurn = true;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override string Key
    {
        get { return "AAAEA"; }
    }
}
