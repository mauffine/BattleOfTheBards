using UnityEngine;
using System.Collections;

public class ScrollingNote : MonoBehaviour {

    public float m_scrollSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position -= new Vector3(m_scrollSpeed, 0, 0);
	}

    public void Initialise(float a_scrollSpeed)
    {
        m_scrollSpeed = a_scrollSpeed;
    }
}
