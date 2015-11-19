using UnityEngine;
using System.Collections;

public class ScreenTransition : MonoBehaviour {

    public bool m_coverScreen = true;

    [SerializeField]
    float m_initialAlpha;
    [SerializeField]
    Texture2D m_defaultTexture;
    Texture2D m_texture;

    Color m_currentColor;
    float m_alpha;
    float m_deltaTimeModifier;

    string m_sceneToLoad;
    bool m_sceneQueued = false;

	// Use this for initialization
	void Start() 
    {
        m_currentColor = GUI.color;
        m_alpha = m_initialAlpha;
        m_deltaTimeModifier = 1.0f;
        m_texture = m_defaultTexture;
	}
	
	// Update is called once per frame
	void Update() 
    {

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

        if(m_sceneQueued && m_alpha >=1.0f)
        {
            Application.LoadLevel(m_sceneToLoad);
        }


	}

    public void SetScreen(bool a_coverScreen, float a_timeToFade = 1.0f)
    {
        m_coverScreen = a_coverScreen;

        m_deltaTimeModifier = a_timeToFade;
    }

    public void SetTexture(Texture2D a_texture)
    {
        m_texture = a_texture;
    }

    public void ResetTexture()
    {
        m_texture = m_defaultTexture;
    }

    public void TransitionToScene(string a_sceneName)
    {
        m_sceneToLoad = a_sceneName;
        m_sceneQueued = true;
        m_texture = m_defaultTexture;
        SetScreen(true);
    }

    void OnGUI()
    {
        GUI.color = m_currentColor; //cannot directly assign alpha value because unity, so this workaround is needed

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), m_texture);
    }
}
