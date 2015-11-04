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
            transform.localScale = new Vector3(3.8f + Mathf.Sin(Time.time * 3) * 1.1f, 3.8f + Mathf.Sin(Time.time * 3) * 1.1f, 1) * 0.3f;
        else
            transform.localScale = new Vector3(1, 1, 1) * 1.5f;
        //transform.localPosition = new Vector3(m_initialPosition.x, m_initialPosition.y + 20 * Mathf.Sin((Time.time + m_hoverOffset) * 1.5f), m_initialPosition.z);
	}

    public void Hide()
    {
        GetComponent<CanvasRenderer>().SetAlpha(0);
    }

    public void Show()
    {
        GetComponent<CanvasRenderer>().SetAlpha(255);
    }

    public void SetSelected()
    {
        m_selected = true;
        GetComponent<CanvasRenderer>().SetColor(Color.Lerp(Color.clear, Color.blue, 0.5f));
    }

    public void SetUnselected()
    {
        m_selected = false;
        GetComponent<CanvasRenderer>().SetColor(Color.white);
    }
}
