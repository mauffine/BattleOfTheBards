﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NoteVisualiser : MonoBehaviour 
{
    bool m_putDown = false;
    Turn m_turn;
    Note m_noteType;
    [SerializeField]
   // Sprite[] m_spriteSheet;
    public List<GameObject> m_spriteDisplay;
    List<GameObject> m_noteList;
	// Use this for initialization
    static NoteVisualiser refToMe;

    NotePool m_notePool;

	void Start()
    {
        refToMe = this;

        m_spriteDisplay = new List<GameObject>();
        //m_greyDisplay = new List<GameObject>();
        m_spriteDisplay.Capacity = 15;
        m_turn = TurnTimer.Instance.CurrentTurn;
        m_notePool = transform.parent.GetComponent<NotePool>();
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
            m_notePool.RemoveAllNotes();
            //Reset();
        }
	}
    public void ReceiveNote(TimedNote a_note)
    {
        m_putDown = true;
        m_noteType = a_note.m_note;
    }

    private void PushNote(Note a_Note)
    {
        Vector3 pos = (Slider.LocalPosition) - new Vector3(0, 0, 0.5f);

        m_notePool.AddNote(pos, a_Note);
        //switch (a_Note)
        //{
        //    case Note.A_:
        //        break;
        //    case Note.A:
        //        {
        //          /*  NotePlacer tempA = new NotePlacer(m_spriteSheet[0], Slider.Position);
        //            m_spriteDisplay.Push(tempA); */
        //            m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note A"), pos, new Quaternion(0, 1, 0, 0)));
        //        }
        //        break;
        //    case Note.B_:
        //        break;
        //    case Note.B:
        //        {
        //           /* NotePlacer tempA = new NotePlacer(m_spriteSheet[1], Slider.Position);
        //            m_spriteDisplay.Push(tempA); */
        //            m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note B"), pos, new Quaternion(0, 1, 0, 0)));
        //        }
        //        break;
        //    case Note.C:
        //        {
        //           /* NotePlacer tempB = new NotePlacer(m_spriteSheet[2], Slider.Position);
        //            m_spriteDisplay.Push(tempB); */
        //            m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note C"), pos, new Quaternion(0, 1, 0, 0)));
        //        }
        //        break;
        //    case Note.D_:
        //        break;
        //    case Note.D:
        //        {
        //         /*   NotePlacer tempC = new NotePlacer(m_spriteSheet[3], Slider.Position);
        //            m_spriteDisplay.Push(tempC); */
        //            m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note D"), pos, new Quaternion(0, 1, 0, 0)));
        //        }
        //        break;
        //    case Note.E:
        //        {
        //          /*  NotePlacer tempD = new NotePlacer(m_spriteSheet[4], Slider.Position);
        //            m_spriteDisplay.Push(tempD); */
        //            m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note E"), pos, new Quaternion(0, 1, 0, 0)));
        //        }
        //        break;
        //    case Note.F_:
        //        break;
        //    case Note.F:
        //        break;
        //    case Note.G:
        //        break;
        //    default:
        //        break;
        //}

        //m_spriteDisplay[m_spriteDisplay.Count - 1].transform.SetParent(this.transform.parent);
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
