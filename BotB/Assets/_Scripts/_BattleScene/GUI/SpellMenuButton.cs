using UnityEngine;
using System.Collections;

public class SpellMenuButton : MonoBehaviour 
{

    Vector3 m_initialPosition;
    public bool m_selected;    

	// Use this for initialization
	void Start() 
    {
        m_initialPosition = transform.localPosition;
        m_selected = false;
	}
	
	// Update is called once per frame
	void Update() 
    {
        if (!m_selected)
            transform.localScale = new Vector3(50 + Mathf.Sin(Time.time * 5) * 5, 50 + Mathf.Sin(Time.time * 5) * 5, 50) * 1.5f;
        else
            transform.localScale = new Vector3(60, 60, 60) * 1.5f;

        //transform.localPosition = new Vector3(m_initialPosition.x, m_initialPosition.y + 20 * Mathf.Sin((Time.time + m_hoverOffset) * 1.5f), m_initialPosition.z);
	}

    public void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Show()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void SetSelected()
    {
        m_selected = true;
    }

    public void SetUnselected()
    {
        m_selected = false;
    }
}
