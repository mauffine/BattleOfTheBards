using UnityEngine;
using System.Collections;

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
            noteObject.transform.SetParent(this.transform);
            noteObject.transform.rotation = new Quaternion(0, 1, 0, 0);
            noteObject.name = "NotePool " + i;
            m_noteList[i] = noteObject;
            noteObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update() 
    {
        //AddNote(new Vector2(-50, 0), Note.C);
	}

    public void AddNote(Vector3 a_pos, Note a_note, bool a_faded)
    {
        foreach(var note in m_noteList)
        {
            if(!note.activeSelf)
            {
                note.SetActive(true);
                note.transform.localPosition = a_pos;

                var spriteRenderer = note.GetComponent<SpriteRenderer>();
                switch (a_note)
                {
                    case Note.A_:
                        break;
                    case Note.A:
                        {
                            if (a_faded)
                                spriteRenderer.sprite = m_fadedNotes[0];
                            else
                                spriteRenderer.sprite = m_noteA;
                        }
                        break;
                    case Note.B_:
                        break;
                    case Note.B:
                        {
                            if (a_faded)
                                spriteRenderer.sprite = m_fadedNotes[1];
                            else
                                spriteRenderer.sprite = m_noteB;
                        }
                        break;
                    case Note.C:
                        {
                            if (a_faded)
                                spriteRenderer.sprite = m_fadedNotes[2];
                            else
                                spriteRenderer.sprite = m_noteC;
                        }
                        break;
                    case Note.D_:
                        break;
                    case Note.D:
                        {
                            if (a_faded)
                                spriteRenderer.sprite = m_fadedNotes[3];
                            else
                                spriteRenderer.sprite = m_noteD;
                        }
                        break;
                    case Note.E:
                        {
                            if (a_faded)
                                spriteRenderer.sprite = m_fadedNotes[4];
                            else
                                spriteRenderer.sprite = m_noteE;
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
