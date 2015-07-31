using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
    ///<summary>Controlls which input method is being used</summary>
    public enum ControllerType : byte
    {
        GHguitar,
        XBXcontroller,
        PS4controller,
        keyboard
    };

   
    [SerializeField] ///<summary>The current input method being used</summary>
    private ControllerType m_playerInput = ControllerType.keyboard;

	void Start () 
    {
	
	}
	
	void Update () 
    {
       if (m_playerInput == ControllerType.keyboard)
       {
           float time = this.GetComponent<TurnTimer>().GetTime();
           //A whole bunch of key checks
           //I have tried to lay the notes out to correspond with the key presses musically
           if (Input.GetKeyDown(KeyCode.W))
               Battle.ReceiveKey(new TimedNote(Note.A, time));
           if (Input.GetKeyDown(KeyCode.S))
               Battle.ReceiveKey(new TimedNote(Note.D, time));
           if (Input.GetKeyDown(KeyCode.A))
               Battle.ReceiveKey(new TimedNote(Note.B, time));
           if (Input.GetKeyDown(KeyCode.D))
               Battle.ReceiveKey(new TimedNote(Note.C, time));
           if (Input.GetKeyDown(KeyCode.Space))
               Battle.ReceiveKey(new TimedNote(Note.E, time)); 
       }
      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //go back to menu
            //Application.LoadLevel("Menu")
        }
	}
}
