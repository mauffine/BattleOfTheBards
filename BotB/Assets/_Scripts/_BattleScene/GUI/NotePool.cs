using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NotePool : MonoBehaviour 
{
    [SerializeField]
    private GameObject m_notePrefab;
    [SerializeField]
    private Sprite m_noteA, m_noteB, m_noteC, m_noteD, m_noteE, m_noteBLANK;
    [SerializeField]
    private Sprite[] m_fadedNotes;
    //GameObject[] m_noteList;
    GameObject[] m_noteList;

	// Use this for initialization
	void Start() 
    {

        m_noteList = new GameObject[32];

        for (int i = 0; i < m_noteList.Length; i++)
        {
            var noteObject = Instantiate(m_notePrefab);
            noteObject.transform.SetParent(transform);
            noteObject.transform.rotation = new Quaternion(0, 1, 0, 0);
            noteObject.name = "NotePool " + i;
            Vector3 lastScale = noteObject.transform.localScale *= 600;
            noteObject.transform.localScale = new Vector3(lastScale.x, lastScale.y,1);
            m_noteList[i] = noteObject;
            noteObject.SetActive(false);
        }
	}
	// Update is called once per frame
	void Update() 
    {
        //for (int i = 0; i < m_noteList.Length; i++)
        //{
        //    m_noteList[i].transform 
        //}
        //AddNote(new Vector2(-50, 0), Note.C);
	}
    public void AddNote(Vector3 a_pos, Note a_note, bool a_faded, float a_scrollSpeed = 0)
    {
        foreach(var note in m_noteList)
        {
            if(!note.activeSelf)
            {
                note.SetActive(true);
                Vector3 newNotePos = new Vector3(a_pos.x,a_pos.y,1);
                note.transform.localPosition = newNotePos;
                //note.GetComponent<ScrollingNote>().Initialise(a_scrollSpeed);

                var spriteRenderer = note.GetComponent<Image>();

                switch (a_note)
                {
                    case Note.A_:
                        break;
                    case Note.A:
                        {
                            spriteRenderer.sprite = (a_faded) ? m_fadedNotes[0] : m_noteA;
                        }
                        break;
                    case Note.B_:
                        break;
                    case Note.B:
                        {
                            spriteRenderer.sprite = (a_faded) ? m_fadedNotes[1] : m_noteB;
                        }
                        break;
                    case Note.C:
                        {
                            spriteRenderer.sprite = (a_faded) ? m_fadedNotes[2] : m_noteC;
                        }
                        break;
                    case Note.D_:
                        break;
                    case Note.D:
                        {
                            spriteRenderer.sprite = (a_faded) ? m_fadedNotes[3] : m_noteD;
                        }
                        break;
                    case Note.E:
                        {
                            spriteRenderer.sprite = (a_faded) ? m_fadedNotes[4] : m_noteE;
                        }
                        break;
                    case Note.F_:
                        break;
                    case Note.F:
                        break;
                    case Note.G:
                        break;
                    case Note.BLANK:
                        {
                            spriteRenderer.sprite = m_noteBLANK;
                        }
                        break;
                    default:
                        break;
                }
                break;
            }
        }
    }
    public void AddNote(float a_xLoc, Note a_note, bool a_faded)
    {
        Vector3 notePos = new Vector3(0,0,0);
        switch (a_note)
	    {
		case Note.A_:
         break;
        case Note.A:
         notePos.y = -305;
         break;
        case Note.B_:
         break;
        case Note.B:
                notePos.y = -260;
         break;
        case Note.C:
         notePos.y = -210;
         break;
        case Note.D_:
         break;
        case Note.D:
         notePos.y = -160;
         break;
        case Note.E:
         notePos.y = -110;
         break;
        case Note.F_:
         break;
        case Note.F:
         notePos.y = -60;
         break;
        case Note.G:
         notePos.y = -10;
         break;
        case Note.BLANK:
         break;
        default:
         break;
	    }
        notePos.x = a_xLoc;
        notePos.y -= 200;
        AddNote(notePos, a_note,a_faded);
    }
    public void RemoveAllNotes()
    {
        foreach (var note in m_noteList)
        {
            if (note.activeSelf)
            {
                note.SetActive(false);
            }
        }
    }
}
