using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NoteVisualiser : MonoBehaviour 
{
    bool m_putDown = false;
    bool m_playerTurn;
    Note m_noteType;
    [SerializeField]
   // Sprite[] m_spriteSheet;
    public List<GameObject> m_spriteDisplay;
    List<GameObject> m_noteList;
	// Use this for initialization
    static NoteVisualiser refToMe;
	void Start()
    {
        refToMe = this;
        m_noteList = new List<GameObject>();
        m_playerTurn = false;
	}
	
	// Update is called once per frame
	void Update()
    {
        if(m_putDown)
        {
            m_putDown = false;
            PushNote(m_noteType);
        }
        if (TurnTimer.Instance.CurrentTurn == Turn.Menu)
            Reset();
	}
    public void ReceiveNote(TimedNote a_note)
    {
        m_putDown = true;
        m_noteType = a_note.m_note;
    }

    private void PushNote(Note a_Note)
    {

        Vector3 pos = (Slider.Position);
        switch (a_Note)
        {
            case Note.A_:
                break;
            case Note.A:
                {
                    m_noteList.Add((GameObject)Instantiate(m_spriteDisplay[0], pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.B_:
                break;
            case Note.B:
                {
                    m_noteList.Add((GameObject)Instantiate(m_spriteDisplay[1], pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.C:
                {
                    m_noteList.Add((GameObject)Instantiate(m_spriteDisplay[2], pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.D_:
                break;
            case Note.D:
                {
                    m_noteList.Add((GameObject)Instantiate(m_spriteDisplay[3], pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.E:
                {
                    m_noteList.Add((GameObject)Instantiate(m_spriteDisplay[4], pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.F_:
                break;
            case Note.F:
                break;
            case Note.G:
                break;
            default:
                break;
        }
    }

    private void Reset()
    {
        for(int I = 0; I < m_noteList.Count; ++I)
        {
            Destroy(m_noteList[I]);
        }
        m_noteList.Clear();
    }
    public static NoteVisualiser Reference
    {
        get { return refToMe; }
    }
}
