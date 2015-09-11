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
    private ControllerType s_inputDevice = ControllerType.keyboard;
    [SerializeField]
    private KeyCode aNote = KeyCode.Z, bNote = KeyCode.X, cNote = KeyCode.C, dNote = KeyCode.V, eNote = KeyCode.Space;

    static uint s_noteCounter = 0;
    static float s_resetTick = 4, s_axisTick = 0.3f;
    static bool s_controllerReset = true;
    const float axisTick = 0.175f;
    void Start()
    {

    }
    //The update function
    public void Update()
    {
        s_resetTick -= Time.deltaTime;
        if (TurnTimer.Instance.CurrentTurn == Turn.Casting && s_noteCounter < 15)
        {
            float timePass = TurnTimer.Instance.CurrentTime;
            //I have tried to lay the notes out to correspond with the key presses musically

            if (s_inputDevice == ControllerType.keyboard)//If Keyboard check Keyboard
                KeyboardControllerCheck(timePass);
            if (s_inputDevice == ControllerType.PS4controller)//If PS4 check PS4
                PS4ControllerCheck(timePass);
        }
        if (s_resetTick < 0)
        {
            s_noteCounter = 0;
            s_resetTick = 4;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //go back to menu
            Application.LoadLevel("MenuScene");
        }
    }

    private void PS4ControllerCheck(float a_time)
    {
        s_axisTick -= Time.deltaTime;
        if (s_controllerReset)//Make sure the axes have been reset so the inputs can't be spammed
        {
            //Left Analog stick
            if (Input.GetAxis("Vertical") > 0.8f)
            {
                s_controllerReset = false;
                Battle.Instance.ReceiveKey(new TimedNote(Note.D, a_time, true)); ++s_noteCounter;
                s_axisTick = axisTick;
            }
            if (Input.GetAxis("Vertical") < -0.8f)
            {
                s_controllerReset = false;
                Battle.Instance.ReceiveKey(new TimedNote(Note.C, a_time, true)); ++s_noteCounter;
                s_axisTick = axisTick;
            }
            if (Input.GetAxis("Horizontal") > 0.8f)
            {
                s_controllerReset = false;
                Battle.Instance.ReceiveKey(new TimedNote(Note.A, a_time, true)); ++s_noteCounter;
                s_axisTick = axisTick;
            }
            if (Input.GetAxis("Horizontal") < -0.8f)
            {
                s_controllerReset = false;
                Battle.Instance.ReceiveKey(new TimedNote(Note.B, a_time, true)); ++s_noteCounter;
                s_axisTick = axisTick;
            }
            //D-Pad are axes for some reason?
            if (Input.GetAxis("D-Pad X") >= 1.0f)
            {
                s_controllerReset = false;
                Battle.Instance.ReceiveKey(new TimedNote(Note.A, a_time, true)); ++s_noteCounter;
                s_axisTick = axisTick;
            }
            if (Input.GetAxis("D-Pad X") <= -1.0f)
            {
                s_controllerReset = false;
                Battle.Instance.ReceiveKey(new TimedNote(Note.B, a_time, true)); ++s_noteCounter;
                s_axisTick = axisTick;
            }
            if (Input.GetAxis("D-Pad Y") >= 1.0f)
            {
                s_controllerReset = false;
                Battle.Instance.ReceiveKey(new TimedNote(Note.D, a_time, true)); ++s_noteCounter;
                s_axisTick = axisTick;
            }
            if (Input.GetAxis("D-Pad Y") <= -1.0f)
            {
                s_controllerReset = false;
                Battle.Instance.ReceiveKey(new TimedNote(Note.C, a_time, true)); ++s_noteCounter;
                s_axisTick = axisTick;
            }
            //Buttons
            if (Input.GetButtonDown("Cross"))
            {                
                s_controllerReset = false;
                Battle.Instance.ReceiveKey(new TimedNote(Note.E, a_time,true)); ++s_noteCounter;
                s_axisTick = axisTick;
            }

        }
        if (s_axisTick < 0)
            s_controllerReset = true;
    }

    private void KeyboardControllerCheck(float a_time)
    {
        if (Input.GetKeyDown(aNote))
        {
            Battle.Instance.ReceiveKey(new TimedNote(Note.A, a_time, true)); ++s_noteCounter;
        }
        if (Input.GetKeyDown(bNote))
        {
            Battle.Instance.ReceiveKey(new TimedNote(Note.B, a_time, true)); ++s_noteCounter;
        }
        if (Input.GetKeyDown(cNote))
        {
            Battle.Instance.ReceiveKey(new TimedNote(Note.C, a_time, true)); ++s_noteCounter;
        }
        if (Input.GetKeyDown(dNote))
        {
            Battle.Instance.ReceiveKey(new TimedNote(Note.D, a_time, true)); ++s_noteCounter;
        }
        if (Input.GetKeyDown(eNote))
        {
            Battle.Instance.ReceiveKey(new TimedNote(Note.E, a_time, true)); ++s_noteCounter;
        }
    }
}