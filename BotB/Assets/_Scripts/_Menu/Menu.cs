using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
    private Ray m_ray;//The Ray to cast
    RaycastHit m_hit;//The infomation from the ray

    [SerializeField]
    Transform m_playTextTrans;//Where the play button is 
    [SerializeField]
    Transform m_quitTextTrans;//Where the play button is 

	void Start() 
    {
	}
	
	void Update() 
    {
        m_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	    if(Physics.Raycast(m_ray, out m_hit))
        {
            if (m_hit.transform == m_playTextTrans && Input.GetMouseButtonDown(0))
                SwitchToGame();
            else if (m_hit.transform == m_quitTextTrans && Input.GetMouseButtonDown(0))
                Quit();
        }
	}

    /// <summary>Switches to the main battle state </summary>
    private void SwitchToGame()
    {
        Application.LoadLevel("MainScene");
    }

    /// <summary>Switches to the main menu state </summary
    public void SwitchToMenu()
    {
        Application.LoadLevel("MenuScene");
    }

    /// <summary>Closes the game </summary>
    private void Quit()
    {
        Application.Quit();
    }
}
