using UnityEngine;
using System.Collections;

public class ScreenTransition : MonoBehaviour {

    public bool m_coverScreen = true;

    [SerializeField]
    float m_initialAlpha;
    [SerializeField]
    Texture2D m_texture;

    Color m_currentColor;
    float m_alpha;
    float m_deltaTimeModifier;

	// Use this for initialization
	void Start () {
        m_currentColor = GUI.color;
        m_alpha = m_initialAlpha;
        m_deltaTimeModifier = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
        //m_currentColor = GUI.color;

        if (m_coverScreen)
        {
            m_alpha += Time.deltaTime / m_deltaTimeModifier;

            if(m_alpha >= 1.0f)
            {
                m_alpha = 1.0f;
            }
            
        }
        else
        {
            m_alpha -= Time.deltaTime / m_deltaTimeModifier;

            if(m_alpha <= 0.0f)
            {
                m_alpha = 0.0f;
            }
        }

        m_currentColor.a = m_alpha;

        //m_currentColor.a = Mathf.Lerp(1.0f, 0.0f, (Time.time / 2));

	}

    public void SetScreen(bool a_coverScreen, float a_timeToFade)
    {
        m_coverScreen = a_coverScreen;

        m_deltaTimeModifier = a_timeToFade;
    }

    void OnGUI()
    {
        GUI.color = m_currentColor; //cannot directly assign alpha value because unity, so this workaround is needed

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), m_texture);
    }
}
