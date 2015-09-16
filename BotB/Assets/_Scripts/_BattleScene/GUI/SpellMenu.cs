using UnityEngine;
using System.Collections;


public class SpellMenu : MonoBehaviour 
{
    [SerializeField]
    static SpellType m_currentSelection;

    [SerializeField]
    GameObject m_offenceButton;
    SpellMenuButton m_offenceButtonScript;
    [SerializeField]
    GameObject m_defenceButton;
    SpellMenuButton m_defenceButtonScript;
    [SerializeField]
    GameObject m_effectSelection;
    SpellMenuButton m_effectSelectionScript;
    public static bool m_showMenu;
    public static SpellMenu s_ref;

	// Use this for initialization
	void Start() 
    {
        ShowMenu();
        m_offenceButtonScript = m_offenceButton.GetComponent<SpellMenuButton>();
        m_defenceButtonScript = m_defenceButton.GetComponent<SpellMenuButton>();
        m_effectSelectionScript = m_effectSelection.GetComponent<SpellMenuButton>();
        s_ref = this;
        
	}
	
	//Updates the menu
	void Update() 
    {
        if (TurnTimer.Instance.CurrentTurn == Turn.Menu)
            ShowMenu();
        else
            HideMenu();  

        if(m_showMenu)
        {
            if (Input.GetButtonDown("Triangle"))
                SelectOffence();
            else if (Input.GetButtonDown("Square"))
                SelectDefence();
            else if (Input.GetButtonDown("Circle"))
                SelectEffect();
        }
	}

    public static SpellMenu Instance
    {
        get { return s_ref; }
    }
    ///  <summary> Returns the current type of spell the player has selected</summary>
    public static SpellType Selection
    {
        get { return m_currentSelection; }
    }
    public void SelectOffence()
    {
        m_currentSelection = SpellType.Offencive;
        m_offenceButtonScript.SetSelected();

        m_defenceButtonScript.SetUnselected();
        m_effectSelectionScript.SetUnselected();
    }
    public void SelectDefence()
    {
        m_currentSelection = SpellType.Defensive;
        m_defenceButtonScript.SetSelected();

        m_offenceButtonScript.SetUnselected();
        m_effectSelectionScript.SetUnselected();
    }
    public void SelectEffect()
    {
        m_currentSelection = SpellType.Effect;
        m_effectSelectionScript.SetSelected();


        m_offenceButtonScript.SetUnselected();
        m_defenceButtonScript.SetUnselected();
    }
    public void ShowMenu()
    {
        m_showMenu = true;
        m_offenceButton.GetComponent<SpellMenuButton>().Show();
        m_defenceButton.GetComponent<SpellMenuButton>().Show();
        m_effectSelection.GetComponent<SpellMenuButton>().Show();
    }
    public void HideMenu()
    {
        m_showMenu = false;
        m_offenceButton.GetComponent<SpellMenuButton>().Hide();
        m_defenceButton.GetComponent<SpellMenuButton>().Hide();
        m_effectSelection.GetComponent<SpellMenuButton>().Hide();

        m_offenceButtonScript.SetUnselected();
        m_defenceButtonScript.SetUnselected();
        m_effectSelectionScript.SetUnselected();
    }
}
