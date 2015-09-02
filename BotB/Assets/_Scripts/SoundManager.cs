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

    private AudioPool m_audioPool;

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
        m_audioPool = GetComponent<AudioPool>();
        m_audioPool.PlayClip(m_backgroundSong);
        //AudioSource.PlayClipAtPoint(m_backgroundSong, origin, m_Backgroundvolume);

        
	}
    public void ReceiveNote(TimedNote a_note)
    {
        Note toPlay = a_note.m_note;
        switch (toPlay)
        {
            case Note.A_:
                break;
            case Note.A:
                //AudioSource.PlayClipAtPoint(m_noteArray[0], origin, m_noteVolume);
                m_audioPool.PlayClip(m_noteArray[0]);
                break;
            case Note.B_:
                break;
            case Note.B:
                m_audioPool.PlayClip(m_noteArray[1]);
                break;
            case Note.C:
                m_audioPool.PlayClip(m_noteArray[2]);
                break;
            case Note.D_:
                break;
            case Note.D:
                m_audioPool.PlayClip(m_noteArray[3]);
                break;
            case Note.E:
                m_audioPool.PlayClip(m_noteArray[4]);
                break;
            case Note.F_:
                break;
            case Note.F:
                m_audioPool.PlayClip(m_noteArray[5]);
                break;
            case Note.G:
                m_audioPool.PlayClip(m_noteArray[6]);
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
