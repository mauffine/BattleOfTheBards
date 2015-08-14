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
	void Start() 
    {
	
	}
	//The update function
	void Update()
    {
        if (TurnTimer.playerTurn)
        {
            if (m_playerInput == ControllerType.keyboard)
            {
                float time = TurnTimer.CountdownTime;
                //A whole bunch of key checks
                //I have tried to lay the notes out to correspond with the key presses musically
                if (Input.GetKeyDown(aNote))
                    Battle.ReceiveKey(new TimedNote(Note.A, time));
                if (Input.GetKeyDown(bNote))
                    Battle.ReceiveKey(new TimedNote(Note.B, time));
                if (Input.GetKeyDown(cNote))
                    Battle.ReceiveKey(new TimedNote(Note.C, time));
                if (Input.GetKeyDown(dNote))
                    Battle.ReceiveKey(new TimedNote(Note.D, time));
                if (Input.GetKeyDown(eNote))
                    Battle.ReceiveKey(new TimedNote(Note.E, time));
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //go back to menu
            //Application.LoadLevel("Menu")
        }
	}
}
