using UnityEngine;
using System.Collections;

public enum MenuSelection
{
    offence,
    defence,
    effect
}

public class SpellMenu : MonoBehaviour 
{
    [SerializeField]
    static MenuSelection m_currentSelection;

	// Use this for initialization
	void Start() 
    {
        
	}
	
	// Update is called once per frame
	void Update() 
    {
	    
	}

    public static MenuSelection SpellType
    {
        get { return m_currentSelection; }
    }

    public static void SelectOffence()
    {
        m_currentSelection = MenuSelection.offence;
    }

    public static void SelectDefence()
    {
        m_currentSelection = MenuSelection.defence;
    }

    public static void SelectEffect()
    {
        m_currentSelection = MenuSelection.effect;
    }
}
