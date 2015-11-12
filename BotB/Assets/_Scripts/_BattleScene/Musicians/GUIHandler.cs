using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIHandler : MonoBehaviour 
{
    private static GUIHandler instance;

    Slider m_playerHealthBar;
    Slider m_enemyHealthBar;

	// Use this for initialization
	void Start() 
    {
        instance = this;
        m_playerHealthBar = GetComponentsInChildren<Slider>()[0];
        m_enemyHealthBar = GetComponentsInChildren<Slider>()[1];
	}
	
	// Update is called once per frame
	void Update() 
    {
	    if(Battle.Instance.m_activeBattle)
        {
            m_playerHealthBar.GetComponentsInChildren<CanvasRenderer>()[0].SetAlpha(1);
            m_enemyHealthBar.GetComponentsInChildren<CanvasRenderer>()[0].SetAlpha(1);
        }
        else
        {
            m_playerHealthBar.GetComponentsInChildren<CanvasRenderer>()[0].SetAlpha(0);
            m_enemyHealthBar.GetComponentsInChildren<CanvasRenderer>()[0].SetAlpha(0);
        }

        
	}

    public static GUIHandler Instance
    {
        get { return instance; }
    }

    public Slider EnemyLifeBar
    {
        get {return GetComponentsInChildren<Slider>()[1];}
    }
        
}
