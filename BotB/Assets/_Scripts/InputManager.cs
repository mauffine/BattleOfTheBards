using UnityEngine;
using System.Collections;

///<summary>Controlls which input method is being used</summary>
public enum ControllerType : byte
{
    GHguitar,
    XBXcontroller,
    PS4controller,
    keyboard
};
public class InputManager : MonoBehaviour 
{
    [SerializeField] ///<summary>The current input method being used</summary>
    private ControllerType m_playerInput = ControllerType.keyboard;
    [SerializeField]
    private KeyCode aNote = KeyCode.Z, bNote = KeyCode.X, cNote = KeyCode.C, dNote = KeyCode.V, eNote = KeyCode.Space;

    static uint noteCounter = 0;
    static float resetTick = 4;
	void Start() 
    {
	
	}
	//The update function
	void Update()
    {
        resetTick -= Time.deltaTime;
        if (m_playerInput == ControllerType.keyboard)
        {
            float time = TurnTimer.CountdownTime;
            //A whole bunch of key checks
            //I have tried to lay the notes out to correspond with the key presses musically
            if(noteCounter < 15)
            { 
                if (Input.GetKeyDown(aNote)) { 
                    Battle.ReceiveKey(new TimedNote(Note.A, time, true)); ++noteCounter;}
                if (Input.GetKeyDown(bNote)) { 
                    Battle.ReceiveKey(new TimedNote(Note.B, time, true)); ++noteCounter;}
                if (Input.GetKeyDown(cNote)) { 
                    Battle.ReceiveKey(new TimedNote(Note.C, time, true)); ++noteCounter;}
                if (Input.GetKeyDown(dNote)) { 
                    Battle.ReceiveKey(new TimedNote(Note.D, time, true)); ++noteCounter;}
                if (Input.GetKeyDown(eNote)) {
                    Battle.ReceiveKey(new TimedNote(Note.E, time, true)); ++noteCounter; }
            }
            if(resetTick < 0)
            {
                noteCounter = 0;
                resetTick = 4;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //go back to menu
            //Application.LoadLevel("Menu")
        }
	}
}
