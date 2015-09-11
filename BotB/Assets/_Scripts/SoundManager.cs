using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour 
{
    static SoundManager s_SoundManRef;
    [SerializeField]
    AudioClip[] m_noteArray;
    [SerializeField]
    float m_noteVolume;
    [SerializeField]
    AudioClip m_backgroundSong;
    [SerializeField]
    float m_Backgroundvolume = 0.0f;
    
    AudioPool m_audioPool;
    [SerializeField]
    GameObject m_audioSourcePrefab;
    
	// Use this for initialization
	void Start() 
    {
        s_SoundManRef = this;

        gameObject.AddComponent<AudioPool>();
        m_audioPool = GetComponent<AudioPool>();
        m_audioPool.Initialise(m_audioSourcePrefab);
        //m_audioPool.m_audioSourcePrefab = m_audioSourcePrefab;

        m_audioPool.PlayClip(m_backgroundSong, m_Backgroundvolume);
        
	}
    public void ReceiveNote(TimedNote a_note)
    {
        Note toPlay = a_note.m_note;
        switch (toPlay)
        {
            case Note.A_:
                break;
            case Note.A:
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

    public static SoundManager Reference
    {
        get { return s_SoundManRef; }
    }
}
