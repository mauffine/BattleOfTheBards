using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NoteVisualiser : MonoBehaviour 
{
    /* private struct NotePlacer
    {
        Sprite image;
        Vector2 pos;
        public NotePlacer(Sprite a_s,Vector2 a_p)
        {
            image = a_s;
            pos = a_p;
        }
    }; */
    public GameObject[] m_notePrefabs;
    bool m_putDown = false;
    Turn m_turn;
    Note m_noteType;
    [SerializeField]
   // Sprite[] m_spriteSheet;
    List<GameObject> m_spriteDisplay;
    List<GameObject> m_greyDisplay;
	// Use this for initialization
    static NoteVisualiser refToMe;
	void Start()
    {
        refToMe = this;
        m_spriteDisplay = new List<GameObject>();
        m_greyDisplay = new List<GameObject>();
        m_spriteDisplay.Capacity = 15;
        m_turn = TurnTimer.Instance.CurrentTurn;
	}
	
	// Update is called once per frame
	void Update()
    {
        if(m_putDown)
        {
            m_putDown = false;
            PushNote(m_noteType);
        }
        if (m_turn != TurnTimer.Instance.CurrentTurn)
        {
            m_turn = TurnTimer.Instance.CurrentTurn;
            Swap();
        }
	}
    public void ReceiveNote(TimedNote a_note)
    {
        m_putDown = true;
        m_noteType = a_note.m_note;
    }

    private void PushNote(Note a_Note)
    {

        Vector3 pos = (Slider.Position);// - new Vector3(0, 0, 0.5f));
        switch (a_Note)
        {
            case Note.A_:
                break;
            case Note.A:
                {
                  /*  NotePlacer tempA = new NotePlacer(m_spriteSheet[0], Slider.Position);
                    m_spriteDisplay.Push(tempA); */
                    m_spriteDisplay.Add((GameObject)Instantiate(m_notePrefabs[0], pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.B_:
                break;
            case Note.B:
                {
                   /* NotePlacer tempA = new NotePlacer(m_spriteSheet[1], Slider.Position);
                    m_spriteDisplay.Push(tempA); */
                    m_spriteDisplay.Add((GameObject)Instantiate(m_notePrefabs[1], pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.C:
                {
                   /* NotePlacer tempB = new NotePlacer(m_spriteSheet[2], Slider.Position);
                    m_spriteDisplay.Push(tempB); */
                    m_spriteDisplay.Add((GameObject)Instantiate(m_notePrefabs[2], pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.D_:
                break;
            case Note.D:
                {
                 /*   NotePlacer tempC = new NotePlacer(m_spriteSheet[3], Slider.Position);
                    m_spriteDisplay.Push(tempC); */
                    m_spriteDisplay.Add((GameObject)Instantiate(m_notePrefabs[3], pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.E:
                {
                  /*  NotePlacer tempD = new NotePlacer(m_spriteSheet[4], Slider.Position);
                    m_spriteDisplay.Push(tempD); */
                    m_spriteDisplay.Add((GameObject)Instantiate(m_notePrefabs[4], pos, new Quaternion(0, 1, 0, 0)));
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
        m_spriteDisplay[m_spriteDisplay.Count - 1].transform.SetParent(this.transform.parent);
    }

    private void Swap()
    {
        //Delete old greyed out notes
        for (int I = 0; I < m_greyDisplay.Count; ++I)
        {
            m_greyDisplay[I].SetActive(false);
            Destroy(m_greyDisplay[I]);
        }
        m_greyDisplay.Clear();
        //swap the lists
        for(int I = 0; I < m_spriteDisplay.Count; ++I)
        {
            m_spriteDisplay[I].GetComponent<SpriteRenderer>().color = Color.grey;
            m_greyDisplay.Add(m_spriteDisplay[I]);
        }
        m_spriteDisplay.Clear();
    }
    public static NoteVisualiser Reference
    {
        get { return refToMe; }
    }
}
