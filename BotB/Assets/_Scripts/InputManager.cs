using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
    
    public enum ControllerType : byte
    {
        GHguitar,
        XBXcontroller,
        PS4controller,
        keyboard
    };
    [SerializeField]
    private ControllerType m_playerInput = ControllerType.keyboard;

	void Start () 
    {
	
	}
	
	void Update () 
    {
       if (m_playerInput == ControllerType.keyboard)
       {
           //A whole bunch of key checks
           //I have tried to lay the notes out to correspond with the key presses musically
           if (Input.GetKeyDown(KeyCode.W))
               Battle.ReceiveKey(Note.A);
           if (Input.GetKeyDown(KeyCode.S))
               Battle.ReceiveKey(Note.D);
           if (Input.GetKeyDown(KeyCode.A))
               Battle.ReceiveKey(Note.B);
           if (Input.GetKeyDown(KeyCode.D))
               Battle.ReceiveKey(Note.C);
           if (Input.GetKeyDown(KeyCode.Space))
               Battle.ReceiveKey(Note.E); 
       }
      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //go back to menu
            //Application.LoadLevel("Menu")
        }
	}
}
