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
        if (TurnTimer.playerTurn && noteCounter < 15)
        {
            float time = TurnTimer.CountdownTime;
            resetTick -= Time.deltaTime;
            //I have tried to lay the notes out to correspond with the key presses musically

            if (m_playerInput == ControllerType.keyboard)//If Keyboard check Keyboard
                KeyboardControllerCheck(time);
            if (m_playerInput == ControllerType.XBXcontroller)//If Xbox check Xbox
                XboxControllerCheck(time);
        }
        if (resetTick < 0)
        {
            noteCounter = 0;
            resetTick = 4;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //go back to menu
            Application.LoadLevel("MenuScene");
        }
    }

    private void XboxControllerCheck(float a_time)
    {
        if (Input.GetAxis("Vertical") > 0.9f)
        {
            Battle.ReceiveKey(new TimedNote(Note.A, a_time)); ++noteCounter;
        }
        if (Input.GetAxis("Vertical") < -0.9f)
        {
            Battle.ReceiveKey(new TimedNote(Note.B, a_time)); ++noteCounter;
        }
        if (Input.GetAxis("Horizontal") > 0.9f)
        {
            Battle.ReceiveKey(new TimedNote(Note.C, a_time)); ++noteCounter;
        }
        if (Input.GetAxis("Horizontal") < -0.9f)
        {
            Battle.ReceiveKey(new TimedNote(Note.D, a_time)); ++noteCounter;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            Battle.ReceiveKey(new TimedNote(Note.E, a_time)); ++noteCounter;
        }
    }

    private void KeyboardControllerCheck(float a_time)
    {
        if (Input.GetKeyDown(aNote))
        {
            Battle.ReceiveKey(new TimedNote(Note.A, a_time)); ++noteCounter;
        }
        if (Input.GetKeyDown(bNote))
        {
            Battle.ReceiveKey(new TimedNote(Note.B, a_time)); ++noteCounter;
        }
        if (Input.GetKeyDown(cNote))
        {
            Battle.ReceiveKey(new TimedNote(Note.C, a_time)); ++noteCounter;
        }
        if (Input.GetKeyDown(dNote))
        {
            Battle.ReceiveKey(new TimedNote(Note.D, a_time)); ++noteCounter;
        }
        if (Input.GetKeyDown(eNote))
        {
            Battle.ReceiveKey(new TimedNote(Note.E, a_time)); ++noteCounter;
        }
    }
}