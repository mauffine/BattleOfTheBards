﻿using UnityEngine;
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
    bool m_putDown = false;
    bool m_playerTurn;
    Note m_noteType;
    [SerializeField]
   // Sprite[] m_spriteSheet;
    List<GameObject> m_spriteDisplay;
	// Use this for initialization
    static NoteVisualiser refToMe;
	void Start()
    {
        refToMe = this;
        m_spriteDisplay = new List<GameObject>();
        m_spriteDisplay.Capacity = 15;
        m_playerTurn = Battle.BattleReference.PlayerTurn;
	}
	
	// Update is called once per frame
	void Update()
    {
        if(m_putDown)
        {
            m_putDown = false;
            PushNote(m_noteType);
        }
        if (m_playerTurn != Battle.BattleReference.PlayerTurn)
        {
            m_playerTurn = Battle.BattleReference.PlayerTurn;
            for (int I = 0; I < m_spriteDisplay.Count; ++I )
            {
                m_spriteDisplay[I].SetActive(false);
                Destroy(m_spriteDisplay[I]);
            }
            m_spriteDisplay.Clear();
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
                    m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note A"), pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.B_:
                break;
            case Note.B:
                {
                   /* NotePlacer tempA = new NotePlacer(m_spriteSheet[1], Slider.Position);
                    m_spriteDisplay.Push(tempA); */
                    m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note B"), pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.C:
                {
                   /* NotePlacer tempB = new NotePlacer(m_spriteSheet[2], Slider.Position);
                    m_spriteDisplay.Push(tempB); */
                    m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note C"), pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.D_:
                break;
            case Note.D:
                {
                 /*   NotePlacer tempC = new NotePlacer(m_spriteSheet[3], Slider.Position);
                    m_spriteDisplay.Push(tempC); */
                    m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note D"), pos, new Quaternion(0, 1, 0, 0)));
                }
                break;
            case Note.E:
                {
                  /*  NotePlacer tempD = new NotePlacer(m_spriteSheet[4], Slider.Position);
                    m_spriteDisplay.Push(tempD); */
                    m_spriteDisplay.Add((GameObject)Instantiate(Resources.Load("_Prefabs/Notes/Note E"), pos, new Quaternion(0, 1, 0, 0)));
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
    public static NoteVisualiser Reference
    {
        get { return refToMe; }
    }
}
