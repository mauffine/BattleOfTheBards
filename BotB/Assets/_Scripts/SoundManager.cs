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
    [SerializeField]
    float m_beatVolume = 1.0f;

    [SerializeField]
    AudioClip m_playingBeatSound;
    [SerializeField]
    AudioClip m_menuBeatSound;
    AudioSource m_beatSource;
    public float m_beat = 0.5f;
    float m_beatTimer = 0;
    bool m_murray = false;
    
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

        //m_audioPool.PlayClip(m_backgroundSong, m_Backgroundvolume);
        m_beatSource = m_audioPool.GetUnusedSource();
        
	}
    public void ReceiveNote(TimedNote a_note)
    {
        Note toPlay = a_note.m_note;
        switch (toPlay)
        {
            case Note.A_:
                break;
            case Note.A:
                m_audioPool.PlayClip(m_noteArray[0],m_noteVolume);
                break;
            case Note.B_:
                break;
            case Note.B:
                m_audioPool.PlayClip(m_noteArray[1], m_noteVolume);
                break;
            case Note.C:
                m_audioPool.PlayClip(m_noteArray[2], m_noteVolume);
                break;
            case Note.D_:
                break;
            case Note.D:
                m_audioPool.PlayClip(m_noteArray[3], m_noteVolume);
                break;
            case Note.E:
                m_audioPool.PlayClip(m_noteArray[4], m_noteVolume);
                break;
            case Note.F_:
                break;
            case Note.F:
                m_audioPool.PlayClip(m_noteArray[5], m_noteVolume);
                break;
            case Note.G:
                m_audioPool.PlayClip(m_noteArray[6],m_noteVolume);
                break;
            default:
                break;
        }
    }
	// Update is called once per frame
	void Update() 
    {
        if (m_murray)
            PlayBeat(m_playingBeatSound);
        else
            PlayBeat(m_menuBeatSound);
	}

    public static SoundManager Reference
    {
        get { return s_SoundManRef; }
    }

    void PlayBeat(AudioClip a_beatClip)
    {
        if(m_beatTimer >= m_beat)
        {
            m_murray = !m_murray;

            m_beatTimer -= m_beat;
            if (m_beatSource.clip != a_beatClip)
                m_beatSource = m_audioPool.GetUnusedSource();
            m_beatSource.Stop();
            m_beatSource.clip = a_beatClip;
            m_beatSource.volume = m_beatVolume;
            m_beatSource.Play();
        }

        m_beatTimer += Time.deltaTime;
    }
}
