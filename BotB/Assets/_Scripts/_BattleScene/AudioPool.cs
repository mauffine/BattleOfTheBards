using UnityEngine;
using System.Collections;

public class AudioPool : MonoBehaviour {

    public GameObject m_audioSourcePrefab;

    AudioSource[] m_audioSourceList;


	// Use this for initialization
	void Start () 
    {
        
	}

    public void Initialise(GameObject a_audioSourcePrefab)
    {
        m_audioSourcePrefab = a_audioSourcePrefab;

        m_audioSourceList = new AudioSource[32];

        for (int i = 0; i < m_audioSourceList.Length; i++)
        {
            var audioSourceObject = Instantiate(m_audioSourcePrefab);
            audioSourceObject.transform.SetParent(this.transform);
            audioSourceObject.name = "AudioPool " + i;
            m_audioSourceList[i] = audioSourceObject.GetComponent<AudioSource>();
        }
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void PlayClip(AudioClip a_clip, float a_volume = 1) 
    {
        foreach (var source in m_audioSourceList)
        {
            if (!source.isPlaying)
            {
                source.clip = a_clip;
                source.volume = a_volume;
                source.Play();
                break;
            }
        }
    }
}
