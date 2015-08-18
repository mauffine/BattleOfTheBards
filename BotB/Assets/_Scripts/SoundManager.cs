using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
    [SerializeField]
    AudioClip[] m_noteArray;
    [SerializeField]
    float m_noteVolume;
    [SerializeField]
    AudioClip m_backgroundSong;
    [SerializeField]
    float m_Backgroundvolume = 0.0f;


    private readonly Vector3 origin = new Vector3(0, 0, 0);
	// Use this for initialization
	void Start() 
    {
        m_noteArray = new AudioClip[7];
        char clipTitle = 'A';
        for (uint I = 0; I < 7; ++I)
        {
            m_noteArray[I] = Resources.Load<AudioClip>("_Sound/Piano Notes/" + clipTitle);
            ++clipTitle;
        }
        AudioSource.PlayClipAtPoint(m_backgroundSong, origin, m_Backgroundvolume);
	}
    public void ReceiveNote(TimedNote a_note)
    {
        Note toPlay = a_note.m_note;
        switch (toPlay)
        {
            case Note.A_:
                break;
            case Note.A:
                AudioSource.PlayClipAtPoint(m_noteArray[0], origin, m_noteVolume);
                break;
            case Note.B_:
                break;
            case Note.B:
                AudioSource.PlayClipAtPoint(m_noteArray[1], origin, m_noteVolume);
                break;
            case Note.C:
                AudioSource.PlayClipAtPoint(m_noteArray[2], origin, m_noteVolume);
                break;
            case Note.D_:
                break;
            case Note.D:
                AudioSource.PlayClipAtPoint(m_noteArray[3], origin, m_noteVolume);
                break;
            case Note.E:
                AudioSource.PlayClipAtPoint(m_noteArray[4], origin, m_noteVolume);
                break;
            case Note.F_:
                break;
            case Note.F:
                AudioSource.PlayClipAtPoint(m_noteArray[5], origin, m_noteVolume);
                break;
            case Note.G:
                AudioSource.PlayClipAtPoint(m_noteArray[6], origin, m_noteVolume);
                break;
            default:
                break;
        }
    }
	// Update is called once per frame
	void Update() 
    {
	
	}
}
